using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public GameObject pizza;
    Player_Input inputManager;
	// Use this for initialization
	void Start () {
        inputManager = GetComponent<Player_Input>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("XB_SQUARE")) Instantiate(pizza, transform.position, transform.rotation);
	}
}
