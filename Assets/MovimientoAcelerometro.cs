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


    Vector2 PosicionInicial;
    [SerializeField] float SwipeMinY;
    [SerializeField] float SwipeMinX;

    Vector2 deltaposition = new Vector2(0,0);
    float speedH = 0;


    bool mover = false;



    public void Start()
    {
        rg = GetComponent<Rigidbody>();

        pos = Vector2.zero;
        desplazar = pos;
        carriActual = 2;
        Debug.Log(carriActual);


    }


    void Update()
    {

        
    }

    public void FixedUpdate()
    {

        if (tocandoTierra)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);

        

        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            

            if (t.phase == TouchPhase.Began)
            {
                PosicionInicial = t.position;
            }
            else if (t.phase == TouchPhase.Ended)

            {
                
                float swipeVertical = (new Vector3(0, t.position.y, 0) - new Vector3(0, PosicionInicial.y, 0)).magnitude;
                if (swipeVertical > SwipeMinY)
                {
                    float u = Mathf.Sign(t.position.y - PosicionInicial.y);
                    if (u > 0)
                    {
                        if (tocandoTierra)
                            saltar();
                    }
                    if (u < 0)
                    {

                        //Moverse hacia abajo
                    }
                }

                float swipeHorizontal = (new Vector3(t.position.x, 0, 0) - new Vector3(PosicionInicial.x, 0, 0)).magnitude;
                if (swipeHorizontal > SwipeMinX)
                {
                    float u = Mathf.Sign(t.position.x - PosicionInicial.x);
                    if (u > 0)
                    {
                        //Moverse hacia la derecha
                        if (carriActual <= 2 &&deltaposition!=t.deltaPosition)
                        {

                            moverse(transform.position + new Vector3(-30, 0, 0));
                            deltaposition = t.deltaPosition;
                            carriActual++;

                            Debug.Log(carriActual);
                          
                            
                        }
                        else
                        {
                            //Esta en el carril3
                        }

                    }
                    if (u < 0)
                    {

                        //Moverse hacia la izquierda
                        if (carriActual >= 2 && deltaposition != t.deltaPosition)
                        {

                            moverse(transform.position + new Vector3(30,0,0));
                            deltaposition = t.deltaPosition;
                            carriActual--;
                            Debug.Log(carriActual);
                           
                         
                        }
                        else
                        {
                            //esta en el Carril1
                        }
                    }
                }
            }
            
        }




    }

    void moverse(Vector3 target)
    {
        float maxDistance = 50 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, maxDistance);

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
       // Debug.Log(collision.gameObject.name);
        tocandoTierra = true;
        GetComponentInChildren<Animator>().speed = 1;
    }
}
