using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{


    // Game Object Variables
    [SerializeField]
    private AudioSource ochSource;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private GameObject bloodMed;
    [SerializeField]
    private GameObject bloodSml;
    public CameraController mainCamera;

    private damageController dc;
    private BoxCollider2D bc;
    private CircleCollider2D cc;

    //variables
    private float mSpeed;
    public bool isSlowed;
    private float tSpeed;
    private bool stunned;
    private int dash;
    private int ammo;
    private int maxAmmo = 20;
    private float stamina;
    private float maxStamina = 1f;
    private Vector2 direction;
    private bool isStaminaRegen = false;
    private bool isStaminaUsed = false;
    private Rigidbody2D rb;
    //private bool isStaminaInDelay = false;

    // Max ammo getter
    public int getMaxAmmo()
    { return maxAmmo; }
    // Ammo getter
    public int getAmmunition()
    { return ammo; }

    // Max Stamina getter
    public float getMaxStamina()
    { return maxStamina; }
    // Stamina getter
    public float getStamina()
    { return stamina; }
    // Sets stamina to zero
    public void zeroStamina()
    { stamina = -1; }

    public float getTSpeed()
    { return tSpeed; }

    // Ammo setter
    public void setAmmunition(int ammount)
    {
        ammo = ammount;
        normalizeAmmo();
    }
    // Ammo incramenter
    public void incAmmunition(int ammount)
    {
        ammo += ammount;
        normalizeAmmo();
    }

    // Stunned getter 
    public bool getIsStunned()
    { return stunned; }

    // Use this for initialization
    void Start()
    {
        mSpeed = 2f;
        isSlowed = false;
        ochSource = GetComponent<AudioSource>();
        dc = GetComponent<damageController>();
        rb = GetComponent<Rigidbody2D>();
        /*
        bc = GetComponent<BoxCollider2D>();
        cc = GetComponent<CircleCollider2D>();
        cc.enabled = false;
        */
        ammo = maxAmmo;
        stamina = maxStamina;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(mainCamera.cameraShake(0.1f, 0.01f));
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //StartCoroutine(mainCamera.cameraShake(0.1f, 0.001f));
        }

        if (stunned)
        {
            // stun effect
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        
        //checks if Dash button (Space) was pressed AND
        //1. player wasn't already in dash mode,
        //2. player is stationary
        if (Input.GetButtonDown("Jump") && dash == 0 && (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f))
        {
            dash = 1;
            gameObject.GetComponent<TrailRenderer>().enabled = true;
            StartCoroutine(Dash());
        }

        // If dashing move faster and set flying to true
        if (!isSlowed)
        {
            if (dash == 1)
            {
                tSpeed = mSpeed * Time.deltaTime * 4;
            }
            else
            {
                tSpeed = mSpeed * Time.deltaTime;
            }
        }
        else
        {
            tSpeed = (mSpeed * Time.deltaTime) / 2;
        }
        // Move in the desired direction
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //transform.Translate(tSpeed * direction.normalized);

        direction = tSpeed * direction.normalized;
        float x = transform.position.x + (direction.x * 1.1f);
        float y = transform.position.y + (direction.y * 1.1f);
        Vector2 pos = new Vector2(x, y);
        rb.MovePosition(pos);
    }

    // Use stamina
    public bool useStamina(float ammount)
    {
        StartCoroutine(staminaRegen());

        if (ammount <= stamina)
        {
            stamina -= ammount;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Bleeding
    public void bleed(bool heavy)
    {
        float x = transform.position.x;
        x = Random.Range(x - 0.01f, x + 0.01f);
        float y = transform.position.y;
        y = Random.Range(y - 0.01f, y + 0.01f);
        Vector3 pos = new Vector3(x, y, 0);

        if (heavy)
        {
            GameObject clone = Instantiate(bloodMed, pos, Quaternion.identity);
        }
        else
        {
            
            GameObject clone = Instantiate(bloodSml, pos, Quaternion.identity);
        }

        
    }

    // Dash
    IEnumerator Dash()
    {
        // Dash
        dc.setFlying(true);
        yield return new WaitForSeconds(0.15F);
        dash = 2;
        dc.setFlying(false);
        gameObject.GetComponent<TrailRenderer>().enabled = false;

        // Dash cooldown
        yield return new WaitForSeconds(1F);
        dash = 0;
    }

    // Stun
    IEnumerator stun(float time)
    {
        if (!stunned)
        {
            stunned = true;
            yield return new WaitForSeconds(time/2);
            stunned = false;
        }
    }

    // Regenerates stamina
    IEnumerator staminaRegen()
    {
        isStaminaUsed = true;
        // Delays alot if there is no stamina left
        if (stamina <= 0f)
        {
            yield return new WaitForSeconds(1.5f);
        }
        // Delays a little if there is stamina left
        else
        {
            yield return new WaitForSeconds(0.4f);
        }
        isStaminaUsed = false;

        // Makes sure not to double up on regen
        if (isStaminaRegen || isStaminaUsed)
        {
            yield break;
        }

        isStaminaRegen = true;
        yield return new WaitForSeconds(0.01f);

        // Loops until stamina is full
        while (stamina <= maxStamina)
        {
            yield return new WaitForSeconds(0.01f);
            // Increases stamina
            stamina += 0.005f;

            // Stops if stamina is used
            if (isStaminaUsed)
            {
                isStaminaRegen = false;
                yield break;
            }
        }
        isStaminaRegen = false;
        normalizeStamina();
    }

    // Normalizes stamina
    void normalizeStamina()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;
    }

    // Normalizes Ammo
    void normalizeAmmo()
    {
        if (ammo > maxAmmo) ammo = maxAmmo;
        else if (ammo < 0) ammo = 0;
    }

    public void SaveTo(PlayerData pd)
    {
        pd.p_ammo = ammo;
        pd.p_maxAmmo = ammo;
        pd.p_maxStamina = maxStamina;

        dc.SaveTo(pd);
    }

    public void LoadFrom(PlayerData pd)
    {
        ammo = pd.p_ammo;
        maxAmmo = pd.p_maxAmmo;
        maxStamina = pd.p_maxStamina;
        stamina = maxStamina;

        dc.LoadFrom(pd);
    }
}

        
