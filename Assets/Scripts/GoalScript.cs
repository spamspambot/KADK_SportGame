using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {
    int points;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("Bama");
        if (other.CompareTag("Pizza")) { points += 1;
            print("Obama");
            Destroy(other.gameObject);
        }

    }
}
