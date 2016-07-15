using UnityEngine;
using System.Collections;

public class Lanzador : MonoBehaviour {


    


    public Transform[] bases;

    public Rigidbody burra;
    public GameObject proyectil;
    public GameObject barrilDorado;
    public GameObject bomba;
    public GameObject puntoCaida;

    //Parametros de Calculo de Tiro Parabolico
    public int altura = 10;
    public float distancia;


    public int lanzados;

    bool dinamita;
    bool chocado;


    // Use this for initialization
    void Start () {

        
       // transform.position = new Vector3(transform.position.x, bases[3].position.y, transform.position.z);
        InvokeRepeating("disparar", 1, 2);
        lanzados = 0;
        dinamita = Random.value > 0.5f;
        


    }




    // Update is called once per frame
    void Update () {


       

	}

    void disparar()
    {
        


        int indice = Random.Range(0, bases.Length);
        //int indice = 1;
     
        float g = Physics.gravity.magnitude;
        float vertSpeed = Mathf.Sqrt(2 * g * altura);
        // calculate the total time var 
        float totalTime = 2 * vertSpeed / g;


        Vector3 Adelante = bases[indice].position + new Vector3(0, 0, -( burra.velocity.magnitude*totalTime));
        Vector3 puntoCae = new Vector3(Adelante.x, puntoCaida.transform.position.y, Adelante.z);
        Collider [] co=Physics.OverlapBox(puntoCae, new Vector3(5, 10, 5));

        bool colisiona = false;
        foreach(Collider c in co)
        {
            if (c.gameObject.tag.Equals("Obtaculo"))
            {
                colisiona = true;
                break;
            }
        }

        bool bonusDorado = Random.value > 0.3f;


        transform.LookAt(Adelante);
        float distancia = Mathf.Abs((Adelante - transform.position).magnitude);
        // calculate the horizontal speed 
        float horSpeed = distancia / totalTime;
        

        
        Vector3 velocidad = new Vector3(0, vertSpeed, horSpeed);

        dinamita = Random.value > 0.6f;
        if(colisiona==false)
        {
            Instantiate(puntoCaida, puntoCae, puntoCaida.transform.rotation);

            if (dinamita)
            {
                //bomba
                GameObject bombaClone = (GameObject)Instantiate(bomba, transform.position + new Vector3(0, -4, 0), transform.rotation);
                bombaClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);
                // launch the projectile! 

            }
            else if(bonusDorado)
            {
                GameObject barrilDoradot = (GameObject)Instantiate(barrilDorado, transform.position, transform.rotation);
                barrilDoradot.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);
            }
            else

            {
                proyectil.GetComponent<Impulsar>().mitadTiempoVuelo = totalTime / 2;
                GameObject proyectilClone = (GameObject)Instantiate(proyectil, transform.position, transform.rotation);
                proyectilClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);// launch the projectile! 

            }
        }

        lanzados++;
        if (lanzados % 10 == 0 && burra.velocity.magnitude<200)
        {

            StartCoroutine(cambiarVelocidad(totalTime+1));
            
        }

    }

    


    IEnumerator cambiarVelocidad(float time)
    {
        CancelInvoke("disparar");
        yield return new WaitForSeconds(time+Time.deltaTime);
        //burra.GetComponent<MovimientoAcelerometro>().speed += 10;
        //altura +=1;
        HUD1.instancia.CambioVelocidad();
        InvokeRepeating("disparar",1, 3);
    }

    


}
