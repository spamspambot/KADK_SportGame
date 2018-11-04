using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour {
    public GameObject pizza;
    public Transform spawnLocation1;
    public Transform spawnLocation2;
    public Transform spawnLocation3;
    public float spawnTime;
    public GameObject pizza1;
    public GameObject pizza2;
    public GameObject pizza3;
    public GameObject ufoSound;
    // Use this for initialization
    void Start () {
        StartCoroutine("SpawnPizza");
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator SpawnPizza() {
        Instantiate(ufoSound, transform.position, Quaternion.identity);
        if(pizza1 == null)      pizza1 =  Instantiate(pizza, spawnLocation1.position, Quaternion.Euler(90,0,0));
        else if(pizza2 == null) pizza2 = Instantiate(pizza, spawnLocation2.position, Quaternion.Euler(90, 0, 0));
        else if (pizza3 == null) pizza3 = Instantiate(pizza, spawnLocation3.position, Quaternion.Euler(90, 0, 0));
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine("SpawnPizza");
    }
}
