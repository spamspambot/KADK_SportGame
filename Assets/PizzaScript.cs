using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaScript : MonoBehaviour {
    public bool idle;
    public float turnSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (idle) { transform.Rotate(0, 0, turnSpeed); }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { transform.rotation = Quaternion.identity;idle = false; }
    }
}
