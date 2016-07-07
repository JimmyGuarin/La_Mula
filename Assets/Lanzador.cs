using UnityEngine;
using System.Collections;

public class Lanzador : MonoBehaviour {


    


    public Transform[] bases;

    public Rigidbody burra;
    public GameObject proyectil;
    public GameObject puntoCaida;

    //Parametros de Calculo de Tiro Parabolico
    public int altura = 10;
    public float distancia;


    public int lanzados;


    // Use this for initialization
    void Start () {


       // transform.position = new Vector3(transform.position.x, bases[3].position.y, transform.position.z);
        InvokeRepeating("disparar", 1, 3);
        lanzados = 0;
    }



	
	// Update is called once per frame
	void Update () {


       

	}

    void disparar()
    {
        lanzados++;

        if (lanzados % 5 == 0)
        {
            burra.GetComponent<MovimientoAcelerometro>().speed+=10;
            HUD1.instancia.CambioVelocidad((int)burra.GetComponent<MovimientoAcelerometro>().speed);


        }


        //int indice = Random.Range(0, bases.Length);
        int indice = 1;
        Vector3 baseT = bases[indice].position;
     
        float g = Physics.gravity.magnitude;
        float vertSpeed = Mathf.Sqrt(2 * g * altura);
        // calculate the total time var 
        float totalTime = 2 * vertSpeed / g;
        Vector3 Adelante = bases[indice].position + new Vector3(0, 0, -( burra.velocity.magnitude*totalTime));
        Instantiate(puntoCaida, new Vector3(Adelante.x, puntoCaida.transform.position.y, Adelante.z), puntoCaida.transform.rotation);
        transform.LookAt(Adelante);
        float distancia = Mathf.Abs((Adelante - transform.position).magnitude);
        // calculate the horizontal speed 
        float horSpeed = distancia / totalTime;
        

        
        Vector3 velocidad = new Vector3(0, vertSpeed, horSpeed);
        GameObject proyectilClone = (GameObject)Instantiate(proyectil, transform.position, transform.rotation);
        proyectilClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);// launch the projectile! 

       
        Debug.Log(burra.GetComponent<Rigidbody>().velocity.magnitude);

    }


    

}
