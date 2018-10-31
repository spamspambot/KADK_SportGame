﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;
    Player_Movement playerMov;
    Quaternion startRotation;
    Vector3 difference;
    public float currentX;
    public float currentY;
    public float distance;
    public float minDistance;
    public float maxDistance;
	// Use this for initialization
	void Start () {
        playerMov = target.GetComponent<Player_Movement>();
        difference = transform.position - target.transform.position;
        startRotation = transform.rotation;
     
	}
	
	// Update is called once per frame
	void Update () {
        currentX = target.rotation.eulerAngles.x;
        currentY = target.rotation.eulerAngles.y;

        distance = -Mathf.Abs((maxDistance-minDistance) * playerMov.currentVelocity / playerMov.maxVelocity + minDistance);
        print(playerMov.currentVelocity);
        Vector3 dir = new Vector3(0, 0, distance);
        Quaternion rotation = Quaternion.Euler(currentY/360, currentX/360, 0);
        transform.position = target.position + target.forward*distance + target.up * 5 ;
        transform.LookAt(target.position);
	}
}
