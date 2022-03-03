using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    public Vector3 currentRotation;
    public float rotateSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + (rotateSpeed * Time.deltaTime)));
        currentRotation = transform.rotation.eulerAngles;
    }
}
