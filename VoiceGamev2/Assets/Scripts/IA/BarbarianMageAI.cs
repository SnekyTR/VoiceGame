using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarianMageAI : MonoBehaviour
{
    public bool isOnRoute, setTarget;
    private EnemyStats enemyStats;
    private NavMeshAgent enemyNM;
    private List<Transform> casillas = new List<Transform>();
    private Transform target;
    private CameraFollow gameM;
    private Animator animator;

    public bool captain;
    [HideInInspector] public bool playerUseMagic;
    private bool isBuffed;
    private bool heavyAtk;

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
            if (playerUseMagic) StatesManager2();
            else StatesManager();
            isOnRoute = false;
            //enemyNM.isStopped = true;
            animator.SetInteger("A_Movement", 0);
        }

        if (setTarget)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotacion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 5f * Time.deltaTime);
        }

        if (gameM.playerUseMagic && !playerUseMagic && captain)
        {
            playerUseMagic = true;
            for (int i = 0; i < gameM.enemys.Count; i++)
            {
                if (gameM.enemys[i].GetComponent<BarbarianBerserkAI>())
                {
                    gameM.enemys[i].GetComponent<BarbarianBerserkAI>().playerUseMagic = true;
                }
                else if (gameM.enemys[i].GetComponent<BarbarianMageAI>())
                {
                    gameM.enemys[i].GetComponent<BarbarianMageAI>().playerUseMagic = true;
                }
            }
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

        if (playerUseMagic) StatesManager2();
        else StatesManager();
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
            if (Vector3.Distance(transform.position, target.position) < enemyStats.GetRange() && enemyStats.GetEnergyActions() >= 2)
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

                transform.position = casillas[destiny].position;
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

    private void StatesManager2()
    {
        if (enemyStats.GetEnergy() > 0 || enemyStats.GetEnergyActions() > 0)
        {
            if (!isBuffed && enemyStats.GetEnergyActions() >= 1)
            {
                StartCoroutine(GiantForm());
            }
            else if (heavyAtk && enemyStats.GetEnergyActions() >= 5)
            {
                StartCoroutine(HeavyAtk());
            }
            else if (Vector3.Distance(transform.position, target.position) < enemyStats.GetRange())
            {
                if (target.GetComponent<PlayerStats>().GetLife() > 0)
                {
                    if (target.GetComponent<PlayerStats>().IsStunned())
                    {
                        if (enemyStats.GetEnergyActions() >= 2)
                        {
                            StartCoroutine(AttackAnim());
                            heavyAtk = true;
                        }
                        else
                        {
                            heavyAtk = true;
                            gameM.NextIA(GetComponent<StateManager>());
                        }
                    }
                    else
                    {
                        int rnd = Random.Range(0, 2);

                        if (rnd == 0)
                        {
                            if (enemyStats.GetEnergyActions() >= 3) StartCoroutine(GroundStun());
                            else gameM.NextIA(GetComponent<StateManager>());
                        }
                        else
                        {
                            if (enemyStats.GetEnergyActions() >= 2) StartCoroutine(AttackAnim());
                            else gameM.NextIA(GetComponent<StateManager>());
                        }
                    }
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

                transform.position = casillas[destiny].position;
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

    private IEnumerator StartRoute()
    {
        yield return new WaitForSeconds(1f);
        isOnRoute = true;
    }

    private IEnumerator AttackAnim()
    {
        setTarget = true;
        enemyStats.canv.EnemyC(false);
        yield return new WaitForSeconds(0.8f);

        enemyStats.canv.EnemyC(false);
        gameM.CameraCinematic();

        animator.SetInteger("A_BasicAtk", 1);
        yield return new WaitForSeconds(0.6f);

        RaycastHit hit;
        Vector3 newPos = transform.position;
        newPos.y += 1;
        Vector3 newDir = target.position - transform.position;
        if (Physics.Raycast(newPos, newDir, out hit, 100f, mask))
        {
            if (hit.transform.tag == "Player")
            {
                float pro = target.GetComponent<PlayerStats>().GetLife() / target.GetComponent<PlayerStats>().maxLife;

                if (pro <= 0.35f)
                {
                    target.GetComponent<PlayerStats>().SetLife(-(int)(enemyStats.GetAtk() * 1.5));
                }
                else
                {
                    target.GetComponent<PlayerStats>().SetLife(-enemyStats.GetAtk());
                }
            }
        }
        enemyStats.SetEnergyAction(-2);
        if (heavyAtk) enemyStats.SetEnergyAction(-5);
        yield return new WaitForSeconds(0.8f);
        setTarget = false;
        enemyStats.canv.EnemyC(true);
        if (playerUseMagic) StatesManager2();
        else StatesManager();
    }

    private IEnumerator GiantForm()
    {
        animator.SetInteger("A_AutoBuff", 1);

        transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("A_AutoBuff", 0);

        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        yield return new WaitForSeconds(0.9f);

        enemyStats.SetDmg((int)(enemyStats.GetAtk() * 1.2f));
        enemyStats.SetEnergyAction(-1);
        StatesManager2();
        isBuffed = true;
    }

    private IEnumerator GroundStun()
    {
        setTarget = true;
        enemyStats.canv.EnemyC(false);
        yield return new WaitForSeconds(0.8f);

        enemyStats.canv.EnemyC(false);
        gameM.CameraCinematic();

        animator.SetInteger("A_JumpAtk", 1);
        yield return new WaitForSeconds(0.6f);
        animator.SetInteger("A_JumpAtk", 0);

        yield return new WaitForSeconds(0.4f);

        RaycastHit hit;
        Vector3 newPos = transform.position;
        newPos.y += 1;
        Vector3 newDir = target.position - transform.position;
        if (Physics.Raycast(newPos, newDir, out hit, 100f, mask))
        {
            if (hit.transform.tag == "Player")
            {
                target.GetComponent<PlayerStats>().SetLife(-enemyStats.GetAtk());
                target.GetComponent<PlayerStats>().StunPlayer(true);
            }
        }
        enemyStats.SetEnergyAction(-3);
        yield return new WaitForSeconds(0.8f);
        setTarget = false;
        enemyStats.canv.EnemyC(true);
        StatesManager2();
    }

    private IEnumerator HeavyAtk()
    {
        setTarget = true;
        enemyStats.canv.EnemyC(false);
        yield return new WaitForSeconds(0.8f);

        enemyStats.canv.EnemyC(false);
        gameM.CameraCinematic();

        animator.SetInteger("A_BasicAtk", 1);
        yield return new WaitForSeconds(0.6f);

        RaycastHit hit;
        Vector3 newPos = transform.position;
        newPos.y += 1;
        Vector3 newDir = target.position - transform.position;
        if (Physics.Raycast(newPos, newDir, out hit, 100f, mask))
        {
            if (hit.transform.tag == "Player")
            {
                target.GetComponent<PlayerStats>().SetLife(-(enemyStats.GetAtk() * 2));
            }
        }
        enemyStats.SetEnergyAction(-5);
        yield return new WaitForSeconds(0.8f);
        setTarget = false;
        enemyStats.canv.EnemyC(true);
        if (playerUseMagic) StatesManager2();
        else StatesManager();

        heavyAtk = false;
    }
}
