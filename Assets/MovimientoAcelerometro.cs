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
    private float altura;

    Vector2 PosicionInicial;
    [SerializeField] float SwipeMinY;
    [SerializeField] float SwipeMinX;

    Vector2 deltaposition = new Vector2(0,0);
    float speedH = 0;


    bool mover = false;

    public float offset;
    public Vector3 carrilAMover;
    public Vector3 carrilActual;
   




    public void Start()
    {
        rg = GetComponent<Rigidbody>();

        pos = Vector2.zero;
        desplazar = pos;
        carriActual = 2;
        Debug.Log(carriActual);

        altura = transform.position.y;


    }


    void Update()
    {

        
    }

    public void FixedUpdate()
    {
        if (tocandoTierra) {

            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
            transform.position = new Vector3(transform.position.x, altura, transform.position.z);
        }


        if (Input.touchCount == 1)
        {
            
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


               
                if (desplazar == Input.GetTouch(0).deltaPosition)
                    return;


                if ((Mathf.Abs(desplazar.x) > Mathf.Abs(desplazar.y)))
                {
                    if (desplazar.x > 0)
                    {

                        if (carriActual > 1 && !mover)
                        {
                            Mover(new Vector3(-16, 0, 0));
                            Camera.main.GetComponent<Camara>().Mover(new Vector3(-10, 0, 0));
                            //transform.Translate(new Vector3(-15, 0, 0));
                            carriActual--;
                            return;
                        }


                    }
                    if (desplazar.x < 0)
                    {
                        if (carriActual < 3&&!mover)
                        {

                            Mover(new Vector3(16, 0, 0));
                            Camera.main.GetComponent<Camara>().Mover(new Vector3(10, 0, 0));
                            //transform.Translate(new Vector3(15, 0, 0));
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

           

        }




       

        if (mover)
        {
            float speedT = 50 * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(carrilAMover.x, transform.position.y, transform.position.z), speedT);
            if (transform.position.x == carrilAMover.x)
            {
                
                mover = false;
                carrilActual = transform.position;
            }
        }
       




    }

    public void Mover(Vector3 objetivo)
    {
        carrilAMover = carrilActual + objetivo;
        mover = true;

    }





    void saltar()
    {
        
            GetComponent<Rigidbody>().velocity = new Vector3(0, fuerzaSalto, -speed);
            GetComponentInChildren<Animator>().speed = 0.3f;

            tocandoTierra = false;
            hasJumped = true;
        
        
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
   
        if (collision.gameObject.tag.Equals("Obtaculo"))
        {

            HUD1.instancia.Perder();
        }
    }
}
