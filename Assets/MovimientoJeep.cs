using UnityEngine;
using System.Collections;

public class MovimientoJeep : MonoBehaviour
{

    public WheelCollider FR;
    public WheelCollider FL;
    public WheelCollider BR;
    public WheelCollider BL;

    public Transform FRM;
    public Transform FLM;
    public Transform BRM;
    public Transform BLM;

    public GameObject proyectil;
    public GameObject generador;

    //------------------------------
    public float xMax;




    public GameObject mula;
    private Vector3 offset;

    float maxTorque = 0f;


    // Use this for initialization
    void Start()
    {

        GetComponent<Rigidbody>().centerOfMass =  new Vector3(GetComponent<Rigidbody>().centerOfMass.x,0, GetComponent<Rigidbody>().centerOfMass.z);
        BR.motorTorque = maxTorque;

        offset = transform.position - mula.transform.position;



    }
    public void Update()
    {
        transform.position=new Vector3(transform.position.x,transform.position.y,(mula.transform.position.z+offset.z));

        if (Input.GetButtonDown("Jump"))
        {
            FireRocket();
        }

    }


    void FireRocket()
    {
        Rigidbody disparo = (Rigidbody)Instantiate(proyectil.GetComponent<Rigidbody>(), generador.transform.position, generador.transform.rotation);
        //disparo.velocity = generador.transform.forward * speed;
        disparo.AddForce(new Vector3(0, 2, 4) * 180);

        
    }

    public void calcularVelocidad(float xmax)
    {

    }



    public void FixedUpdate()
    {
        BR.motorTorque = maxTorque;
        BL.motorTorque = maxTorque;


        FRM.Rotate(new Vector3(FR.rpm * -4 * Time.deltaTime, 0, 0));
        FLM.Rotate(new Vector3(FL.rpm * -4 * Time.deltaTime, 0, 0));
        BRM.Rotate(new Vector3(BR.rpm * -4 * Time.deltaTime, 0, 0));
        BLM.Rotate(new Vector3(BL.rpm * -4 * Time.deltaTime, 0, 0));
    }


}