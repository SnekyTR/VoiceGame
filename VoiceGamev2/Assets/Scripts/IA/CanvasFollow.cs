using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CanvasFollow : MonoBehaviour
{
    private Quaternion starRotation;
    [SerializeField] public NavMeshAgent enemy;
    void Start()
    {
        starRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = starRotation;
    }
}
