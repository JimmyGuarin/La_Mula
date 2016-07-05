using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Impulsar : MonoBehaviour
{


    Rigidbody rg;





    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {


    }

   

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Respawn"))
        {
            Debug.Log("Entra");
            HUD1.instancia.Encholar();
            Destroy(this.gameObject);
        }
    }
}
