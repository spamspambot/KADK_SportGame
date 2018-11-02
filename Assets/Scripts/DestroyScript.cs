using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {
    public float lifetime = 2;
	// Use this for initialization
	void Start () {
        StartCoroutine("Destroy", lifetime);
	}

    IEnumerator Destroy(float lifetime) {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
