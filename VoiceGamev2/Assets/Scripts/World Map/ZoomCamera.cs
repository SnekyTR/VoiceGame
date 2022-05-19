using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float scrollSpeed =10;
    private Camera zoomCamera;
    // Start is called before the first frame update
    void Start()
    {
        zoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomCamera.orthographic)
        {

                zoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

            
        }
        else
        {

                zoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

            
        }
    }
}
