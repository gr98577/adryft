using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    private Vector2 direction;
    private Vector2 dir;
    private Vector3[] verticies;
    [SerializeField]
    private float lightAngle;
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private GameObject mesh;
    private MeshFilter lightMeshFilter;
    private Mesh lightMesh;

    private bool lightIsOn;

    // Use this for initialization
    void Start()
    {
        lightMeshFilter = mesh.GetComponent<MeshFilter>();
        lightMesh = mesh.GetComponent<Mesh>();

        lightMesh = new Mesh();
        lightMesh.name = "Light Mesh";
        lightMeshFilter.mesh = lightMesh;

        //wall = GameObject.FindGameObjectWithTag("Wall");
        verticies = new Vector3[201];

        lightIsOn = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale != 0f && !GetComponentInParent<CutsceneController>().inCutscene)
        {
            // Toggles the flash light on/off
            if (Input.GetButtonDown("light"))
            {
                if (lightIsOn)
                {
                    lightIsOn = false;
                    lightMesh.Clear();
                }
                else lightIsOn = true;
            }

            if (lightIsOn)
            {
                //gameObject.layer = 2;
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                float dirAngle = AngleBetweenVector2(transform.position, mousePosition);

                dirAngle -= lightAngle / 2;
                float deg = lightAngle / 200;

                for (int i = 0; i < 200; i++)
                {
                    float radian = (dirAngle + (deg * i)) * Mathf.Deg2Rad;
                    direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, mask.value);

                    //Vector3 temp = new Vector3(transform.position.x - hit.point.x, transform.position.y - hit.point.y, 0);
                    //verticies[i + 1] = temp;
                    verticies[i + 1] = transform.InverseTransformPoint(hit.point);
                    verticies[i + 1].z = 1;

                    //Debug.DrawLine(transform.position, hit.point, Color.green, 0.1f, false);
                }
                //gameObject.layer = 0;

                mesh.transform.position = transform.position;

                verticies[0] = Vector3.zero;
                verticies[0].z = 1;
                //verticies[0] = transform.position;
                int[] triangles = new int[597];
                for (int i = 0; i < 199; i++)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 2;
                    triangles[i * 3 + 2] = i + 1;
                }

                lightMesh.Clear();
                lightMesh.vertices = verticies;
                lightMesh.triangles = triangles;
                lightMesh.RecalculateNormals();
            }
        }
        else if (GetComponentInParent<SpriteRenderer>().color.a == 0)
        {
            lightMesh.Clear();
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}

