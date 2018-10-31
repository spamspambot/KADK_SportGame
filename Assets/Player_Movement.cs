using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    Player_Input inputManager;
    Rigidbody rb;
    public float velocity;
    public float brakeVelocity;
    public float turnVelocity;
    // Use this for initialization
    void Start () {
        inputManager = GetComponent<Player_Input>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate() {
        if (Input.GetButton("XB_CROSS")) rb.AddForce(transform.forward * velocity);
        
        if (Input.GetButton("XB_CIRCLE")) rb.AddForce(new Vector3(0, 0, brakeVelocity));
        if (Input.GetButton("XB_SQUARE")) rb.AddTorque(new Vector3(0, turnVelocity, 0));
        //rb.AddTorque(new Vector3(0, inputManager.inputHorizontal, 0));
        transform.Rotate(new Vector3(0, inputManager.inputHorizontal, 0));
        //    rb.AddForce(new Vector3(inputManager.inputHorizontal*velocity, 0, inputManager.inputVertical * velocity));
    }
}
