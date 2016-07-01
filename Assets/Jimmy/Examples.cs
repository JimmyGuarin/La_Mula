using UnityEngine;
using System.Collections;

public class Examples : MonoBehaviour {

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Debug.Log("Dedo " + touch.fingerId);
            Debug.Log("Posicion +" + touch.position);
            Debug.Log("\nPosición respecto al ultimo frame" + touch.deltaPosition);

            Debug.Log("\nfase en la que se encuentra el dedo " + touch.phase);




        }
    }
}
