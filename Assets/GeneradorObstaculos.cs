using UnityEngine;
using System.Collections;

public class GeneradorObstaculos : MonoBehaviour
{

    public GameObject[] obstaculos;
    public GameObject[] obstaculosE;

    public GameObject[] bases;
    public GameObject[] basesE;


    public float tiempoGeneracion = 3;
    private int altura = 3;
    public Rigidbody burra;
    public GameObject[] bonus;

    bool segundoObs;
    bool bonusB =false;

    // Use this for initialization
    void Start()
    {
        
        //InvokeRepeating("crearObstaculos", 1, 4);
        InvokeRepeating("crearObstaculosE", 1,tiempoGeneracion);
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    void crearObstaculos()
    {
        
        GameObject obstaculo = obstaculos[Random.Range(0, obstaculos.Length)];
        int indice = Random.Range(0, bases.Length);

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
        
   
      // Instantiate(obstaculo, bases[].transform.position, transform.rotation);

    }

    void crearObstaculosE()
    {
        segundoObs = Random.value > 0.6f;
        bonusB = Random.value > 0.3f;

        

        int indice = Random.Range(0, basesE.Length);

        int indice2 = Random.Range(0, basesE.Length);
        
               
        if(bonusB && !hayBonus() )
        {
            if (!burra.GetComponent<MovimientoAcelerometro>().casco.activeSelf)
            {
                Instantiate(bonus[0], basesE[indice].transform.position + Vector3.up * 10, basesE[indice].transform.rotation);
            }
            else if(!burra.GetComponent<MovimientoAcelerometro>().iman.activeSelf)
            {
                Instantiate(bonus[1], basesE[indice].transform.position + Vector3.up * 10, basesE[indice].transform.rotation);
            }
            else if(!burra.GetComponent<MovimientoAcelerometro>().iman.activeSelf&& !burra.GetComponent<MovimientoAcelerometro>().casco.activeSelf)
            {
                Instantiate(bonus[Random.Range(0, bonus.Length)], basesE[indice].transform.position + Vector3.up * 10, basesE[indice].transform.rotation);
            }

        }
        else
        {
            GameObject obstaculo = obstaculosE[Random.Range(0, obstaculosE.Length)];
            Instantiate(obstaculo, basesE[indice].transform.position, basesE[indice].transform.rotation);
        }
        


        if(segundoObs)
        {
           
            if (bonusB && !hayBonus())
            { 

                while (indice == indice2)
                {
                    indice2 = Random.Range(0, basesE.Length);
                }
                if(!burra.GetComponent<MovimientoAcelerometro>().casco.activeSelf)
                {
                    Instantiate(bonus[0], basesE[indice2].transform.position + Vector3.up * 10, basesE[indice2].transform.rotation);
                }
                else if(!burra.GetComponent<MovimientoAcelerometro>().iman.activeSelf)
                {
                    Instantiate(bonus[1], basesE[indice2].transform.position + Vector3.up * 10, basesE[indice2].transform.rotation);
                }
                else if (!burra.GetComponent<MovimientoAcelerometro>().iman.activeSelf && !burra.GetComponent<MovimientoAcelerometro>().casco.activeSelf)
                {
                    Instantiate(bonus[Random.Range(0,bonus.Length)], basesE[indice2].transform.position + Vector3.up * 10, basesE[indice2].transform.rotation);
                }
                
            }
            else
            {
                while (indice == indice2)
                {
                    indice2 = Random.Range(0, basesE.Length);
                }
                GameObject obstaculo = obstaculosE[Random.Range(0, obstaculosE.Length)];
                Instantiate(obstaculo, basesE[indice2].transform.position, basesE[indice].transform.rotation);
            }
            
        }

    }

    bool hayBonus()
    {
        GameObject casco = GameObject.Find("casco(Clone)");
        GameObject iman = GameObject.Find("Iman(Clone)");

        if (!casco && !iman)
            return false;
        else
            return true;
    }

    
    

    
}
