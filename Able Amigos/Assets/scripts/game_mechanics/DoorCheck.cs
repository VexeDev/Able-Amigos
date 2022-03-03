using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public GameObject door;
    public List<GameObject> playersInDoor;
    public bool winCondition = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player1" && other.GetType() == typeof(BoxCollider2D))
        {
            playersInDoor.Add(other.gameObject);
        }

        if (other.tag == "player2" && other.GetType() == typeof(BoxCollider2D))
        {
            playersInDoor.Add(other.gameObject);
        }

        CheckIfDoorOpen();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "player1" && collision.GetType() == typeof(BoxCollider2D))
        {
            playersInDoor.Remove(collision.gameObject);
        }

        if (collision.tag == "player2" && collision.GetType() == typeof(BoxCollider2D))
        {
            playersInDoor.Remove(collision.gameObject);
        }

        CheckIfDoorOpen();
    }

    void CheckIfDoorOpen ()
    {
        if (playersInDoor.Count == 2 && winCondition == true)
        {
            door.GetComponent<Door>().OpenDoor();
        } else
        {
            door.GetComponent<Door>().CloseDoor();
        }
    }
}
