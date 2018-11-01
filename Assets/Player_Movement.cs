using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Input inputManager;
    Rigidbody rb;
    public bool mov;
    public bool nitro;
    public float actualVelocity;
    public float currentVelocity;
    public float velocity;
    public float maxVelocity;
    public float nitroVelocity;
    public float nitroMaxVelocity;
    public float velocityReduction;
    public float brakeVelocity;
    public float turnVelocity;
    public float stability;
    public float crashTime;
    public float stretchTime;
    Transform startTransform;
    Vector3 zeroVel;
    // Use this for initialization
    void Start()
    {
        inputManager = GetComponent<Player_Input>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        startTransform = transform;
        zeroVel = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //if (Input.GetButton("XB_CROSS")) rb.velocity = rb.velocity + (transform.forward * velocity);
        if (mov)
        {
            if (Input.GetButton("XB_R1")) nitro = true;
            else nitro = false;
            if (Input.GetButton("XB_CROSS"))
            {
                if (nitro) currentVelocity += nitroVelocity;
                else currentVelocity += velocity;
            }
            else if (Input.GetButton("XB_CIRCLE")) currentVelocity -= velocityReduction * 5;
            else if (currentVelocity > 0) currentVelocity -= velocityReduction;
            else if (currentVelocity < 0) currentVelocity += velocityReduction;
            else currentVelocity = 0;
            if (nitro && Input.GetButton("XB_CROSS")) { currentVelocity = Mathf.Clamp(currentVelocity, -nitroMaxVelocity, nitroMaxVelocity);
                transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(startTransform.localScale.x, startTransform.localScale.y,8), ref zeroVel, stretchTime); }
            else { currentVelocity = Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);
                transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(startTransform.localScale.x, startTransform.localScale.y,1 ), ref zeroVel, stretchTime/4); }
            rb.velocity = transform.forward * currentVelocity;


            // if (Input.GetButton("XB_CROSS")) rb.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
            // rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
            // if (Mathf.Abs(inputManager.inputHorizontal) > 0) rb.AddForceAtPosition(inputManager.inputHorizontal * turnVelocity * transform.right, transform.position, ForceMode.Acceleration);
            // if (Mathf.Abs(inputManager.inputVertical) > 0) rb.AddForceAtPosition(-inputManager.inputVertical * turnVelocity * transform.up, transform.position, ForceMode.Acceleration);
            //  if (Mathf.Abs(inputManager.inputVertical) > 0) transform.Rotate(new Vector3(inputManager.inputVertical * turnVelocity , 0, 0));


            //if (Mathf.Abs(inputManager.inputHorizontal) > 0) transform.Rotate(new Vector3(0, inputManager.inputHorizontal * turnVelocity * (Mathf.Abs(currentVelocity) / maxVelocity), 0));
            //if (Mathf.Abs(inputManager.inputVertical) > 0) transform.Rotate(new Vector3(inputManager.inputVertical * turnVelocity * (Mathf.Abs(currentVelocity) / maxVelocity), 0, 0));
            if (Mathf.Abs(inputManager.inputHorizontal) > 0 && currentVelocity > 0)
            {
                transform.Rotate(new Vector3(0, inputManager.inputHorizontal * turnVelocity, 0));   
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -inputManager.inputHorizontal * 90), stability);
            }
            if (Mathf.Abs(inputManager.inputVertical) > 0 && currentVelocity > 0) { transform.Rotate(new Vector3(inputManager.inputVertical * turnVelocity, 0, 0)); }


            //   transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
            //Return to normal
            if (Mathf.Abs(inputManager.inputHorizontal) < 0.5F && Mathf.Abs(inputManager.inputVertical) < 0.5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), stability);
            }
        }



        //    if (Input.GetButton("XB_CIRCLE")) rb.AddForce(new Vector3(0, 0, brakeVelocity));
        //    if (Input.GetButton("XB_SQUARE")) rb.AddTorque(new Vector3(0, turnVelocity, 0));


        //  print(transform.rotation.eulerAngles);
        //        if (transform.rotation.eulerAngles.y > 2) transform.Rotate(-stability, 0, 0);
        //      else transform.rotation = Quaternion.identity;
        //   else if (transform.rotation.eulerAngles.y < 0) transform.Rotate(stability, 0, 0);
        //    rb.AddForce(new Vector3(inputManager.inputHorizontal*velocity, 0, inputManager.inputVertical * velocity));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pizza"))
        if (other.transform.CompareTag("Wall") && mov) { StartCoroutine("Crash"); print("crash"); }
        StartCoroutine("Crash"); print("crash");
    }

    IEnumerator Crash()
    {
        print("crash");
        mov = false;
        yield return new WaitForSeconds(crashTime);
        mov = true;
        currentVelocity = 0;
    }
}
