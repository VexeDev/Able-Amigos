using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray = new Sprite[2];
    BoxCollider2D collider;

    public SingleButton buttonScript;
    //public SingleButtonM modifiedButtonScript;
    public bool down;
    public bool pressing;
    Vector3 originalTriggerPos;
    Vector3 startTriggerPos;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        originalTriggerPos = transform.GetChild(0).localPosition;
        startTriggerPos = transform.GetChild(0).localPosition;
        originalTriggerPos.y -= (0.37f - 0.19f);
    }

    // Update is called once per frame
    void Update()
    {
        down = buttonScript.down;
        //pressing = modifiedButtonScript.pressing;
        //Debug.Log(pressing);
        ChangeButtonSprite();
    }

    void ChangeButtonSprite()
    {
        if (down)
        {
            spriteRenderer.sprite = spriteArray[1];
            collider.size = new Vector2(0.88f, 0.09f);
            collider.offset = new Vector2(0.0015f, -0.3632f);
            transform.GetChild(0).localPosition = originalTriggerPos;
        }
        else
        {
            spriteRenderer.sprite = spriteArray[0];
            collider.size = new Vector2(0.88f, 0.37f);
            collider.offset = new Vector2(0.0015f, -0.3132f);
            transform.GetChild(0).localPosition = startTriggerPos;
        }
    }
}
