using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public bool isOnRoute;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyNM;
    private Transform[] casillas;
    private PlayerMove player;
    private CameraFollow gameM;
    private Animator animator;

    private void Start()
    {
        casillas = GameObject.Find("RayCast").GetComponent<GridActivation>().casillas;
        enemyStats = GetComponent<EnemyStats>();
        enemyNM = GetComponent<NavMeshAgent>();
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();

        isOnRoute = false;

        print(casillas[RandomPlayerPiece()].name);
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
            }
        }
    }

    public void StarIA()
    {
        StatesManager();
    }

    private void StatesManager()
    {
        if (enemyStats.GetEnergy() > 0)
        {
            print(Vector3.Distance(transform.position, player.transform.position));
            if(Vector3.Distance(transform.position, player.transform.position) < 4f && enemyStats.GetEnergy() > 4)
            {
                player.GetComponent<PlayerStats>().SetLife(-enemyStats.GetAtk());
                enemyStats.SetEnergy(-4);
                StatesManager();
                StartCoroutine(AttackAnim());
                print("ataco");
            }
            else
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
                    print("energia insuficiente");
                    return;
                }

                enemyStats.SetEnergy(-energy);

                enemyNM.SetDestination(casillas[destiny].position);
                animator.SetInteger("A_Movement", 1);
                StartCoroutine(StartRoute());
                print("en ruta");
                print(enemyStats.GetEnergy());
            }
        }
        else
        {
            gameM.NextIA(GetComponent<StateManager>());
            print("0 energia");
        }
    }
    private IEnumerator AttackAnim()
    {
        animator.SetInteger("A_Attack", 1);
        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator StartRoute()
    {
        yield return new WaitForSeconds(0.5f);
        isOnRoute = true;
    }

    private int RandomPlayerPiece()
    {
        int e = Random.Range(0, casillas.Length);
        if (Vector3.Distance(player.transform.position, casillas[e].position) > 3)
        {
            return RandomPlayerPiece();
        }
        else
        {
            return e;
        }
    }
}
