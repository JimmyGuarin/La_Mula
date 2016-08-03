using UnityEngine;
using System.Collections;

public class ManejadorSuelo : MonoBehaviour {


    public ArrayList terrenoActual;

    public GameObject[] terrenosRural;
    public ArrayList terrenosListaRural;


    public GameObject[] terrenosCiudad;
    public ArrayList terrenosListaCiudad;


    public GameObject[] terrenosOscuros;
    public ArrayList terrenosListaOscuro;


    public GameObject mula;
    
    private float offset;
    public static ManejadorSuelo instancia;

    private float distanciaTerrenos;

    [HideInInspector]
    public int terreno;
    public static readonly int TERRENO_RURAL = 0;
    public static readonly int TERRENO_URBANO = 1;
    public static readonly int TERRENO_OSCURO = 2;

    // Use this for initialization
    void Start () {


        offset = mula.transform.position.z - transform.position.z;
        instancia = this;

        terrenosListaRural = new ArrayList();
        terrenosListaCiudad = new ArrayList();
        terrenosListaOscuro = new ArrayList();

        LlenarArrayList(terrenosListaRural, terrenosRural);
        LlenarArrayList(terrenosListaCiudad, terrenosCiudad);
        LlenarArrayList(terrenosListaOscuro, terrenosOscuros);

        terrenoActual=terrenosListaRural;

        distanciaTerrenos = Mathf.Abs(terrenosRural[0].transform.position.z - terrenosRural[1].transform.position.z);
        terreno = TERRENO_RURAL;
        Invoke("CambiarTerreno", 10);
        Invoke("CambiarTerreno", 20);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position =new Vector3(transform.position.x,transform.position.y,mula.transform.position.z -offset);
	}

    public void GenerarSuelo(string nombre,GameObject terre)
    {
        if ((nombre.Equals("TerrenosRural") && terreno == TERRENO_RURAL) || (nombre.Equals("TerrenosCalle") && terreno == TERRENO_URBANO) ||
            (nombre.Equals("TerrenosOscuro") && terreno == TERRENO_OSCURO))
        {
            GameObject pos = (GameObject)terrenoActual[3];
            GameObject aux = (GameObject)terrenoActual[0];

            aux.transform.position = pos.transform.position - (Vector3.forward * distanciaTerrenos);
            aux.GetComponent<Collider>().enabled = true;
            Debug.Log("Entra");
            terrenoActual.RemoveAt(0);
            terrenoActual.Add(aux);
        }
        else
        {
            if (terreno == TERRENO_URBANO && nombre.Equals("TerrenosRural"))
                StartCoroutine(DeshabilitarTerreno(terrenosRural));
            else
            {
                if (terreno == TERRENO_OSCURO && nombre.Equals("TerrenosCalle"))
                    StartCoroutine(DeshabilitarTerreno(terrenosCiudad));
            }
        }



    }

    private void LlenarArrayList(ArrayList vacio, GameObject[] lleno)
    {
        for (int i = 0; i < lleno.Length; i++)
            vacio.Add(lleno[i]);
    }

    public void CambiarTerreno()
    {
        GameObject t;
        GameObject aux2 = (GameObject)terrenoActual[3];
        GameObject[] terrenoAux;

        if (terreno == TERRENO_RURAL)
        {
            terreno = TERRENO_URBANO;
            t = (GameObject)terrenosListaCiudad[0];
            terrenoAux = terrenosCiudad;
            terrenoActual = terrenosListaCiudad;

        }
        else
        {
            terreno = TERRENO_OSCURO;
            t = (GameObject)terrenosListaOscuro[0];
            terrenoAux = terrenosOscuros;
            terrenoActual = terrenosListaOscuro;

        }

        t.transform.position=aux2.transform.position - (Vector3.forward * distanciaTerrenos);

        for(int i = 1; i < terrenoAux.Length; i++)
        {
            terrenoAux[i].transform.position= terrenoAux[i-1].transform.position- (Vector3.forward * distanciaTerrenos);
        }

        t.transform.parent.gameObject.SetActive(true);
       

    }

    IEnumerator  DeshabilitarTerreno(GameObject [] terreno)
    {
        for(int i = 0; i < terreno.Length; i++)
        {
            if (terreno[i].activeSelf)
            {

                yield return new WaitForSeconds(Time.timeScale*2);
                terreno[i].SetActive(false);
                yield return null;
            }
                
        }
    }

    
}
