using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CanvasFollow : MonoBehaviour
{
    private Quaternion starRotation;
    [SerializeField] public NavMeshAgent enemy;
    private Transform cam;
    void Start()
    {
        starRotation = transform.rotation;
        cam = GameObject.Find("Main Camera").transform;
    }

    void LateUpdate()
    {
        Vector3 direction = cam.transform.position - transform.position;

        Quaternion rt = Quaternion.LookRotation(direction);
        rt.x = transform.rotation.x;
        rt.z = transform.rotation.z;
        if(SceneManager.GetActiveScene().buildIndex != 1)transform.rotation = Quaternion.Slerp(transform.rotation, rt, 6f * Time.deltaTime);
        else transform.rotation = starRotation;
    }
}
