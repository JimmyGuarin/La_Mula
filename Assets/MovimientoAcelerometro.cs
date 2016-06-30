using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoAcelerometro : MonoBehaviour
{

    public float speed = 10.0f;
    public float traslacion = 20.0f;

    private Rigidbody rg;

    public Text VELOCIDAD;
    public Text fps;


    float deltaTime = 0.0f;
    float fps1 = 0.0f;

    public Vector3 carril1;
    public Vector3 carril2;
    public Vector3 carril3;
    public Vector3 carriActual;

    private bool moviendo;

    public void Start()
    {
        rg = GetComponent<Rigidbody>();
        carril1 = transform.position + new Vector3(-10, 0, 0);
        carril3 = transform.position;
        carriActual = carril3;
        carril2 = transform.position + new Vector3(-5, 0, 0);
        moviendo = false;
    }

    void Update()
    {

        rg.AddForce(transform.forward * speed);




        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fps1 = 1.0f / deltaTime;

        Vector3 dir = Vector3.zero;
        VELOCIDAD.text = rg.velocity.y + " KM/H";
        fps.text = "" + ((int)fps1);


        transform.Translate(new Vector3(0, 0, 10) * Time.deltaTime);
    }
}
