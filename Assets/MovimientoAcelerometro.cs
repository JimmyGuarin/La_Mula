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
    public Vector3 cambioCarril;

    private bool moviendo;

    public float fuerzaSalto;
    public bool tocandoTierra = true;
    private bool hasJumped = false;

    




    public void Start()
    {
        rg = GetComponent<Rigidbody>();


        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
        carriActual = 3;


    }


    void Update()
    {

        // transform.Translate(Vector3.forward * speed * Time.deltaTime

        if (!tocandoTierra && GetComponent<Rigidbody>().velocity.y <= 0.1)
        {
            tocandoTierra = true;
            GetComponent<Animator>().speed = 1;


        }
       




    }

    public void FixedUpdate()
    {


        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 pos = touch.deltaPosition;

                if ((Mathf.Abs(pos.x) > Mathf.Abs(pos.y)))
                {
                    if (pos.x > 0)
                    {

                        if (carriActual > 1)
                        {
                            transform.Translate(new Vector3(15, 0, 0));
                            carriActual--;
                        }


                    }
                    if (pos.x < 0)
                    {
                        if (carriActual < 5)
                        {
                            transform.Translate(new Vector3(-15, 0, 0));
                            carriActual++;
                        }
                    }

                    Debug.Log(carriActual);

                }
                if ((Mathf.Abs(pos.y) > Mathf.Abs(pos.x)))
                {
                    if (pos.y > 0)
                    {

                        saltar();
                    }
                }


            }


        }
    }


    void saltar()
    {
       
        GetComponent<Rigidbody>().AddForce(transform.up * fuerzaSalto);
        GetComponent<Animator>().speed = 0.3f;
        
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
}
