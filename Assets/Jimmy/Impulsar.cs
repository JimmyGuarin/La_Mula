using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Impulsar : MonoBehaviour
{

    public GameObject canastoIzquierdo;
    public GameObject canastoDerecho;
    public GameObject Iman;
    public float mitadTiempoVuelo;
    public bool dorado;


    private Vector3 objetivo;
    private Rigidbody rg;
    private bool EnBajada;

    // Use this for initialization
    void Start()
    {
        EnBajada = false;
        rg = GetComponent<Rigidbody>();
        MovimientoAcelerometro mBurra = GameObject.Find("Burra").GetComponent<MovimientoAcelerometro>();
        canastoIzquierdo = mBurra.refCanastoIzquierdo;
        canastoDerecho = mBurra.refCanastoDerecho;
        Iman = mBurra.iman;        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().isKinematic)
        {
            AtraccionIman();
            return;
        }

        if (EnBajada && Iman.activeSelf && !rg.isKinematic)
        {
            rg.isKinematic = true;
            return;
        }

    }



    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Respawn"))
        {

            if (!dorado)
                HUD1.instancia.Encholar();
            else
                HUD1.instancia.EncholarDorado();


            gameObject.SetActive(false);
            Debug.Log("EntraDesactiva");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Suelo"))
        {
            HUD1.instancia.Perdida();
            gameObject.SetActive(false);

        }
    }

    public void OnDisable()
    {
        CancelInvoke();
        rg.isKinematic = false;
    }

    public void OnEnable()
    {
        Invoke("MitadTiempoVuelo", mitadTiempoVuelo);
       
    }

    public void AtraccionIman()
    {
        float a = Vector3.Distance(transform.position, canastoIzquierdo.transform.position);
        float b = Vector3.Distance(transform.position, canastoDerecho.transform.position);
        if (a < b)
            objetivo = canastoIzquierdo.transform.position;
        else
            objetivo = canastoDerecho.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, objetivo, 100 * Time.deltaTime);
        if (Mathf.Abs(transform.position.z - objetivo.z) < 2)
        {

            if (!dorado)
                HUD1.instancia.Encholar();
            else
                HUD1.instancia.EncholarDorado();

            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.SetActive(false);
        }
    }

    public void MitadTiempoVuelo()
    {
        if (Iman.activeSelf)
        {
            rg.isKinematic = true;
        }
        EnBajada = true;

    }
}
