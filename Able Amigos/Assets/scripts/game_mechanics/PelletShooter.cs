using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletShooter : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject pellet;
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("Fire1"))
        {
            FirePellet();
        }
    }

    void FirePellet()
    {
        if (player.GetComponent<player1Script>() != null)
        {
            if (player.GetComponent<player1Script>().isFacingLeft == false)
            {
                Instantiate(pellet, shootPoint.position, shootPoint.rotation);
            } 
            else
            {
                Instantiate(pellet, -shootPoint.position, shootPoint.rotation);
            }
        } 
        else if (player.GetComponent<player2Script>() != null)
        {
            if (player.GetComponent<player2Script>().isFacingLeft == false)
            {
                Debug.Log("shoot1");
                Debug.Log(player.GetComponent<player2Script>().isFacingLeft);
                Instantiate(pellet, shootPoint.position, shootPoint.rotation);
            }
            else
            {
                Debug.Log("shoot2");
                Debug.Log(player.GetComponent<player2Script>().isFacingLeft);
                Instantiate(pellet, -shootPoint.position, shootPoint.rotation);
            }
        }
    }

 
}
 