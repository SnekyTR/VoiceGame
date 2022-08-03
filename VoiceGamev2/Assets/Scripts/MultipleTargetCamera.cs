using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();
    private Vector3 velocity;
    public float smoothTime = 4;
    public Vector3 offset;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    private Camera cam;

    private bool isRotate;
    private Quaternion rotationPos;

    [Header("Rotation")]
    public List<Vector3> rotations;
    public List<Vector3> positions;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if(targets.Count == 0)
            return;

        Move();
        Zoom();

        Rotation();
    }

    private void Rotation()
    {
        if (isRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationPos, 1.8f * Time.deltaTime);
        }
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/ zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
    }
    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
    Vector3 GetCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    public void CameraStrategic(Transform l)
    {
        if(targets.Count > 0) targets = new List<Transform>();
        targets.Add(l);

        offset = positions[0];
        rotationPos = Quaternion.Euler(rotations[0]);
        isRotate = true;

    }

    public void CameraPlayer(Transform l)
    {
        targets = new List<Transform>();
        targets.Add(l);

        offset = positions[1];
        rotationPos = Quaternion.Euler(rotations[1]);
        isRotate = true;
    }

    public void CameraEnemy(Transform l)
    {
        targets = new List<Transform>();
        targets.Add(l);

        offset = positions[0];
        rotationPos = Quaternion.Euler(rotations[0]);
        isRotate = true;
    }

    public void SectionAdd(Transform l)
    {
        targets.Add(l);
    }

    public void CameraPlayerTurn(List<Transform> l)
    {
        targets = new List<Transform>();
        targets = l;

        offset = positions[0];
        rotationPos = Quaternion.Euler(rotations[0]);
    }
}
