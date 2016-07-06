using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoAcelerometro : MonoBehaviour
{

    public float speed = 0f;
    public float traslacion = 20.0f;

    private Rigidbody rg;

    public Text VELOCIDAD;
    public Text fps;


    float deltaTime = 0.0f;
    float fps1 = 0.0f;


    public int carriActual;
    public Vector2 pos;

    private bool moviendo;

    public float fuerzaSalto;
    public bool tocandoTierra = true;
    private bool hasJumped = false;






    public void Start()
    {
        rg = GetComponent<Rigidbody>();

        pos = Vector2.zero;

        carriActual = 3;


    }


    void Update()
    {




        





    }

    public void FixedUpdate()
    {

        if (tocandoTierra)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);



        //foreach (Touch touch in Input.touches)
        //{


        //if (touch.phase == TouchPhase.Ended)
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            if (pos == Input.GetTouch(0).deltaPosition)
                return;
            else
            {
                pos = Input.GetTouch(0).deltaPosition;
            }

            if ((Mathf.Abs(pos.x) > Mathf.Abs(pos.y)))
            {
                if (pos.x > 0)
                {

                    if (carriActual > 1)
                    {
                        transform.Translate(new Vector3(-15, 0, 0));
                        carriActual--;

                    }


                }
                if (pos.x < 0)
                {
                    if (carriActual < 5)
                    {
                        transform.Translate(new Vector3(15, 0, 0));
                        carriActual++;

                    }
                }



            }
            if ((Mathf.Abs(pos.y) > Mathf.Abs(pos.x)))
            {
                if (pos.y > 0)
                {
                    if(tocandoTierra)
                         saltar();

                }
            }

            Debug.Log("Carril " + carriActual);
            Debug.Log("Pos " + pos);

        }




        //}
        if (tocandoTierra)
            transform.position = new Vector3(transform.position.x, -0.36f, transform.position.z);
    }


    void saltar()
    {

        GetComponent<Rigidbody>().velocity = new Vector3(0, fuerzaSalto, -speed);
        GetComponentInChildren<Animator>().speed = 0.3f;

        tocandoTierra = false;
        hasJumped = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("GameController"))
        {
            ManejadorSuelo.instancia.GenerarSuelo();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        tocandoTierra = true;
        GetComponentInChildren<Animator>().speed = 1;
    }
}
