using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public GameObject pointSound;
    public GameObject yuckSound;
    public int ID;
    int points;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("Bama");
        if (other.CompareTag("Pizza"))
        {
            if (other.GetComponent<PizzaScript>().spoiled)
            {
                if (ID == 1)
                    ManagerScript.team1Score--;
                else if (ID == 2) ManagerScript.team2Score--;

                Instantiate(yuckSound, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
            else
            {
                if (ID == 1)
                    ManagerScript.team1Score++;
                else if (ID == 2) ManagerScript.team2Score++;

                Instantiate(pointSound, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }

        }

    }
}
