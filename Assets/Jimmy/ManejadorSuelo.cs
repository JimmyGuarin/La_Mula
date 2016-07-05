using UnityEngine;
using System.Collections;

public class ManejadorSuelo : MonoBehaviour {


    public GameObject terreno;
    public GameObject[] terrenos;

    public GameObject mula;

    private float offset;
    public static ManejadorSuelo instancia;
    
    // Use this for initialization
	void Start () {


        offset = mula.transform.position.z - transform.position.z;

        instancia = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerarSuelo()
    {
        Instantiate(terreno, transform.position- (Vector3.forward*offset), terreno.transform.rotation);
    }
}
