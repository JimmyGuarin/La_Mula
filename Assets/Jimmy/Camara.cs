using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {


    public GameObject Burra;

    public bool mover;

    public Vector3 carrilAMover;
    public Vector3 carrilActual;
    public float offset;
    public int velocidadCamara;
    
    // Use this for initialization
	void Start () {

        offset = Burra.transform.position.z - transform.position.z;
        carrilActual = transform.position;
        //Camera.main.GetComponent<Camara>().Mover(new Vector3(10, 0, 0));
    }

    // Update is called once per frame
    void Update () {

        if (mover)
        {
            float speed = velocidadCamara * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position,new Vector3(carrilAMover.x,transform.position.y,transform.position.z), speed);
            if (transform.position.x == carrilAMover.x)
            {

                mover = false;
                carrilActual = transform.position;
            }
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, Burra.transform.position.z-offset);
        

	}


    public void Mover(Vector3 objetivo)
    {
            carrilAMover =carrilActual+objetivo;

            mover = true;

    }
}
