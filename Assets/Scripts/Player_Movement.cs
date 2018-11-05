using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Input inputManager;
    Rigidbody rb;

    public bool inverted;
    public GameObject pizzaContainer;
    public GameObject pizzaObject;
    public GameObject pizzaObject2;
    public GameObject pizzaObject3;
    public float stretchAmount = 8;
    public bool mov;
    public bool nitro;
    public int fuel;
    int currentFuel;
    public bool overheat;
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
    public float pizzaDelay;
    public float pizzaShootVelocity;
    public AudioSource thrustSound;
    public AudioSource nitroSource;
    public AudioClip nitroStart;
    public AudioClip nitroStop;
    public GameObject pizzaSound;
    public GameObject crashSound;
    public GameObject collideSound;
    bool thrust;
    Transform startTransform;
    Vector3 zeroVel;
    public List<GameObject> pizzaList;

    // Use this for initialization
    void Start()
    {
        inputManager = GetComponent<Player_Input>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        startTransform = transform;
        zeroVel = Vector3.zero;
        currentFuel = fuel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (inputManager.inputQueue.Count > 0)
        {
            if (inputManager.inputQueue[0] == 1) print("Square");
            else if (inputManager.inputQueue[0] == 2) { inverted = !inverted; print("Triangle"); }
            else if (inputManager.inputQueue[0] == 4) print("Circle");
            else if (inputManager.inputQueue[0] == 6 && pizzaObject != null) ShootPizza();
            inputManager.inputQueue.RemoveAt(0);
        }


        if (mov)
        {
            if (inputManager.crossHold)
            {
                if (!thrust) { thrustSound.Play(); }
                thrust = true;
                if (nitro) currentVelocity += nitroVelocity;
                else currentVelocity += velocity;
            }
            else { thrust = false; thrustSound.Stop(); }
            if (inputManager.circleHold) currentVelocity -= velocityReduction * 5;
            else if (currentVelocity > 0) currentVelocity -= velocityReduction;
            else if (currentVelocity < 0) currentVelocity += velocityReduction;
            else { currentVelocity = 0; thrust = false; }

            rb.velocity = transform.forward * currentVelocity;


            if (Mathf.Abs(inputManager.inputHorizontal) > 0 && currentVelocity != 0)
            {
                transform.Rotate(new Vector3(0, inputManager.inputHorizontal * turnVelocity, 0));
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -inputManager.inputHorizontal * 90), stability);
            }
            if (Mathf.Abs(inputManager.inputVertical) > 0 && currentVelocity != 0)
            {
                if (inverted) transform.Rotate(new Vector3(-inputManager.inputVertical * turnVelocity, 0, 0));
                else transform.Rotate(new Vector3(inputManager.inputVertical * turnVelocity, 0, 0));
            }

            if (Mathf.Abs(inputManager.inputHorizontal) < 0.5F && Mathf.Abs(inputManager.inputVertical) < 0.5f && inputManager.crossHold || !inputManager.crossHold)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), stability);
            }
        }
        if (inputManager.L1Hold && currentFuel > 0 && !overheat && inputManager.crossHold && mov && pizzaObject == null)
        {
            currentVelocity = Mathf.Clamp(currentVelocity, -nitroMaxVelocity, nitroMaxVelocity);
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(startTransform.localScale.x, startTransform.localScale.y, stretchAmount), ref zeroVel, stretchTime); currentFuel--; if (currentFuel <= 0) overheat = true; if (!nitro) { nitroSource.clip = nitroStart; nitroSource.Play(); }
            nitro = true;
        }
        else
        {
            currentVelocity = Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);
            transform.localScale = Vector3.SmoothDamp(transform.localScale, new Vector3(startTransform.localScale.x, startTransform.localScale.y, 1), ref zeroVel, stretchTime / 4);
            if (currentFuel == fuel) overheat = false;
            if (currentFuel < fuel) currentFuel++;
            if (nitro) { nitroSource.clip = nitroStop; nitroSource.Play(); }
            nitro = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Instantiate(collideSound, transform.position, Quaternion.identity);
        if (other.CompareTag("Pizza") && pizzaDelay + 2f < Time.time) GetPizza(other.gameObject);
        if (other.transform.CompareTag("Wall") && mov) { StartCoroutine("Crash"); print("crash"); }
        // StartCoroutine("Crash"); print("crash");
    }

    void GetPizza(GameObject pizza)
    {
        if (!pizzaList.Contains(pizza))
        {
            pizzaList.Add(pizza);
            if (pizzaObject == null) { pizzaObject = pizza; pizzaObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; }
            else if (pizzaObject2 == null) { pizzaObject2 = pizza; pizzaObject2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; }

            else if (pizzaObject == null) { pizzaObject3 = pizza; pizzaObject3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; }

            pizza.transform.position = pizzaContainer.transform.position;
            pizza.transform.rotation = transform.rotation;
            pizza.transform.SetParent(pizzaContainer.transform);
        }
 

      

    }

    void ShootPizza()
    {
        Instantiate(pizzaSound, transform.position, Quaternion.identity);
        print("Shoot");
        if (pizzaObject3 != null) { pizzaObject3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; pizzaObject3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            pizzaDelay = Time.time;
            pizzaObject3.GetComponent<Rigidbody>().isKinematic = false;

            pizzaObject3.transform.SetParent(null);
            pizzaObject3.GetComponent<Rigidbody>().velocity = transform.forward * (pizzaShootVelocity + currentVelocity);
            pizzaList.Remove(pizzaObject3);
            pizzaObject3 = null;

        }
        else if (pizzaObject2 != null) { pizzaObject2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; pizzaObject2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            pizzaDelay = Time.time;
            pizzaObject2.GetComponent<Rigidbody>().isKinematic = false;

            pizzaObject2.transform.SetParent(null);
            pizzaObject2.GetComponent<Rigidbody>().velocity = transform.forward * (pizzaShootVelocity + currentVelocity);
            pizzaList.Remove(pizzaObject2);
            pizzaObject2 = null;
        }

        else if (pizzaObject != null) { pizzaObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; pizzaObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            pizzaDelay = Time.time;
            pizzaObject.GetComponent<Rigidbody>().isKinematic = false;

            pizzaObject.transform.SetParent(null);
            pizzaObject.GetComponent<Rigidbody>().velocity = transform.forward * (pizzaShootVelocity + currentVelocity);
            pizzaList.Remove(pizzaObject);
            pizzaObject = null;
        }
    }

    IEnumerator Crash()
    {
        Instantiate(crashSound, transform.position, Quaternion.identity);
        print("crash");
        mov = false;
        yield return new WaitForSeconds(crashTime);
        mov = true;
        currentVelocity = 0;
    }
}
