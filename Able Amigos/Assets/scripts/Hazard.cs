using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player1")
        {
            collision.GetComponent<player1Script>().health = 0;
        }
        if(collision.tag == "player2")
        {
            collision.GetComponent<player2Script>().health = 0;
        }
    }
}
