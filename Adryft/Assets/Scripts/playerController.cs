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

    private damageController dc;
    

    //variables
    private float mSpeed;
    private float tSpeed;
    private bool stunned;
    private int dash;
    private int ammo;
    private int maxAmmo = 20;
    private float stamina;
    private float maxStamina = 1f;
    private Vector3 direction;
    private bool isStaminaRegen = false;
    private bool isStaminaUsed = false;
    private bool isStaminaInDelay = false;

    public int getMaxAmmo()
    { return maxAmmo; }
    public int getAmmunition()
    { return ammo; }

    public float getMaxStamina()
    { return maxStamina; }
    public float getStamina()
    { return stamina; }
    public void zeroStamina()
    {
        stamina = -1;
        //StopCoroutine(staminaRegen());
        //StartCoroutine(staminaDelay());
    }

    public void setAmmunition(int ammount)
    {
        ammo = ammount;
        normalizeAmmo();
    }
    public void incAmmunition(int ammount)
    {
        ammo += ammount;
        normalizeAmmo();
    }

    public bool getIsStunned()
    { return stunned; }

    // Use this for initialization
    void Start()
    {
        mSpeed = 2f;

        ochSource = GetComponent<AudioSource>();

        dc = GetComponent<damageController>();

        ammo = maxAmmo;

        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetButtonDown("Jump") && dash == 0)
        {
            if (useStamina(.9f))
            {
                dash = 1;
                StartCoroutine(Dash());
            }
        }

        if (dash == 1)
        {
            tSpeed = mSpeed * Time.deltaTime * 5;
        }
        else
        {
            tSpeed = mSpeed * Time.deltaTime;
        }
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        transform.Translate(tSpeed * direction.normalized);
    }

    public bool useStamina(float ammount)
    {
        if (isStaminaInDelay == true)
        {
            StopCoroutine(staminaRegen());
            StartCoroutine(staminaRegen());
            return false;
        }
        else if (isStaminaUsed)
        {
            return false;
        }
        else if (ammount <= stamina)
        {
            stamina -= ammount;
            //StartCoroutine(staminaDelay());
            StartCoroutine(staminaRegen());
            return true;
        }
        return false;
    }

    IEnumerator Dash()
    {
        isStaminaUsed = true;
        dc.setFlying(true);
        yield return new WaitForSeconds(0.15F);
        dash = 2;
        dc.setFlying(false);
        isStaminaUsed = false;
        yield return new WaitForSeconds(1F);
        dash = 0;
    }

    IEnumerator stun(float time)
    {
        if (!stunned)
        {
            stunned = true;
            yield return new WaitForSeconds(time/2);
            stunned = false;
        }
    }

    IEnumerator staminaDelay()
    {
        isStaminaInDelay = true;
        yield return new WaitForSeconds(1.5f);
        isStaminaRegen = false;
        stamina = 0f;
        StartCoroutine(staminaRegen());
        isStaminaInDelay = false;
    }

    IEnumerator staminaRegen()
    {
        if (isStaminaRegen)
            yield break;

        isStaminaRegen = true;
        yield return new WaitForSeconds(0.01f);
        while (stamina <= maxStamina)
        {
            yield return new WaitForSeconds(0.01f);
            stamina += 0.005f;
            if (stamina <= 0.0f)
            {
                Debug.Log("YAYAYAYAY");
                StartCoroutine(staminaDelay());
                yield break;
            }
        }
        isStaminaRegen = false;
        normalizeStamina();
    }

    void normalizeStamina()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        else if (stamina < 0) stamina = 0;
    }

    void normalizeAmmo()
    {
        if (ammo > maxAmmo) ammo = maxAmmo;
        else if (ammo < 0) ammo = 0;
    }
}

        
