using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    public bool toxic;
    public bool isOnRoute, setTarget;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyNM;
    private List<Transform> casillas = new List<Transform>();
    private Transform target;
    private CameraFollow gameM;
    private Animator animator;
    private AudioSource audioSource;
    private AudioClip audioClip;

    private GridActivation gridA;

    int mask;

    public GameObject toxicFX;
    public GameObject bloodFx;

    private void Start()
    {
        gridA = GameObject.Find("RayCast").GetComponent<GridActivation>();
        casillas = gridA.casillas;
        enemyStats = GetComponent<EnemyStats>();
        enemyNM = GetComponent<NavMeshAgent>();
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        audioSource = gameM.gameObject.GetComponent<AudioSource>();
        audioClip = gameM.gameObject.GetComponent<PlayerMove>().moveSteps;

        isOnRoute = false;
        setTarget = false;

        mask = 1 << LayerMask.NameToLayer("Player");
        mask |= 1 << LayerMask.NameToLayer("Occlude");
    }

    void LateUpdate()
    {
        if (isOnRoute)
        {
            if (enemyNM.velocity == Vector3.zero)
            {
                gridA.DisableAtkGrid();
                StatesManager();
                isOnRoute = false;
                audioSource.Stop();
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
        if (enemyStats.IsStunned())
        {
            StartCoroutine(NextEnemyStun());
            return;
        }

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

    IEnumerator NextEnemyStun()
    {
        yield return new WaitForSeconds(0.5f);
        gameM.NextIA(GetComponent<StateManager>());
    }

    private void StatesManager()
    {
        if (enemyStats.GetEnergy() > 0 || enemyStats.GetEnergyActions() > 0)
        {
            RaycastHit hit;
            Vector3 newPos = transform.position;
            newPos.y += 1;
            Vector3 newDir = target.position - transform.position;
            if (Physics.Raycast(newPos, newDir, out hit, 100f, mask))
            {

            }

            if (Vector3.Distance(transform.position, target.position) <= enemyStats.GetRange() && enemyStats.GetEnergyActions() >= 2 && hit.transform.tag == "Player")
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
            else if (Vector3.Distance(transform.position, target.position) >= enemyStats.GetRange())
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

                gridA.EnableAtkGrid(transform, enemyStats.GetEnergy()*2);

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

        gameM.CameraCinematic();

        animator.SetInteger("A_Attack", 1);

        yield return new WaitForSeconds(0.6f);

        if (toxic)
        {
            Vector3 abl = transform.GetChild(0).GetChild(0).GetChild(1).position;
            abl.y -= 1.6f;
            Destroy(Instantiate(toxicFX, abl, transform.rotation), 1.2f);
            yield return new WaitForSeconds(0.4f);
        }

        RaycastHit hit;
        Vector3 newPos = transform.position;
        newPos.y += 1;
        Vector3 newDir = target.position - transform.position;
        if (Physics.Raycast(newPos, newDir, out hit, 7f, mask))
        {
            if (hit.transform == target)
            {
                target.GetComponent<PlayerStats>().SetLife(-enemyStats.GetAtk());

                if (toxic) target.GetComponent<PlayerStats>().PoisonStart();
                else
                {
                    Vector3 pos = target.position;
                    pos.y += 1;

                    Destroy(Instantiate(bloodFx, pos, transform.rotation), 1.5f);
                }
            }
        }
        enemyStats.SetEnergyAction(-2);
        yield return new WaitForSeconds(1f);
        setTarget = false;
        StatesManager();
    }
    private IEnumerator StartRoute()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.clip = audioClip;
        audioSource.volume = 0.2f;
        audioSource.Play();
        isOnRoute = true;
    }

    private int RandomPlayerPiece()
    {
        float dis1 = 10000;
        float dis2 = 10000;
        int ps = 0;

        for (int i = 0; i < casillas.Count; i++)
        {
            if ((Vector3.Distance(transform.position, casillas[i].position) / 2) <= enemyStats.GetEnergy())
            {
                if (Vector3.Distance(target.position, casillas[i].position) < (enemyStats.GetRange() - 0.2f) && !casillas[i].GetComponent<SectionControl>().isOcuped
                    && Vector3.Distance(transform.position, casillas[i].position) <= dis1)
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
                            dis1 = Vector3.Distance(transform.position, casillas[i].position);

                            print("hi1");
                        }
                    }
                }
                else if(!casillas[i].GetComponent<SectionControl>().isOcuped && Vector3.Distance(target.position, casillas[i].position) <= dis2)
                {
                    ps = i;
                    dis2 = Vector3.Distance(target.position, casillas[i].position);

                    print("hi2");
                }
            }
        }

        return ps;
    }
}