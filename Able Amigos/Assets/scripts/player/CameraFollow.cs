using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 vel;
    public float smoothingTime = 0.5f;
    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimit = 50;

    public Camera cam;

    public float deadZoom;

    private void LateUpdate()
    {
        //move
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref vel, smoothingTime);

        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimit);
        //zoom
        if (shouldZoom(newZoom) == true)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        }
    }

    bool shouldZoom (float second)
    {
        //Debug.Log("zoom: " + (Mathf.Abs(cam.orthographicSize - second).ToString()));
        if (Mathf.Abs(cam.orthographicSize - second) > deadZoom)
        {
            return true;
        } else
        {
            return false;
        }
    }

    Vector3 GetCenterPoint ()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < 2; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    float GetGreatestDistance ()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < 2; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }
}
