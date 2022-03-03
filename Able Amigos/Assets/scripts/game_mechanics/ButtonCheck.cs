using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCheck : MonoBehaviour
{
    public PuzzleManager manage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "player1" || collider.tag == "player2")
        {
            manage.playerButtonPress.Add(transform.parent.gameObject.tag);

        }
    }

    /*void OnMouseDown()
    {
        Debug.Log("clicky clicky");
        if(gameObject.tag == "red" || gameObject.tag == "green" || gameObject.tag == "blue" || gameObject.tag == "yellow")
        {
            manage.playerButtonPress.Add(gameObject.tag);

        }
    }*/
}
