﻿using UnityEngine;
using System.Collections;

public class ActivarExplosión : MonoBehaviour
{

    public GameObject explosion;
    // Use this for initialization
    void Start()
    {

    }

    

    public void OnCollisionEnter(Collision collision)
    {       
           Instantiate(explosion, transform.position, transform.rotation);

        if (collision.gameObject.tag.Equals("Player"))
        {
            MovimientoAcelerometro mBurra = GameObject.Find("Burra").GetComponent<MovimientoAcelerometro>();
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
