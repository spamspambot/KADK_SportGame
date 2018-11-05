using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public int ID;
    public Text scoreText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ID == 1)
            scoreText.text = ""+ManagerScript.team1Score;
        else if (ID == 2)
            scoreText.text = ""+ManagerScript.team2Score;
    }
}
