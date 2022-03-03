using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThatOneLevel : MonoBehaviour
{
    public GameObject player1;

    void Start()
    {
        player1.GetComponent<player1Script>().thatOneLevel();
    }
}
