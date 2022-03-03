using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleButtonM : MonoBehaviour
{
    public bool pressing = false;
    public AudioSource source;
    public AudioClip buttonPress;

    void Start()
    {
        source = GameObject.FindWithTag("sound").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        source.clip = buttonPress;
        source.Play();
        pressing = true;
    }

}
