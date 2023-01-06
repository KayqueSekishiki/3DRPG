using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public Vector3 Offset;

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    public float currentZoom;

    public float pitch;

  
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; 
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void LateUpdate()
    {
        transform.position = player.position - Offset * currentZoom;
        transform.LookAt(player.position + Vector3.up * pitch);
    }
}
