using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class B_Cinematic : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Animator animator;
    private Transform dest1;
    private Transform player;
    private CameraFollow cameraFollow;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordHip;
    [SerializeField] private GameObject FTUE;
    [SerializeField] private Canvas canvas;
    private bool entered;
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
            if (!entered)
            {
                entered = true;
                animator.SetInteger("A_Walk", 0);
                StartCoroutine(Withdraw());
            }
        }
    }
    private void StartWalking()
    {
        sword.SetActive(false);
        navMesh.SetDestination(dest1.position);
        animator.SetInteger("A_Walk", 1);
    }
    IEnumerator Withdraw()
    {
        animator.SetInteger("A_Withdraw", 1);
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(true);
        swordHip.SetActive(false);
        animator.SetInteger("A_Withdraw", 0);
        // 0 1 6
        // 15 -180
    }
    public void ActivateFTUE()
    {
        cameraFollow.selectPJCmdR.Start();
        print("Se activa");
        canvas.enabled = true;
        FTUE.SetActive(true);
        navMesh.speed = 4;
    }
}
