using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Impulsar : MonoBehaviour
{

    public GameObject canastoIzquierdo;
    public GameObject canastoDerecho;
    public GameObject Iman;
    public float mitadTiempoVuelo;

    private Vector3 objetivo;
    private Rigidbody rg;
    private bool EnBajada;

    // Use this for initialization
    void Start()
    {
        EnBajada = false;
        rg = GetComponent<Rigidbody>();
        Invoke("MitadTiempoVuelo",mitadTiempoVuelo);

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
            HUD1.instancia.Encholar();
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Suelo"))
        {
            HUD1.instancia.Perdida();
        }
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
        if (Mathf.Abs(transform.position.z-objetivo.z)<2)
        {
            HUD1.instancia.Encholar();
            GetComponent<Rigidbody>().isKinematic = false;

            Destroy(this.gameObject);

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
