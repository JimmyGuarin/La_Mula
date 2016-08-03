using UnityEngine;
using System.Collections;

public class Lanzador : MonoBehaviour {


    


    public Transform[] bases;

    public Rigidbody burra;
    public GameObject[] barrilesComun;
    public GameObject barrilDorado;
    public GameObject[] dinamitas;
    public GameObject[] puntosCaida;
  

    //Parametros de Calculo de Tiro Parabolico
    public int altura = 10;
    public float distancia;


    public int lanzados;

    bool dinamita;
    bool chocado;
    Vector3 velocidad;
    float g;
    int indiceCaida;
    int indiceBarrilComun;
    int indiceDimamita;


    // Use this for initialization
    void Start () {

        
       // transform.position = new Vector3(transform.position.x, bases[3].position.y, transform.position.z);
        InvokeRepeating("disparar", 1, 2f);
        lanzados = 0;
        dinamita = Random.value > 0.5f;
        g = Physics.gravity.magnitude;
        indiceCaida = 0;
        indiceBarrilComun = 0;
        indiceDimamita = 0;
    }

    // Update is called once per frame
    void Update () {

	}

    void disparar()
    {
        int indice = Random.Range(0, bases.Length);
             
        float vertSpeed = Mathf.Sqrt(2 * g * altura);
        // calculate the total time var 
        float totalTime = 2 * vertSpeed / g;


        Vector3 Adelante = bases[indice].position + new Vector3(0, 0, -( burra.velocity.magnitude*totalTime));
        Vector3 puntoCae = new Vector3(Adelante.x, puntosCaida[0].transform.position.y, Adelante.z);
        Collider [] co=Physics.OverlapBox(puntoCae, new Vector3(5, 10, 7));

        bool colisiona = false;
        foreach(Collider c in co)
        {
            if (c.gameObject.tag.Equals("Obtaculo"))
            {
                colisiona = true;
                break;
            }
        }

        transform.LookAt(Adelante);
        float distancia = Mathf.Abs((Adelante - transform.position).magnitude);
        // calculate the horizontal speed 
        float horSpeed = distancia / totalTime;
                
        velocidad = new Vector3(0, vertSpeed, horSpeed);
        bool bonusDorado = Random.value < 0.05f;
        dinamita = Random.value > 0.6f;


        if(colisiona==false)
        {
            EstablecerPuntoCaida(puntoCae);
       

            if (dinamita)
            {                
                Lanzar(dinamitas[indiceDimamita], transform.position);
                EstablecerIndiceArray(ref indiceDimamita);
            }
            else if(bonusDorado)
            {
                barrilDorado.GetComponent<Impulsar>().mitadTiempoVuelo = totalTime / 2;
                Lanzar(barrilDorado, transform.position);
            }
            else
            {
                barrilesComun[indiceBarrilComun].GetComponent<Impulsar>().mitadTiempoVuelo = totalTime / 2;
                Lanzar(barrilesComun[indiceBarrilComun],transform.position);
                EstablecerIndiceArray(ref indiceBarrilComun);
            }
        }

        lanzados++;
        if (lanzados % 5 == 0 && burra.velocity.magnitude<200)
        {
            StartCoroutine(cambiarVelocidad(totalTime+1));           
        }

    }

    IEnumerator cambiarVelocidad(float time)
    {
        CancelInvoke("disparar");
        yield return new WaitForSeconds(time+Time.deltaTime);
        //burra.GetComponent<MovimientoAcelerometro>().speed += 10;
        //altura +=1;
        HUD1.instancia.CambioVelocidad();
        InvokeRepeating("disparar",2, 2);
    }

    
    public void Lanzar(GameObject objeto,Vector3 posicion)
    {
        objeto.transform.position = posicion;
        objeto.SetActive(true);
        objeto.GetComponent<Rigidbody>().velocity = transform.TransformDirection(velocidad);// launch the projectile! 
    }

    public void EstablecerPuntoCaida(Vector3 posicion)
    {       
       puntosCaida[indiceCaida].SetActive(true);
       puntosCaida[indiceCaida].transform.position = posicion;
        EstablecerIndiceArray(ref indiceCaida);
    }

    public void EstablecerIndiceArray(ref int ind)
    {
        if (ind == 0)
            ind++;
        else
            ind--;
    }

}
