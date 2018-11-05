using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaScript : MonoBehaviour {
    public bool idle;
    public float turnSpeed;
    public float spoilTime = 15F;
    public GameObject pineapple;
    public GameObject hawaiiSound;
    public bool spoiled;
	// Use this for initialization
	void Start () {
        StartCoroutine("HawaiiPizza");		
	}
	
	// Update is called once per frame
	void Update () {
        if (idle) { transform.Rotate(0, 0, turnSpeed); }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            idle = false;
        }
        else idle = true;
    }

    IEnumerator HawaiiPizza()
    {
        yield return new WaitForSeconds(spoilTime);
        spoiled = true;
        Instantiate(hawaiiSound, transform.position, Quaternion.identity);
        pineapple.SetActive(true);
    }
}
