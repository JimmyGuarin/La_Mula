using UnityEngine;
using System.Collections;

public class ManejadorSuelo : MonoBehaviour {


    public GameObject terreno;
    public GameObject[] terrenos;
    public GameObject mula;
    public ArrayList terrenosLista;
    private float offset;
    public static ManejadorSuelo instancia;
    
    // Use this for initialization
	void Start () {


        offset = mula.transform.position.z - transform.position.z;
        instancia = this;

        terrenosLista = new ArrayList();
        terrenosLista.Add(terrenos[0]);
        terrenosLista.Add(terrenos[1]);
        terrenosLista.Add(terrenos[2]);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = mula.transform.position - Vector3.forward*offset;
	}

    public void GenerarSuelo()
    {
        GameObject t=(GameObject)Instantiate(terreno, transform.position- (Vector3.forward*offset), terreno.transform.rotation);
        Destroy((GameObject)terrenosLista[0]);
        terrenosLista.Add(t);
    }
}
