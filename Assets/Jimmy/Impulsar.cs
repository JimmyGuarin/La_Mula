using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Impulsar : MonoBehaviour {


    Rigidbody rg;

	// Use this for initialization
	void Start () {

        rg = GetComponent<Rigidbody>();

        rg.AddForce(new Vector3(0, 2, -4)*180);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
