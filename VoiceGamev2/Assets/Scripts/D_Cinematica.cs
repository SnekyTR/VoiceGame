using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class D_Cinematica : MonoBehaviour
{
    private NavMeshAgent magnusNavMesh;
    private NavMeshAgent VagnarNavMesh;
    [SerializeField]private Animator magnusAnim;
    [SerializeField]private Animator vagnarAnim;
    private Transform magnusDest;
    private Transform vagnarDest;
    [SerializeField]private Transform Esqueleto;
    [SerializeField]private Transform araña;
    private Transform magnus;
    private CameraFollow cameraFollow;
    private Transform vagnar;
    private Canvas canvas;
    private BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        magnusNavMesh = transform.GetChild(0).GetComponent<NavMeshAgent>();
        VagnarNavMesh = transform.GetChild(1).GetComponent<NavMeshAgent>();
        magnus = transform.GetChild(0).GetComponent<Transform>();
        magnusAnim = magnus.GetComponent<Animator>();
        vagnar = transform.GetChild(1).GetComponent<Transform>();
        vagnarAnim = vagnar.GetComponent<Animator>();
        magnusDest = GameObject.Find("MagnusDest").GetComponent<Transform>();
        cameraFollow = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        box = cameraFollow.transform.GetChild(0).GetComponent<BoxCollider>();
        vagnarDest = GameObject.Find("VagnarDest").GetComponent<Transform>();
        canvas = GameObject.Find("CanvasManager").GetComponent<Canvas>();
        canvas.enabled = false;
        box.isTrigger = false;

        StartCoroutine(Talk());
        //0 1 6
        // 15 -180 0
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(magnus.position, magnusDest.position)) <= 0.4f)
        {
            magnusAnim.SetInteger("A_Run", 0);
            magnus.LookAt(Esqueleto.position);
            //StartCoroutine(Withdraw());
        }
        if ((Vector3.Distance(vagnar.position, vagnarDest.position)) <= 0.4f)
        {
            vagnarAnim.SetInteger("A_Run", 0);
            vagnar.LookAt(araña.position);
            //StartCoroutine(Withdraw());
        }
    }
    public void Nod()
    {
        vagnarAnim.SetInteger("A_NoIdle", 0);
        vagnarAnim.SetInteger("A_Nod", 1);
    }
    public void Run()
    {
        StartCoroutine(StartRunning());
    }
    IEnumerator StartRunning()
    {
        vagnarAnim.SetInteger("A_Nod", 0);
        magnusNavMesh.SetDestination(magnusDest.position);
        magnusAnim.SetInteger("A_Run", 1);
        yield return new WaitForSeconds(0.5f);
        VagnarNavMesh.SetDestination(vagnarDest.position);
        vagnarAnim.SetInteger("A_Run", 1);

    }
    public void Kick()
    {
        StartCoroutine(Kicking());
    }
    IEnumerator Kicking()
    {
        magnusAnim.SetInteger("A_Kick", 1);
        yield return new WaitForSeconds(0.5f);
        magnusAnim.SetInteger("A_Kick", 0);

    }
    public void ActivateCanvas()
    {
        cameraFollow.selectPJCmdR.Start();
        canvas.enabled = true;
        box.isTrigger = true;
    }
    IEnumerator Talk()
    {
        magnusAnim.SetInteger("A_Talk", 1);
        vagnarAnim.SetInteger("A_NoIdle", 1);
        yield return new WaitForSeconds(1f);
        magnusAnim.SetInteger("A_Talk", 0);
        
    }
}
