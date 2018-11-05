using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool player4;
    public int cameraID;
    public Transform target;
    Player_Movement playerMov;
    Quaternion startRotation;
    Vector3 difference;
    public float currentX;
    public float currentY;
    public float distance;
    public float minDistance;
    public float maxDistance;
    Camera cam;
    float zeroFloat = 0;
    public float cameraZoomSpeed;
    public float cameraHeight = 5;
    public float fieldOfViewMultiplier;
    // Use this for initialization

    void Awake()
    {

        cam = GetComponent<Camera>();
        if (player4)
        {
            if (cameraID == 1)
                cam.rect = new Rect(0, 0, 0.5F, 0.5F);
            else if (cameraID == 2) cam.rect = new Rect(0.5F, 0, 0.5F, 0.5F);
            else if (cameraID == 3) cam.rect = new Rect(0, 0.5F, 0.5F, 0.5F);
            else if (cameraID == 4) cam.rect = new Rect(0.5F, 0.5F, 0.5F, 0.5F);
        }
        else
        {
            if (cameraID == 1)
                cam.rect = new Rect(0, 0, 0.5F, 1F);
            else if (cameraID == 2) cam.rect = new Rect(0.5F, 0, 0.5F, 1F);
        }     
    }

    void Start()
    {
        playerMov = target.GetComponent<Player_Movement>();
        difference = transform.position - target.transform.position;
        startRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (playerMov.nitro)
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, 85, ref zeroFloat, cameraZoomSpeed);
       else cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, 70, ref zeroFloat, cameraZoomSpeed);
        currentX = target.rotation.eulerAngles.x;
        currentY = target.rotation.eulerAngles.y;
        distance = minDistance;
    //    distance = Mathf.SmoothDamp(distance, ((maxDistance - minDistance)  + minDistance), ref zeroFloat, cameraZoomSpeed);

        Vector3 dir = new Vector3(0, 0, distance);
        Quaternion rotation = Quaternion.Euler(currentY / 360, currentX / 360, 0);
        transform.position = target.position + target.forward * distance + target.up * cameraHeight;
        transform.LookAt(target.position);

    }
}
