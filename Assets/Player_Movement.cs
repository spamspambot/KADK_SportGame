using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Input inputManager;
    Rigidbody rb;
    public bool mov;
    public float actualVelocity;
    public float currentVelocity;
    public float velocity;
    public float maxVelocity;
    public float velocityReduction;
    public float brakeVelocity;
    public float turnVelocity;
    public float stability;
    public float crashTime;
    // Use this for initialization
    void Start()
    {
        inputManager = GetComponent<Player_Input>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
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
            if (Input.GetButton("XB_CROSS")) currentVelocity += velocity;
            else if (Input.GetButton("XB_CIRCLE")) currentVelocity = currentVelocity * 0.9F;
            else if (currentVelocity > 0) currentVelocity -= velocityReduction;
            else if (currentVelocity < 0) currentVelocity += velocityReduction;
            else currentVelocity = 0;
            currentVelocity = Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);
            rb.velocity = transform.forward * currentVelocity;
        //    rb.AddForce(transform.forward * currentVelocity);
        //    rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity), Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity));

            if (Mathf.Abs(inputManager.inputHorizontal) > 0) transform.Rotate(new Vector3(0, inputManager.inputHorizontal * turnVelocity * (Mathf.Abs(currentVelocity) / maxVelocity), 0));
            if (Mathf.Abs(inputManager.inputVertical) > 0) transform.Rotate(new Vector3(inputManager.inputVertical * turnVelocity * (Mathf.Abs(currentVelocity) / maxVelocity), 0, 0));
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

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

    void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Wall") && mov) { StartCoroutine("Crash"); print("crash"); }
        }

    IEnumerator Crash() {
        print("crash");
        mov = false;
        yield return new WaitForSeconds(crashTime);
        mov = true;
    }
}
