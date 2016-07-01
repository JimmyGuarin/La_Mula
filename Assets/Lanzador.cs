using UnityEngine;
using System.Collections;

public class Lanzador : MonoBehaviour {


    


    public Transform[] bases;


    public GameObject proyectil;


    //Parametros de Calculo de Tiro Parabolico
    public int altura = 20;
    public float velocidad;
    public float distancia;
    public float totalTime;




    // Use this for initialization
    void Start () {

        disparar();

	
	}



	
	// Update is called once per frame
	void Update () {

        


	}

    void disparar()
    {

        Transform baseT = bases[Random.Range(0, bases.Length)];
        float distancia =Mathf.Abs((baseT.position - transform.position).magnitude);
        Debug.Log(distancia);
        transform.LookAt(baseT);

        float g = Physics.gravity.magnitude;
        float vertSpeed = Mathf.Sqrt(2 * g * altura);
        // calculate the total time var 
        totalTime = 2 * vertSpeed / g;
        

      

        float horSpeed = distancia / totalTime;

        // calculate the horizontal speed 

        GameObject proyectilClone =(GameObject) Instantiate(proyectil, transform.position, transform.rotation);

        Vector3 velocidad = new Vector3(0, vertSpeed, horSpeed);
        proyectilClone.GetComponent<Rigidbody>().velocity =transform.TransformDirection(velocidad); // launch the projectile! 



        Invoke("disparar", 1f);
    }


}
