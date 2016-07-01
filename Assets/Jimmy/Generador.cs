using UnityEngine;
using System.Collections;

public class Generador : MonoBehaviour {


    public GameObject proyectil;

    public GameObject Canasto;

    public float distancia;

	// Use this for initialization
	void Start () {


        distancia = Mathf.Abs(transform.position.z - Canasto.transform.position.z);


        float vel = (9.81f / 2) * (distancia /0.766f) * (1 /0.6427f);

        proyectil.GetComponent<Impulsar>().velocidad = vel;
        proyectil.GetComponent<Impulsar>().distancia = distancia;
        Instantiate(proyectil,this.transform.position,proyectil.transform.rotation);
        //0.698132f

    }

    // Update is called once per frame
    void Update () {
	




	}
}
