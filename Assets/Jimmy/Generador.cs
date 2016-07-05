using UnityEngine;
using System.Collections;

public class Generador : MonoBehaviour {


    public Transform[] bases;


    


    public GameObject proyectil;

    public GameObject Canasto;

    public float distancia;

	// Use this for initialization
	void Start () {


        distancia = Mathf.Abs(transform.position.z - Canasto.transform.position.z);

        //0.698132f
        InvokeRepeating("Generar", 1, 5);
    }

    // Update is called once per frame
    void Update () {
	




	}


    void Generar()
    {
        proyectil.GetComponent<Impulsar>().Canasto = Canasto;
        Instantiate(proyectil, this.transform.position, proyectil.transform.rotation);
    }
}
