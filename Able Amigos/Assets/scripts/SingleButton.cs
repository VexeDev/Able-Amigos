using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleButton : MonoBehaviour
{
    public GameObject triggerObj = null;
    public bool pressing;
    public bool canTriggerMultiple = false;
    bool pressed = false;
    public bool down;
    public AudioSource source;
    public AudioClip buttonPress;

    void Start()
    {
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "player1" || collision.tag == "player2") && pressed == false)
        {
            down = !down;
            source.clip = buttonPress;
            source.Play();
            if(triggerObj.GetComponent<PlaceableDoor>() != null)
            {
                triggerObj.GetComponent<PlaceableDoor>().Toggle();
                pressed = true;
            }
        }
        else if ((collision.tag == "player1" || collision.tag == "player2") && pressed == true && canTriggerMultiple == true)
        {
            down = !down;
            source.clip = buttonPress;
            source.Play();
            if (triggerObj.GetComponent<PlaceableDoor>() != null)
            {
                triggerObj.GetComponent<PlaceableDoor>().Toggle();
                pressed = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        pressing = true;
    }
}
