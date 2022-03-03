using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    bool pressing1;
    bool pressing2;
    public GameObject triggerObj;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        pressing1 = button1.transform.GetChild(0).gameObject.GetComponent<SingleButtonM>().pressing;
        pressing2 = button2.transform.GetChild(0).gameObject.GetComponent<SingleButtonM>().pressing;

        if (pressing1 && pressing2)
        {
            if(triggerObj.GetComponent<Door>() != null)
            {
                triggerObj.GetComponent<Door>().OpenDoor();
            }
        }
    }
}
