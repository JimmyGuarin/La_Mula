using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoAcelerometro : MonoBehaviour
{

    public float speed = 0f;
    public float traslacion = 20.0f;

    private Rigidbody rg;

    public Text VELOCIDAD;
    public Text fps;


    float deltaTime = 0.0f;
    float fps1 = 0.0f;


    public int carriActual;
    public Vector3 cambioCarril;

    private bool moviendo;




    public void Start()
    {
        rg = GetComponent<Rigidbody>();


        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
        carriActual = 3;


    }


    void Update()
    {

        // transform.Translate(Vector3.forward * speed * Time.deltaTime);




        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 pos = touch.deltaPosition;

                if ((Mathf.Abs(pos.x) > Mathf.Abs(pos.y)))
                {
                    if (pos.x > 0)
                    {

                        if (carriActual > 1)
                        {
                            transform.Translate(new Vector3(15, 0, 0));
                            carriActual--;
                        }


                    }
                    if (pos.x < 0)
                    {
                        if (carriActual < 5)
                        {
                            transform.Translate(new Vector3(-15, 0, 0));
                            carriActual++;
                        }
                    }

                    Debug.Log(carriActual);

                }
                if ((Mathf.Abs(pos.y) > Mathf.Abs(pos.x)))
                {
                    if (pos.y > 0)
                    {
                        rg.AddForce(Vector3.forward + Vector3.up);
                        Debug.Log("Entra");
                    }
                }

            }
        }




    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("GameController"))
        {
            ManejadorSuelo.instancia.GenerarSuelo();
        }
    }
}
