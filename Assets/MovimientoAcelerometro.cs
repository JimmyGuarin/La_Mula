using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoAcelerometro : MonoBehaviour
{

    public float speed = 10.0f;


    private Rigidbody rg;

    public void Start()
    {
        rg = GetComponent<Rigidbody>();
    }    

    void Update()
    {

        rg.AddForce(transform.forward * 3);


        Vector3 dir = Vector3.zero;

        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);
    }
}
