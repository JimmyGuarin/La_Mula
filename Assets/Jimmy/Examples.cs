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


            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 pos = touch.deltaPosition;

                if ( (Mathf.Abs(pos.x)>Mathf.Abs(pos.y)))
                {
                    if (pos.x > 0)
                    {
                        transform.Translate(new Vector3(5, 0, 0));
                    }
                    if (pos.x < 0)
                    {
                        transform.Translate(new Vector3(-5, 0, 0));
                    }
                    


                }
                if ((Mathf.Abs(pos.y) > Mathf.Abs(pos.x)))
                {
                    if (pos.y > 0)
                    {
                        transform.Translate(new Vector3(0, 1, 2));
                        Debug.Log("Entra");
                    }
                    if (pos.y < 0)
                    {
                        
                    }



                }
            }


        }
    }
}
