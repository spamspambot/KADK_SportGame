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
    public float cameraStrechSpeed;
    public float cameraHeight = 5;
    public float fieldOfViewMultiplier;
    // List<GameObject> players;
    GameObject[] players;
    public List<bool> onScreenList;

    // Use this for initialization

    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        
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

        if (Input.GetKeyDown("k")) { PlayerIndicator(); }

        if (playerMov.nitro)
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, 85, ref zeroFloat, cameraStrechSpeed);
        else cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, 70, ref zeroFloat, cameraStrechSpeed);
        currentX = target.rotation.eulerAngles.x;
        currentY = target.rotation.eulerAngles.y;
        //      distance = minDistance;
        if (playerMov.nitro)
            distance = Mathf.SmoothDamp(distance, maxDistance, ref zeroFloat, cameraZoomSpeed);
        else distance = Mathf.SmoothDamp(distance, minDistance, ref zeroFloat, cameraZoomSpeed/4);

        Vector3 dir = new Vector3(0, 0, distance);
        Quaternion rotation = Quaternion.Euler(currentY / 360, currentX / 360, 0);
        transform.position = target.position + target.forward * distance + target.up * cameraHeight;
        transform.LookAt(target.position);

    }

    void PlayerIndicator() {
     //   Vector3 screenPosition = cam.WorldToScreenPoint(players[0].transform.position);
   //     onScreenList[0] = screenPosition.x > 0 && screenPosition.x < 1 && screenPosition.y > 0 && screenPosition.y < 1;
    }
}
