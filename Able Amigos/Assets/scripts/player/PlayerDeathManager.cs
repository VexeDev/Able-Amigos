using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{ 
    void Update()
    {
        healthCheck();
    }
    void healthCheck()
    {
        GameObject player1 = GameObject.Find("player1");
        GameObject player2 = GameObject.Find("player2");

        player1Script p1Script = player1.GetComponent<player1Script>();
        player2Script p2Script = player2.GetComponent<player2Script>();

        // if either health is 0, set both players to 0 health
        if (p1Script.health == 0 || p2Script.health == 0)
        {
            p1Script.health = 0;
            p2Script.health = 0;
        }
    }
}
