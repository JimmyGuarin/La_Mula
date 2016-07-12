﻿using UnityEngine;


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
    // Si la mula se está moviendo
    private bool moviendo;  
    //Carril donde se va a mover la Mula
    private Vector3 carrilAMover;
    //Si se va  mover la Muula
    private bool mover = false;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    //Metodo llamado al inicio del script
    public void Start()
    {
        rg = GetComponent<Rigidbody>();

        pos = Vector2.zero;
        carriActual = 2;

        velocidadY = 0;
        altura = transform.position.y;
        dragDistance = Screen.width * 10 / 100;

    }


    void Update()
    {

        if(Input.touchCount>0)  //use loop to detect more than one swipe
        { //can be ommitted if you are using lists 
            Touch touch = Input.GetTouch(0);

          if (touch.phase == TouchPhase.Began) //check for the first touch
          {
              fp = touch.position;
              lp = touch.position;
              transform.position = new Vector3(carrilAMover.x, transform.position.y, transform.position.z);
              carrilActual = transform.position;
              mover = false;
            }

            if (touch.phase == TouchPhase.Moved) //add the touches to list as the swipe is being made
            {
                //touchPositions.Add(touch.position);
            }

            if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
                //fp = touchPositions[0]; //get first touch position from the list of touches
                //lp = touchPositions[touchPositions.Count - 1]; //last touch position 

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                   
                    //check if the drag is vertical or horizontal 
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                       
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            if (carriActual > 1)
                            {
                                Mover(new Vector3(-16, 0, 0));
                                Camera.main.GetComponent<Camara>().Mover(new Vector3(-10, 0, 0));
                                carriActual--;
                                mover = true;

                            }
                            Debug.Log("Right Swipe");
                        }
                        else
                        {   //Left swipe

                            if (carriActual < 3)
                            {
                                carriActual++;
                                mover = true;
                                Mover(new Vector3(16, 0, 0));
                                Camera.main.GetComponent<Camara>().Mover(new Vector3(10, 0, 0));

                            }
                            Debug.Log("Left Swipe");
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            if (tocandoTierra)
                                saltar();
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
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
                carrilActual = transform.position;
                mover = false;
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
            Destroy(other.transform.parent.gameObject);
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
