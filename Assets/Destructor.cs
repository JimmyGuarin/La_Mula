using UnityEngine;
using System.Collections;

public class Destructor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider collision)
    {

        if(collision.tag.Equals("PuntoCaida") || collision.tag.Equals("Caso")|| collision.tag.Equals("Iman"))
        {
            collision.gameObject.SetActive(false);
        }
        else
        {
            if(collision.tag.Equals("Obtaculo"))
                 Destroy(collision.gameObject);
        }
        
    }
}
