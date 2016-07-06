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
    public Text desplazamiento;

    float deltaTime = 0.0f;
    float fps1 = 0.0f;


    public int carriActual;
    public Vector2 pos;
    public Vector2 desplazar;

    private bool moviendo;

    public float fuerzaSalto;
    public bool tocandoTierra = true;
    private bool hasJumped = false;






    public void Start()
    {
        rg = GetComponent<Rigidbody>();

        pos = Vector2.zero;
        desplazar = pos;
        carriActual = 3;


    }


    void Update()
    {




        





    }

    public void FixedUpdate()
    {

        if (tocandoTierra)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);


        if (Input.touchCount==1)
        {
            fps.text = Input.GetTouch(0).phase.ToString();
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pos = Vector2.zero;
            }
            pos += Input.GetTouch(0).deltaPosition;

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (desplazar == pos)
                {
                    desplazar = Vector2.zero;
                    return;
                }
                else
                {
                    desplazar = pos;
                }    

                
                desplazamiento.text = "" + pos;

                if (desplazar == Input.GetTouch(0).deltaPosition)
                    return;
               

                if ((Mathf.Abs(desplazar.x) > Mathf.Abs(desplazar.y)))
                {
                    if (desplazar.x > 0)
                    {

                        if (carriActual > 1)
                        {
                            transform.Translate(new Vector3(-15, 0, 0));
                            carriActual--;
                            return;
                        }


                    }
                    if (desplazar.x < 0)
                    {
                        if (carriActual < 5)
                        {
                            transform.Translate(new Vector3(15, 0, 0));
                            carriActual++;
                            return;

                        }
                    }



                }
                if ((Mathf.Abs(desplazar.y) > Mathf.Abs(desplazar.x)))
                {
                    if (desplazar.y > 0)
                    {
                        if (tocandoTierra)
                            saltar();

                    }
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
