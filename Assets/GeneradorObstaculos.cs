using UnityEngine;
using System.Collections;

public class GeneradorObstaculos : MonoBehaviour
{

    public GameObject[] obstaculosCalle;
    public GameObject[] obstaculosRural;

    public GameObject[] bases;
    public GameObject[] basesE;

    public float tiempoGeneracion = 3;
    public Rigidbody burra;
    public GameObject[] bonus;

    bool segundoObs;
    bool bonusB =false;

    // Use this for initialization
    void Start()
    {
        
        InvokeRepeating("crearObstaculosE", 1,tiempoGeneracion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void crearObstaculosE()
    {
        segundoObs = Random.value > 0.6f;
        bonusB = Random.value > 0.3f;

        int indice = Random.Range(0, basesE.Length);
        int indice2 = Random.Range(0, basesE.Length);
                      
        if(bonusB && !hayBonus() )
        {
            VerificarBunusActivo(indice2);

        }
        else
        {
            InstanciarObstaculo(indice,indice);
        }
        


        if(segundoObs)
        {
            while (indice == indice2)
            {
                indice2 = Random.Range(0, basesE.Length);
            }


            if (bonusB && !hayBonus())
            { 
               
                VerificarBunusActivo(indice2);
            }
            else
            {
                InstanciarObstaculo(indice2,indice);
            }
            
        }

    }

    bool hayBonus()
    {
        GameObject casco = GameObject.Find("casco");
        GameObject iman = GameObject.Find("Iman");

        if (!casco && !iman)
            return false;
        else
            return true;
    }

    
    void GenerarBonus(GameObject bonus,Vector3 posicionBase)
    {
        bonus.transform.position = posicionBase+Vector3.up * 10;
        bonus.SetActive(true);
    }

    void VerificarBunusActivo(int indice)
    {
        if (!burra.GetComponent<MovimientoAcelerometro>().casco.activeSelf)
        {
            GenerarBonus(bonus[0], basesE[indice].transform.position);
        }
        else if (!burra.GetComponent<MovimientoAcelerometro>().iman.activeSelf)
        {
            GenerarBonus(bonus[1], basesE[indice].transform.position);
        }
        else if (!burra.GetComponent<MovimientoAcelerometro>().iman.activeSelf && !burra.GetComponent<MovimientoAcelerometro>().casco.activeSelf)
        {
            GenerarBonus(bonus[Random.Range(0, bonus.Length)], basesE[indice].transform.position);
        }
    }




    void InstanciarObstaculo(int posicion,int indice)
    {
        GameObject obstaculo;

        if (ManejadorSuelo.instancia.terreno == ManejadorSuelo.TERRENO_RURAL|| ManejadorSuelo.instancia.terreno == ManejadorSuelo.TERRENO_OSCURO)
        {
            obstaculo = obstaculosRural[Random.Range(0, obstaculosRural.Length)];
        }
        else
        {
            obstaculo = obstaculosCalle[Random.Range(0, obstaculosCalle.Length)];
        }

        Instantiate(obstaculo, basesE[posicion].transform.position, obstaculo.transform.rotation);
    }


}
