using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public int playerID;

    public float bufferWindow;
    public float releaseWindow;
    public float switchWindow;
    public float directionalWindow;

    public float lastInput;
    public List<int> inputQueue;
    public List<int> releaseQueue;
    public List<int> weaponSwitchQueue;
    public List<int> directionalQueue;
    public int lastDirectional;

    //  public Player_AttackScript playerAttackScript;
    //  public Player_Movement playerMovement;
    bool L2Switch;
    bool R1Switch;
    public bool crossHold;
    public bool circleHold;
    public bool L1Hold;
    public bool KB = true;
    public bool XBOX = true;
    public bool PS4;
    public float inputHorizontal;
    public float inputVertical;

    void Start()
    {
        //playerMovement = GetComponent<Player_Movement>();
        //playerAttackScript = GetComponent<Player_AttackScript>();

    }


    void Update()
    {

        //  if(Input.GetJoystickNames()[0].Contains("XBOX")) print(Input.GetJoystickNames()[0]);
        //  else if (Input.GetJoystickNames()[0].Contains("Wireless")) print(Input.GetJoystickNames()[0]);

        //    if (!PauseMenu.gameIsPaused)
        {
            if (playerID == 0)
            {
                if (Mathf.Abs(Input.GetAxis("KB_Horizontal")) > Mathf.Abs(Input.GetAxis("XB_Horizontal"))) { inputHorizontal = Input.GetAxis("KB_Horizontal"); }
                else inputHorizontal = Input.GetAxis("XB_Horizontal");
                if (Mathf.Abs(Input.GetAxis("KB_Vertical")) > Mathf.Abs(Input.GetAxis("XB_Vertical"))) { inputVertical = Input.GetAxis("KB_Vertical"); }
                else inputVertical = Input.GetAxis("XB_Vertical");


                if (inputHorizontal == 1 && lastDirectional != 6)
                {
                    StartCoroutine("DirectionalReset", 6);
                    lastDirectional = 6;
                }
                if (inputHorizontal == 0 && lastDirectional != 5)
                {
                    StartCoroutine("DirectionalReset", 5);
                    lastDirectional = 5;
                }
                if (inputHorizontal == -1 && lastDirectional != 4)
                {
                    StartCoroutine("DirectionalReset", 4);
                    lastDirectional = 4;
                }
                /*
                if (directionalQueue.Count > 2)
                    if (directionalQueue[0] == 6 && directionalQueue[1] == 5 && directionalQueue[2] == 6) playerMovement.doubleTap = true;
                    else if (directionalQueue[0] == 4 && directionalQueue[1] == 5 && directionalQueue[2] == 4) playerMovement.doubleTap = true;
                if (playerMovement.running && directionalQueue.Count > 0)
                {
                    if (directionalQueue[0] == 5 && lastDirectional == 5) playerMovement.doubleTap = false;
                }*/

                if (KB)
                {
                    if (Input.GetButtonDown("KB_CIRCLE") && !Input.GetButtonDown("KB_SQUARE") && !Input.GetButtonDown("KB_TRIANGLE") && inputHorizontal == 0) BackDash();
                    if (Input.GetButtonDown("KB_CIRCLE") && !Input.GetButtonDown("KB_SQUARE") && !Input.GetButtonDown("KB_TRIANGLE") && inputHorizontal != 0) Dash();

                    if (Input.GetButtonDown("KB_CROSS")) Cross();

                    //    if (Input.GetAxis("KB_Trigger") > 0 && !L2Switch) { SwitchUp(); L2Switch = true; }
                    //    if (Input.GetAxis("KB_Trigger") < 0 && !L2Switch) { SwitchDown(); L2Switch = true; }

                    if (Input.GetButtonDown("KB_R1") && !R1Switch) { SwitchUp(); R1Switch = true; }
                    if (Input.GetButtonDown("KB_L1") && !L2Switch) { SwitchDown(); L2Switch = true; }
                    if (Input.GetButtonUp("KB_R1")) R1Switch = false;
                    if (Input.GetButtonUp("KB_L1")) L2Switch = false;

                    if (Input.GetButtonDown("KB_TRIANGLE") && Input.GetAxis("KB_Horizontal") == 0 && Input.GetAxis("KB_Vertical") == 0) Circle();
                    if (Input.GetButtonDown("KB_TRIANGLE") && Input.GetAxis("KB_Horizontal") != 0 && Input.GetAxis("KB_Vertical") == 0) Circle();
                    if (Input.GetButtonDown("KB_TRIANGLE") && Input.GetAxis("KB_Vertical") > 0) USpecial();


                    if (Input.GetButtonDown("KB_TRIANGLE") && Input.GetAxis("KB_Vertical") < 0) DSpecial();

                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Horizontal") == 0 && Input.GetAxis("KB_Vertical") == 0 && !Input.GetButtonDown("KB_CIRCLE")) Square();
                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Horizontal") != 0 && Input.GetAxis("KB_Vertical") == 0 && !Input.GetButtonDown("KB_CIRCLE")) SAttack();
                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Vertical") > 0 && !Input.GetButtonDown("KB_CIRCLE")) Triangle();
                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Vertical") < 0 && !Input.GetButtonDown("KB_CIRCLE")) DAttack();

                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Horizontal") == 0 && Input.GetAxis("KB_Vertical") == 0 && Input.GetButtonDown("KB_CIRCLE")) Dash();
                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Horizontal") != 0 && Input.GetAxis("KB_Vertical") == 0 && Input.GetButtonDown("KB_CIRCLE")) Dash();
                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Vertical") > 0 && Input.GetButtonDown("KB_CIRCLE")) Dash();
                    if (Input.GetButtonDown("KB_SQUARE") && Input.GetAxis("KB_Vertical") < 0 && Input.GetButtonDown("KB_CIRCLE")) Dash();

                    if (Input.GetButtonUp("KB_SQUARE") && Input.GetAxis("KB_Vertical") == 0 && !Input.GetButtonDown("KB_CIRCLE")) ReleaseAttack();
                }
                if (XBOX)
                {
                    if (Input.GetButtonDown("XB_R1")) { R1();  }
                    if (Input.GetButtonDown("XB_L1")) { L1();  }


                    if (Input.GetButtonDown("XB_CROSS")) Cross();
                    if (Input.GetButtonDown("XB_SQUARE")) Square();
                    if (Input.GetButtonDown("XB_TRIANGLE")) Triangle();
                    if (Input.GetButtonDown("XB_CIRCLE")) Circle();
                    if (Input.GetButton("XB_CROSS")) crossHold = true;
                    else crossHold = false;
                    if (Input.GetButton("XB_CIRCLE")) circleHold = true;
                    else circleHold = false;
                    if (Input.GetButton("XB_L1")) L1Hold = true;
                    else L1Hold = false;


                }
            }
            else if (playerID == 1)
            {
                if (Mathf.Abs(Input.GetAxis("KB_Horizontal_2")) > Mathf.Abs(Input.GetAxis("XB_Horizontal_2"))) { inputHorizontal = Input.GetAxis("KB_Horizontal_2"); }
                else inputHorizontal = Input.GetAxis("XB_Horizontal_2");
                if (Mathf.Abs(Input.GetAxis("KB_Vertical_2")) > Mathf.Abs(Input.GetAxis("XB_Vertical_2"))) { inputVertical = Input.GetAxis("KB_Vertical_2"); }
                else inputVertical = Input.GetAxis("XB_Vertical_2");


                if (inputHorizontal == 1 && lastDirectional != 6)
                {
                    StartCoroutine("DirectionalReset", 6);
                    lastDirectional = 6;
                }
                if (inputHorizontal == 0 && lastDirectional != 5)
                {
                    StartCoroutine("DirectionalReset", 5);
                    lastDirectional = 5;
                }
                if (inputHorizontal == -1 && lastDirectional != 4)
                {
                    StartCoroutine("DirectionalReset", 4);
                    lastDirectional = 4;
                }
                /*
                if (directionalQueue.Count > 2)
                    if (directionalQueue[0] == 6 && directionalQueue[1] == 5 && directionalQueue[2] == 6) playerMovement.doubleTap = true;
                    else if (directionalQueue[0] == 4 && directionalQueue[1] == 5 && directionalQueue[2] == 4) playerMovement.doubleTap = true;
                if (playerMovement.running && directionalQueue.Count > 0)
                {
                    if (directionalQueue[0] == 5 && lastDirectional == 5) playerMovement.doubleTap = false;
                }*/

                if (KB)
                {
                    if (Input.GetButtonDown("KB_CIRCLE_2") && !Input.GetButtonDown("KB_SQUARE_2") && !Input.GetButtonDown("KB_TRIANGLE_2") && inputHorizontal == 0) BackDash();
                    if (Input.GetButtonDown("KB_CIRCLE_2") && !Input.GetButtonDown("KB_SQUARE_2") && !Input.GetButtonDown("KB_TRIANGLE_2") && inputHorizontal != 0) Dash();

                    if (Input.GetButtonDown("KB_CROSS_2")) Cross();

                    //    if (Input.GetAxis("KB_Trigger") > 0 && !L2Switch) { SwitchUp(); L2Switch = true; }
                    //    if (Input.GetAxis("KB_Trigger") < 0 && !L2Switch) { SwitchDown(); L2Switch = true; }

                    if (Input.GetButtonDown("KB_R1_2") && !R1Switch) { SwitchUp(); R1Switch = true; }
                    if (Input.GetButtonDown("KB_L1_2") && !L2Switch) { SwitchDown(); L2Switch = true; }
                    if (Input.GetButtonUp("KB_R1_2")) R1Switch = false;
                    if (Input.GetButtonUp("KB_L1_2")) L2Switch = false;

                    if (Input.GetButtonDown("KB_TRIANGLE_2") && Input.GetAxis("KB_Horizontal_2") == 0 && Input.GetAxis("KB_Vertical_2") == 0) Circle();
                    if (Input.GetButtonDown("KB_TRIANGLE_2") && Input.GetAxis("KB_Horizontal_2") != 0 && Input.GetAxis("KB_Vertical_2") == 0) Circle();
                    if (Input.GetButtonDown("KB_TRIANGLE_2") && Input.GetAxis("KB_Vertical_2") > 0) USpecial();


                    if (Input.GetButtonDown("KB_TRIANGLE_2") && Input.GetAxis("KB_Vertical_2") < 0) DSpecial();

                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Horizontal_2") == 0 && Input.GetAxis("KB_Vertical_2") == 0 && !Input.GetButtonDown("KB_CIRCLE_2")) Square();
                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Horizontal_2") != 0 && Input.GetAxis("KB_Vertical_2") == 0 && !Input.GetButtonDown("KB_CIRCLE_2")) SAttack();
                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Vertical_2") > 0 && !Input.GetButtonDown("KB_CIRCLE_2")) Triangle();
                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Vertical_2") < 0 && !Input.GetButtonDown("KB_CIRCLE_2")) DAttack();

                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Horizontal_2") == 0 && Input.GetAxis("KB_Vertical_2") == 0 && Input.GetButtonDown("KB_CIRCLE_2")) Dash();
                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Horizontal_2") != 0 && Input.GetAxis("KB_Vertical_2") == 0 && Input.GetButtonDown("KB_CIRCLE_2")) Dash();
                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Vertical_2") > 0 && Input.GetButtonDown("KB_CIRCLE_2")) Dash();
                    if (Input.GetButtonDown("KB_SQUARE_2") && Input.GetAxis("KB_Vertical_2") < 0 && Input.GetButtonDown("KB_CIRCLE_2")) Dash();

                    if (Input.GetButtonUp("KB_SQUARE_2") && Input.GetAxis("KB_Vertical_2") == 0 && !Input.GetButtonDown("KB_CIRCLE_2")) ReleaseAttack();
                }
                if (XBOX)
                {
                    if (Input.GetButtonDown("XB_CROSS_2")) Cross();
                    if (Input.GetButtonDown("XB_SQUARE_2")) Square();
                    if (Input.GetButtonDown("XB_TRIANGLE_2")) Triangle();
                    if (Input.GetButtonDown("XB_CIRCLE_2")) Circle();

                    if (Input.GetButtonDown("XB_R1_2")) { R1(); }
                    if (Input.GetButtonDown("XB_L1_2")) { L1(); }

                    if (Input.GetButton("XB_L1_2")) L1Hold = true;
                    else L1Hold = false;

                    if (Input.GetButton("XB_CROSS_2")) crossHold = true;
                    else crossHold = false;
                    if (Input.GetButton("XB_CIRCLE_2")) circleHold = true;
                    else circleHold = false;

                }


            }


        }
    }

    private void FixedUpdate()
    {
    }

    IEnumerator BufferReset(int inputID)
    {
        inputQueue.Add(inputID);
        for (int i = 0; i < bufferWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (inputQueue.Count > 0)
            inputQueue.RemoveAt(0);
    }

    IEnumerator ReleaseReset(int inputID)
    {
        releaseQueue.Add(inputID);
        for (int i = 0; i < releaseWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (releaseQueue.Count > 0)
            releaseQueue.RemoveAt(0);
    }

    IEnumerator DirectionalReset(int inputID)
    {
        directionalQueue.Add(inputID);
        for (int i = 0; i < directionalWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (directionalQueue.Count > 0)
            directionalQueue.RemoveAt(0);
    }

    IEnumerator WeaponSwitchReset(int inputID)
    {
        weaponSwitchQueue.Add(inputID);
        for (int i = 0; i < switchWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (weaponSwitchQueue.Count > 0)
            weaponSwitchQueue.RemoveAt(0);
    }


    void Square() { StartCoroutine("BufferReset", 1); }
    void Triangle() { StartCoroutine("BufferReset", 2); }
    void Cross() { StartCoroutine("BufferReset", 3); }
    void Circle() { StartCoroutine("BufferReset", 4); }
    void R1() { StartCoroutine("BufferReset", 6); }
    void L1() { StartCoroutine("BufferReset", 5); }



    void Dash() { StartCoroutine("BufferReset", 1); }
    void BackDash() { StartCoroutine("BufferReset", 2); }


    void SAttack() { StartCoroutine("BufferReset", 3); }

    void DAttack() { StartCoroutine("BufferReset", 5); }

    void USpecial() { StartCoroutine("BufferReset", 7); }
    void DSpecial() { StartCoroutine("BufferReset", 8); }


    void SwitchUp() { StartCoroutine("WeaponSwitchReset", 1); }
    void SwitchDown() { StartCoroutine("WeaponSwitchReset", 2); }



    void SpecialHold() { StartCoroutine("BufferReset", 9); }
    void SpecialRelease() { StartCoroutine("BufferReset", 9); }
    void SSpecial() { StartCoroutine("BufferReset", 9); }

    void ReleaseAttack() { StartCoroutine("ReleaseReset", 1); }
    void ReleaseUAttack() { StartCoroutine("ReleaseReset", 2); print("released"); }
    void ReleaseDAttack() { StartCoroutine("ReleaseReset", 3); print("released"); }


    void ENAttack() { StartCoroutine("BufferReset", 9); }
    void ESAttack() { StartCoroutine("BufferReset", 9); }
    void EUAttack() { print("Neutral attack"); StartCoroutine("BufferReset", 9); }
    void EDAttack() { StartCoroutine("BufferReset", 10); }



}
