using UnityEngine;
using System.Collections;

public class ColisionesMula : MonoBehaviour
{
    public GameObject casco;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Obtaculo"))
        {
            Debug.Log("COlisioneObs");
            if (!casco.activeSelf)
            {
                
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;

            }


        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Destructores"))
        {

            ManejadorSuelo.instancia.GenerarSuelo(other.transform.parent.name,other.gameObject);
        }
    }

   
  

       


   
}
