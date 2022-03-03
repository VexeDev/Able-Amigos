using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Vector3 originalPos;
    public float speed =  1.2f;
    public bool moving = false;
    public bool reseting = false;
    public GameObject platformCollider;
    public Vector3 currentPos;
    public Vector3 goalPos;
    public Vector3 platformScale;
    public bool vertical = false;
    

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        currentPos = transform.position;
        platformScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        moving = platformCollider.GetComponent<MoveOnTouch>().moving;
        reseting = platformCollider.GetComponent<MoveOnTouch>().reseting;
        
        

        if (moving)
        {
            //parent player to platform
            GameObject.FindWithTag(platformCollider.GetComponent<MoveOnTouch>().playerTag).transform.parent =  this.transform;

        }

        else if (reseting)
        {
            //unparent player object
            GameObject.FindWithTag(platformCollider.GetComponent<MoveOnTouch>().playerTag).transform.parent = null;
            //return platform to original position
            
        }
        
        if (currentPos.x < originalPos.x || currentPos.y < originalPos.y)
        {
            reseting = false;
            moving = true;
            speed = -1 * speed;
           
        }

        else if (currentPos.x > goalPos.x || currentPos.y > goalPos.y)
        {
            //moving = false;
            //reseting = true;
            speed = -1 * speed;
        }
        
    }

    void FixedUpdate()
    {
        if(moving && vertical == false)
        {
            currentPos.x += (speed * Time.deltaTime);
            transform.position = currentPos;
        }

        else if(reseting && vertical == false)
        {
            currentPos.x -= (speed * Time.deltaTime);
            transform.position = currentPos;
            
        }
        else if(moving && vertical)
        {
            currentPos.y += (speed * Time.deltaTime);
            transform.position = currentPos;
            
        }
        else if(reseting && vertical)
        {
            currentPos.y -= (speed * Time.deltaTime);
            transform.position = currentPos;
            
        }

        
        
    }
}
