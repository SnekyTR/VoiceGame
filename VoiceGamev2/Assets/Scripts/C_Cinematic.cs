using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class C_Cinematic : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Animator animator;
    private Transform dest1;
    private Transform player;
    private CameraFollow cameraFollow;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordHip;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GameObject.Find("Magnus").GetComponent<NavMeshAgent>();
        animator = navMesh.GetComponent<Animator>();
        dest1 = GameObject.Find("Destination").transform;
        player = transform.GetChild(0).GetComponent<Transform>();
        cameraFollow = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        canvas = GameObject.Find("CanvasManager").GetComponent<Canvas>();
        canvas.enabled = false;

        StartWalking();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.position, dest1.position)) <= 0.5f)
        {
            animator.SetInteger("A_Cwalk", 0);
            //StartCoroutine(Withdraw());
        }
    }
    private void StartWalking()
    {
        navMesh.SetDestination(dest1.position);
        animator.SetInteger("A_Cwalk", 1);
    }
    public void ActivateCanvas()
    {
        cameraFollow.selectPJCmdR.Start();
        canvas.enabled = true;
    }

    IEnumerator OpenSequence()
    {
        yield return new WaitForSeconds(2f);
    }
}
