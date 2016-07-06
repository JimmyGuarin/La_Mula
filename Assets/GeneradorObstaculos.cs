using UnityEngine;
using System.Collections;

public class GeneradorObstaculos : MonoBehaviour
{

    public GameObject[] obstaculos;
    public GameObject[] bases;
    private int altura = 5;
    public Rigidbody burra;

    // Use this for initialization
    void Start()
    {
        
        InvokeRepeating("crearObstaculos", 1, 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    void crearObstaculos()
    {
        
        GameObject obstaculo = obstaculos[Random.Range(0, obstaculos.Length)];
        int indice = Random.Range(0, bases.Length);

        if(indice<=4)
        {
            float g = Physics.gravity.magnitude;
            float vertSpeed = Mathf.Sqrt(2 * g * altura);
            // calculate the total time var 
            float totalTime = 2 * vertSpeed / g;

            Vector3 Adelante = bases[indice].transform.position + new Vector3(0, 0, -(burra.velocity.magnitude * totalTime));

            transform.LookAt(Adelante);

            float distancia = Mathf.Abs((Adelante - transform.position).magnitude);

            float horSpeed = distancia / totalTime;

            // calculate the horizontal speed 
            Vector3 velocidad = new Vector3(0, vertSpeed, horSpeed);
            GameObject ObstaculoClone = (GameObject)Instantiate(obstaculo, transform.position, transform.rotation);
            ObstaculoClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);
        }
        else
        {

        }

        






        // Instantiate(obstaculo, bases[].transform.position, transform.rotation);

    }

    
}
