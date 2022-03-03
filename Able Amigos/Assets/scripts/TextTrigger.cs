using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    private TypeWriterEffect typeWriterEffect;
    public TMP_Text textLabel;
    bool hasTriggered = false;
    public bool triggerInstantly = false;

    [SerializeField] public string text;

    private void Start()
    {
        if(triggerInstantly == true)
        {
            if (hasTriggered == false)
            {
                GetComponent<TypeWriterEffect>().Run(text, textLabel);
                hasTriggered = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered == false)
        {
            GetComponent<TypeWriterEffect>().Run(text, textLabel);
            hasTriggered = true;
        }
    }
}
