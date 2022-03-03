using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAttempt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.GetJoystickNames();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("JumpPlayer1"))
        {
            Debug.Log("pressed player 1");
        }

        if (Input.GetButtonDown("JumpPlayer2"))
        {
            Debug.Log("pressed player2");
        }
    }

}
