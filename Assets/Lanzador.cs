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
    




    // Use this for initialization
    void Start () {


        InvokeRepeating("disparar", 1, 3);

    }



	
	// Update is called once per frame
	void Update () {


       

	}

    void disparar()
    {


        int indice = Random.Range(0, bases.Length);
        Vector3 baseT = bases[Random.Range(0,bases.Length)].position;
        //transform.LookAt(baseT);
        
       
        
        
        
        float g = Physics.gravity.magnitude;
        float vertSpeed = Mathf.Sqrt(2 * g * altura);
        // calculate the total time var 
        float totalTime = 2 * vertSpeed / g;

        Vector3 Adelante = bases[indice].position + new Vector3(0, 0, -( burra.velocity.magnitude*totalTime));

        Instantiate(puntoCaida, new Vector3(Adelante.x, puntoCaida.transform.position.y, Adelante.z), puntoCaida.transform.rotation);


        transform.LookAt(Adelante);



        float distancia = Mathf.Abs((Adelante - transform.position).magnitude);






        float horSpeed = distancia / totalTime;

        // calculate the horizontal speed 

        
        Vector3 velocidad = new Vector3(0, vertSpeed, horSpeed);
        GameObject proyectilClone = (GameObject)Instantiate(proyectil, transform.position, transform.rotation);
        proyectilClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);// launch the projectile! 


        

    }


}
