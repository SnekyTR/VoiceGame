using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonGuardianAI : MonoBehaviour
{
    public bool isOnRoute, setTarget;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyNM;
    private Transform[] casillas;
    private Transform target;
    private CameraFollow gameM;
    private Animator animator;

    int mask;

    void Start()
    {
        casillas = GameObject.Find("RayCast").GetComponent<GridActivation>().casillas;
        enemyStats = GetComponent<EnemyStats>();
        enemyNM = GetComponent<NavMeshAgent>();
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();

        isOnRoute = false;
        setTarget = false;

        mask = 1 << LayerMask.NameToLayer("Player");
        mask |= 1 << LayerMask.NameToLayer("Occlude");
    }


    void Update()
    {
        if (isOnRoute)
        {
            if (enemyNM.velocity == Vector3.zero)
            {
                StatesManager();
                isOnRoute = false;
                enemyNM.isStopped = true;
            }
        }

        if (setTarget)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotacion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 5f * Time.deltaTime);
        }
    }

    public void StarIA()
    {
        float dis = 10000;

        if (gameM.players.Count == 0) return;
        for (int i = 0; i < gameM.players.Count; i++)
        {
            if (Vector3.Distance(transform.position, gameM.players[i].transform.position) < dis)
            {
                target = gameM.players[i].transform;
                dis = Vector3.Distance(transform.position, gameM.players[i].transform.position);
            }
        }

        StatesManager();
    }

    private void StatesManager()
    {

    }
}
