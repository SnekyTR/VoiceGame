using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonGuardianAI : MonoBehaviour
{
    public bool isOnRoute, setTarget;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyNM;
    private List<Transform> casillas = new List<Transform>();
    private Transform target;
    private CameraFollow gameM;
    private Animator animator;

    int mask;
    public int ShieldAport;

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
                animator.SetInteger("A_Movement", 0);
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
        if (enemyStats.GetEnergy() > 0 || enemyStats.GetEnergyActions() > 0)
        {

            if(enemyStats.GetEnergyActions() >= 3 && EnemysLife()) StartCoroutine(SupportEnemy());
            else if (Vector3.Distance(transform.position, target.position) < enemyStats.GetRange() && enemyStats.GetEnergyActions() >= 2)
            {
                if (target.GetComponent<PlayerStats>().GetLife() > 0)
                {
                    StartCoroutine(AttackAnim());
                }
                else
                {
                    StarIA();
                }
            }
            else if (Vector3.Distance(transform.position, target.position) > enemyStats.GetRange())
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

                enemyNM.isStopped = false;
                enemyNM.SetDestination(casillas[destiny].position);
                animator.SetInteger("A_Movement", 1);
                StartCoroutine(StartRoute());
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
        animator.SetInteger("A_BasicAtk", 1);
        gameM.CameraCinematic();
        yield return new WaitForSeconds(0.5f);

        RaycastHit hit;
        Vector3 newPos = transform.position;
        newPos.y += 1;
        Vector3 newDir = target.position - transform.position;
        if (Physics.Raycast(newPos, newDir, out hit, 7f, mask))
        {
            if (hit.transform == target)
            {
                target.GetComponent<PlayerStats>().SetLife(-enemyStats.GetAtk());
            }
        }
        enemyStats.SetEnergyAction(-2);
        print(2);
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
        float dis = 10000;
        int ps = 0;

        for (int i = 0; i < casillas.Count; i++)
        {
            if ((Vector3.Distance(transform.position, casillas[i].position) / 2) <= enemyStats.GetEnergy())
            {
                if (Vector3.Distance(target.position, casillas[i].position) <= dis && !casillas[i].GetComponent<SectionControl>().isOcuped)
                {
                    RaycastHit hit;
                    Vector3 newPos = casillas[i].position;
                    newPos.y += 1;
                    Vector3 newDir = target.position - casillas[i].position;
                    if (Physics.Raycast(newPos, newDir, out hit, 100f, mask))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            ps = i;
                            dis = Vector3.Distance(target.position, casillas[i].position);
                        }
                    }
                }
            }
        }

        return ps;
    }

    private IEnumerator SupportEnemy()
    {
        int en = 0;

        for(int i = 0; i < gameM.enemys.Count; i++)
        {
            float div = (gameM.enemys[i].GetComponent<EnemyStats>().GetLife() / gameM.enemys[i].GetComponent<EnemyStats>().maxLife);

            if (div < 0.75f)
            {
                animator.SetInteger("A_BasicAtk", 3);
                enemyStats.SetEnergyAction(-3);

                en = i;
                break;
            }
        }

        yield return new WaitForSeconds(1.5f);

        gameM.enemys[en].GetComponent<EnemyStats>().NewShield(ShieldAport);

        yield return new WaitForSeconds(0.5f);

        StatesManager();
    }

    private bool EnemysLife()
    {
        for (int i = 0; i < gameM.enemys.Count; i++)
        {
            float div = (gameM.enemys[i].GetComponent<EnemyStats>().GetLife() / gameM.enemys[i].GetComponent<EnemyStats>().maxLife);

            if (div < 0.75f)
            {
                return true;
            }
        }
        return false;
    }
}
