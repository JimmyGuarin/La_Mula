using UnityEngine;


/// <summary>
/// Clase que representa el Movimiento de la Mula
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovimientoAcelerometro : MonoBehaviour
{


    //VARIABLES PUBLICAS..............................................................................

    //Variable que representa la velocidad de la mula
    public float speed = 30f;
    //traslacion lateral de la mula
    public float traslacion = 20.0f;
    //Fuerza de salto de la mula
    public float fuerzaSalto;
    // Si la mula está tocando el suelo
    public bool tocandoTierra = true;
    //Altura que tendrá el barril
    public float altura;
    //Velocidad en el salto de la Mula
    public int velocidadY;
    //Referencia al casco que tendra la Mula
    public GameObject casco;
    //Referencia al Campo de fuerza Iman
    public GameObject iman;
    public GameObject refCanastoIzquierdo;
    public GameObject refCanastoDerecho;

    //VARIABLES PRIVADAS..............................................................................

    //Componente Rigibody de la Mula
    private Rigidbody rg;
    //Indice del Carril actual de la Mula
    private int carriActual;
    //Carril actual de la Mula
    public Vector3 carrilActual;
    //Posicion acumulada del desplazamiento del touch
    private Vector2 pos;
    //Desplazamiento del touch
    private Vector2 desplazar;
    // Si la mula se está moviendo
    private bool moviendo;  
    //Carril donde se va a mover la Mula
    private Vector3 carrilAMover;
    //Si se va  mover la Muula
    private bool mover = false;


    //Metodo llamado al inicio del script
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
       

        if (Input.touchCount == 1)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                pos = Vector2.zero;
                mover = true;
               
            }

            if (toque.phase == TouchPhase.Moved && mover&&toque.deltaPosition.magnitude>1)
            {
                desplazar = Input.GetTouch(0).deltaPosition;
                mover = false;
                Debug.Log(desplazar);

                if (Mathf.Pow(desplazar.x, 2) >=Mathf.Pow(desplazar.y, 2)&&desplazar.x!=0&& !mover)
                {
                    if (desplazar.x > 0.5)
                    {
                        if (carriActual > 1)
                        {
                            Mover(new Vector3(-16, 0, 0));
                            Camera.main.GetComponent<Camara>().Mover(new Vector3(-10, 0, 0));
                            //transform.Translate(new Vector3(-15, 0, 0));
                            carriActual--;
                           
                        }
                        return;
                    }
                    else
                    {
                        if (carriActual < 3)
                        {
                            Mover(new Vector3(16, 0, 0));
                            Camera.main.GetComponent<Camara>().Mover(new Vector3(10, 0, 0));
                            //transform.Translate(new Vector3(15, 0, 0));
                            carriActual++;
                           
                        }
                        return;
                    }
                }
                else
                {
                    if (desplazar.y > 0.5 && !mover)
                    {
                        if (tocandoTierra)
                            saltar();
                       
                    }
                    return;
                }
            }

        }
        if (!mover)
        {
            float speedT = 100 * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(carrilAMover.x, transform.position.y, transform.position.z), speedT);
            if (transform.position.x == carrilAMover.x)
            {
                carrilActual = transform.position;
            }
        }

    }

    public void FixedUpdate()
    {

        if (tocandoTierra)
        {

            velocidadY = 0;
            transform.position = new Vector3(transform.position.x, altura, transform.position.z);
            GetComponent<Rigidbody>().velocity = new Vector3(0, velocidadY, -speed);
            GetComponentInChildren<Animator>().speed = 1f;
        }
        if (!tocandoTierra)
        {
            velocidadY--;

            if (velocidadY <= 20)
                velocidadY -= 3;

            GetComponent<Rigidbody>().velocity = new Vector3(0, velocidadY, -speed);

        }


    }

    public void Mover(Vector3 objetivo)
    {
        carrilAMover = carrilActual + objetivo;
    
    }

    void saltar()
    {        
         GetComponent<Rigidbody>().velocity = new Vector3(0, fuerzaSalto, -speed);
         GetComponentInChildren<Animator>().speed = 0.3f;
         velocidadY =(int) fuerzaSalto;
         tocandoTierra = false;     
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
        if(other.gameObject.tag.Equals("Iman"))
        {
            if (!iman.activeSelf)
            {
                iman.SetActive(true);
                HUD1.instancia.MostrarPanelBonus();
            }
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.tag.Equals("Obtaculo"))
        {
            
            
            transform.GetChild(0).gameObject.SetActive(false);
            if (!casco.activeSelf)
            {
                Destruida();
            }
            else
            {
                Destroy(collision.gameObject, 2);
                collision.gameObject.GetComponent<Collider>().enabled = false;
                QuitarCasco();
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

    public void QuitarCasco()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        casco.gameObject.SetActive(false);
    }
}
