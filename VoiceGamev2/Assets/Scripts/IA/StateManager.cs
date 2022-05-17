using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public bool isOnRoute, setTarget;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyNM;
    private Transform[] casillas;
    private Transform target;
    private CameraFollow gameM;
    private Animator animator;

    private void Start()
    {
        casillas = GameObject.Find("RayCast").GetComponent<GridActivation>().casillas;
        enemyStats = GetComponent<EnemyStats>();
        enemyNM = GetComponent<NavMeshAgent>();
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();

        isOnRoute = false;
        setTarget = false;
    }

    void LateUpdate()
    {
        if (isOnRoute)
        {
            if(enemyNM.velocity == Vector3.zero)
            {
                StatesManager();
                isOnRoute = false;
                animator.SetInteger("A_Movement", 0);
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
        for (int i = 0; i < gameM.players.Length; i++)
        {
            if(Vector3.Distance(transform.position, gameM.players[i].transform.position) < dis)
            {
                target = gameM.players[i].transform;
                dis = Vector3.Distance(transform.position, gameM.players[i].transform.position);
            }
        }

        StatesManager();
    }

    private void StatesManager()
    {
        if (enemyStats.GetEnergy() > 0)
        {
            print(Vector3.Distance(transform.position, target.position));
            if(Vector3.Distance(transform.position, target.position) < 4f && enemyStats.GetEnergy() >= 4)
            {
                StartCoroutine(AttackAnim());
            }
            else if(Vector3.Distance(transform.position, target.position) > 4f)
            {
                int destiny = RandomPlayerPiece();

                float energy = (Vector3.Distance(casillas[destiny].position, transform.position) / 2);
                if (energy > ((int)energy + 0.1f) && energy < ((int)energy + 0.7f))
                {
                    energy = (int)energy + 0.5f;
                }
                else if (energy > ((int)energy + 0.7f)) energy = (int)energy + 1;
                else energy = (int)energy;

                if (energy > enemyStats.GetEnergy())
                {
                    gameM.NextIA(GetComponent<StateManager>());
                    return;
                }

                enemyStats.SetEnergy(-energy);

                enemyNM.SetDestination(casillas[destiny].position);
                animator.SetInteger("A_Movement", 1);
                StartCoroutine(StartRoute());
                print(enemyStats.GetEnergy());
            }
            else
            {
                gameM.NextIA(GetComponent<StateManager>());
            }
        }
        else
        {
            gameM.NextIA(GetComponent<StateManager>());
        }
    }
    private IEnumerator AttackAnim()
    {
        setTarget = true;
        yield return new WaitForSeconds(0.8f);
        animator.SetInteger("A_Attack", 1);
        yield return new WaitForSeconds(0.5f);
        target.GetComponent<PlayerStats>().SetLife(-enemyStats.GetAtk());
        enemyStats.SetEnergy(-4);
        yield return new WaitForSeconds(0.5f);
        setTarget = false;
        StatesManager();
    }
    private IEnumerator StartRoute()
    {
        yield return new WaitForSeconds(0.5f);
        isOnRoute = true;
    }

    private int RandomPlayerPiece()
    {
        int e = Random.Range(0, casillas.Length);
        float dist = Vector3.Distance(target.position, casillas[e].position);
        if (dist > 3f || dist < 1f)
        {
            return RandomPlayerPiece();
        }
        else
        {
            return e;
        }
    }
}
