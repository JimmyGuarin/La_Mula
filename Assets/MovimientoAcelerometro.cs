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
   

    public int velocidadY;

    public GameObject casco;

    public void Start()
    {
        rg = GetComponent<Rigidbody>();

        pos = Vector2.zero;
        desplazar = pos;
        carriActual = 2;

        velocidadY = 0;
        altura = transform.position.y;


    }


    void Update()
    {

        
    }

    public void FixedUpdate()
    {
        

        if (tocandoTierra) {
            
            velocidadY = 0;
            transform.position = new Vector3(transform.position.x, altura, transform.position.z);
            GetComponent<Rigidbody>().velocity = new Vector3(0, velocidadY, -speed);
            GetComponentInChildren<Animator>().speed = 1f;
        }
        if (!tocandoTierra)
        {
            velocidadY--;

            if (velocidadY <=20)
                velocidadY-=3;

            GetComponent<Rigidbody>().velocity = new Vector3(0, velocidadY, -speed);
            Debug.Log(rg.velocity.y);
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
            float speedT = 100 * Time.deltaTime;

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
            velocidadY =(int) fuerzaSalto;
            tocandoTierra = false;
            hasJumped = true;
        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("GameController"))
        {
            ManejadorSuelo.instancia.GenerarSuelo();
        }

        if (other.gameObject.tag.Equals("Casco"))
        {
            if (!casco.activeSelf)
            {
                casco.SetActive(true);
               
            }
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.tag.Equals("Obtaculo"))
        {
            
            Debug.Log("Entra");
            transform.GetChild(0).gameObject.SetActive(false);
            if (!casco.activeSelf)
            {
                Destruida();
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Destroy(collision.gameObject, 2);
                collision.gameObject.GetComponent<Collider>().enabled = false;
                casco.gameObject.SetActive(false);
                return;
            }
            
        }
        if (collision.gameObject.name.Equals("Suelo"))
        {
            tocandoTierra = true;
        }
    }

    public void Destruida()
    {
        rg.velocity = Vector3.zero;
        speed = 0;
        transform.GetChild(1).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        HUD1.instancia.Perder();
        this.enabled = false;
    }
}
