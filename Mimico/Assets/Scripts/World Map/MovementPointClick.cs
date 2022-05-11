using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovementPointClick : MonoBehaviour
{
    [SerializeField]private Camera camera;
    [SerializeField]private GameObject targetDest;
    private NavMeshAgent player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(ray,out hitPoint))
            {
                targetDest.transform.position = hitPoint.point;
                player.SetDestination(hitPoint.point);
            }
        }
    }
}
