using UnityEngine;
using System.Collections;

public class ManejadorSuelo : MonoBehaviour {


    public GameObject terreno;
    public GameObject[] terrenos;
    public GameObject mula;
    public ArrayList terrenosLista;
    private float offset;
    public static ManejadorSuelo instancia;

    private float distanciaTerrenos;

    // Use this for initialization
	void Start () {


        offset = mula.transform.position.z - transform.position.z;
        instancia = this;

        terrenosLista = new ArrayList();
        terrenosLista.Add(terrenos[0]);
        terrenosLista.Add(terrenos[1]);
        terrenosLista.Add(terrenos[2]);
        terrenosLista.Add(terrenos[3]);

        distanciaTerrenos = Mathf.Abs(terrenos[0].transform.position.z - terrenos[1].transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position =new Vector3(transform.position.x,transform.position.y,mula.transform.position.z -offset);
	}

    public void GenerarSuelo()
    {
        GameObject pos = (GameObject)terrenosLista[3];

        GameObject t = (GameObject)Instantiate(terreno, pos.transform.position - (Vector3.forward * distanciaTerrenos), terreno.transform.rotation);

        GameObject aux = (GameObject)terrenosLista[0];

        terrenosLista.RemoveAt(0);
        Destroy(aux);
        terrenosLista.Add(t);
    }
}
