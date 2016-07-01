using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Impulsar : MonoBehaviour {


    Rigidbody rg;
    public  float velocidad;
    public float distancia;
    public float t;

	// Use this for initialization
	void Start () {

        rg = GetComponent<Rigidbody>();

        float y = (velocidad * 0.6427f) - (Physics.gravity.y * (Mathf.Pow(distancia, 2) / (Mathf.Pow(velocidad, 2) * 0.5868f)));


        float x = velocidad * 0.766f;


        t = (2 * velocidad * 0.6427f) / -Physics.gravity.y;

        float acel = velocidad / t;

       

        float f = acel * rg.mass;

        //float y = f* 0.6427f;
        //float x = f * 0.766f;
        Vector3 v = new Vector3(0, y, x);
        Debug.Log(v);
        rg.AddForce(v);
        
       
       
    }
	
	// Update is called once per frame
	void Update () {


       





    }
}
