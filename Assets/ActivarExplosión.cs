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
          
           Destroy(gameObject);
                
    }

    // Update is called once per frame
    void Update()
    {

    }
}
