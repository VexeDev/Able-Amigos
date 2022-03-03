using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch : MonoBehaviour
{
    
    public bool moving = false;
    public bool reseting = false;
    public string playerTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "player1" || coll.tag == "player2")
        {
            moving = true;
            reseting = false;
            playerTag = coll.tag;
        }
        
        
        
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "player1" || coll.tag == "player2")
        {
            moving = false;
            reseting = true;
        }
    }
}
