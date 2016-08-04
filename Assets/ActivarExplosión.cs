using UnityEngine;
using System.Collections;

public class ActivarExplosión : MonoBehaviour
{

    public GameObject explosion;
    public MovimientoAcelerometro mBurra;
    // Use this for initialization
    void Start()
    {
        mBurra = GameObject.Find("Burra").GetComponent<MovimientoAcelerometro>();
    }

    

    public void OnCollisionEnter(Collision collision)
    {       
           Instantiate(explosion, transform.position, transform.rotation);

        if (collision.gameObject==mBurra.gameObject)
        {
            if (!mBurra.casco.activeSelf)
                mBurra.Destruida();
            else
                mBurra.QuitarCasco();
        }
        else
        {
            HUD1.instancia.dinamitasEsquivadas++;
        }

        gameObject.SetActive(false);            
    }

    // Update is called once per frame
    void Update()
    {

    }
}
