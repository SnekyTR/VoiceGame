using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class G_Cinematic : MonoBehaviour
{
    private NavMeshAgent magnusNavMesh;
    private NavMeshAgent vagnarNavMesh;
    private Animator magnusAnimator;
    private Animator vagnarAnimator;
    private Transform dest1;
    private Transform dest2;
    private Transform magnus;
    private Transform vagnar;
    private CameraFollow cameraFollow;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordHip;
    [SerializeField] private Canvas canvas;
    private bool entered;
    // Start is called before the first frame update
    void Start()
    {
        /*magnusNavMesh = GameObject.Find("Magnus").GetComponent<NavMeshAgent>();
        magnusAnimator = magnusNavMesh.GetComponent<Animator>();
        vagnarNavMesh = GameObject.Find("Vagnar").GetComponent<NavMeshAgent>();
        vagnarAnimator = vagnarNavMesh.GetComponent<Animator>();
        dest1 = GameObject.Find("MagnusDest").transform;
        dest2 = GameObject.Find("VagnarDest").transform;
        magnus = transform.GetChild(0).GetComponent<Transform>();
        vagnar = transform.GetChild(1).GetComponent<Transform>();*/
        cameraFollow = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        canvas = GameObject.Find("CanvasManager").GetComponent<Canvas>();
        canvas.enabled = false;

        //StartWalking();
    }
    public void StartWalking()
    {
        magnusNavMesh.SetDestination(dest1.position);
        magnusAnimator.SetInteger("A_Walk", 1);

        vagnarNavMesh.SetDestination(dest2.position);
        vagnarAnimator.SetInteger("A_Walk", 1);

        StartCoroutine(sss());
    }
    IEnumerator sss()
    {
        yield return new WaitForSeconds(0.2f);
        sword.SetActive(false);
    }
    private void Update()
    {
        /*if ((Vector3.Distance(magnus.position, dest1.position)) <= 1f)
        {
            if (!entered)
            {
                entered = true;
                magnusAnimator.SetInteger("A_Walk", 0);
                StartCoroutine(Withdraw());
            }
        }
        if ((Vector3.Distance(vagnar.position, dest2.position)) <= 1f)
        {
            if (!entered)
            {
                entered = true;
                vagnarAnimator.SetInteger("A_Walk", 0);
                StartCoroutine(Withdraw());
            }
        }*/
    }
    IEnumerator Withdraw()
    {
        magnusAnimator.SetInteger("A_Withdraw", 1);
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(true);
        swordHip.SetActive(false);
        magnusAnimator.SetInteger("A_Withdraw", 0);
        // 0 1 6
        // 15 -180
    }
    public void ActivateCanvas()
    {
        cameraFollow.selectPJCmdR.Start();
        canvas.enabled = true;
    }
}
