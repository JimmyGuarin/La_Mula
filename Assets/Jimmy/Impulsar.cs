using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Impulsar : MonoBehaviour {


    Rigidbody rg;
    public  float velocidad;
    public float distancia;
    public float totalTime;
    public GameObject Canasto;
    public GameObject Caballo;
    public int altura = 20;

    // Use this for initialization
    void Start () {


        transform.position = new Vector3(Canasto.transform.position.x, Canasto.transform.position.y, transform.position.z);
        distancia = Mathf.Abs(transform.position.z - Canasto.transform.position.z);
        rg = GetComponent<Rigidbody>();
        float g = Physics.gravity.magnitude;

       
       




        float vertSpeed = Mathf.Sqrt(2 * g *altura);
        totalTime = 2 * vertSpeed / g;

        float d2 = 30 * totalTime;
        Debug.Log("d2:" + d2);
        Debug.Log("distancia" + distancia);

        distancia = Mathf.Abs(distancia - d2);

        Debug.Log("distanciaFinal" + distancia);

        float horSpeed = distancia / totalTime;
     
        // calculate the horizontal speed 
        rg.velocity = new Vector3(0, vertSpeed, horSpeed); // launch the projectile! 

    }

    // Update is called once per frame
    void Update () {


       





    }
}
