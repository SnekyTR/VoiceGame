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

    public GameObject tpFX;
    public GameObject magicAura;
    public GameObject bullet;
    public GameObject inmunityAura;

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

                StartCoroutine(StartRoute(destiny));
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
            else if (CheckAlliesLife() && enemyStats.GetEnergyActions() >= 3)
            {
                StartCoroutine(InmunityShield());
            }
            else if (Vector3.Distance(transform.position, target.position) < enemyStats.GetRange())
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
            else if (Vector3.Distance(transform.position, target.position) > enemyStats.GetRange() && enemyStats.GetEnergyActions() >= 2)
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

                StartCoroutine(StartRoute(destiny));
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
        float dis1 = 10000;
        float dis2 = 10000;
        int ps = 0;

        for (int i = 0; i < casillas.Count; i++)
        {
            if ((Vector3.Distance(transform.position, casillas[i].position) / 2) <= enemyStats.GetEnergy())
            {
                if (Vector3.Distance(target.position, casillas[i].position) <= enemyStats.GetRange() && !casillas[i].GetComponent<SectionControl>().isOcuped
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
                        }
                    }
                }
                else if (!casillas[i].GetComponent<SectionControl>().isOcuped && Vector3.Distance(target.position, casillas[i].position) <= dis2)
                {
                    ps = i;
                    dis2 = Vector3.Distance(target.position, casillas[i].position);
                }
            }
        }

        return ps;
    }

    private IEnumerator StartRoute(int i)
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("A_AutoBuff", 0);
        yield return new WaitForSeconds(0.4f);
        Destroy(Instantiate(tpFX, transform.position, transform.rotation), 1.5f);

        yield return new WaitForSeconds(0.3f);
        transform.position = casillas[i].position;

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
        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("A_BasicAtk", 0);
        yield return new WaitForSeconds(0.3f);

        GameObject obj = Instantiate(bullet, transform.position, transform.rotation);
        obj.GetComponent<BulletDmg>().dmg = enemyStats.GetAtk();
        obj.GetComponent<BulletDmg>().target = target;

        yield return new WaitForSeconds(0.6f);

        enemyStats.SetEnergyAction(-2);
        yield return new WaitForSeconds(1f);
        setTarget = false;
        enemyStats.canv.EnemyC(true);
        if (playerUseMagic) StatesManager2();
        else StatesManager();
    }

    private IEnumerator GiantForm()
    {
        animator.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.3f);
        animator.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1.2f);

        GetComponent<EnemyStats>().AddAura(Instantiate(magicAura, transform.position, transform.rotation, transform));

        enemyStats.SetDmg((int)(enemyStats.GetAtk() * 1.2f));
        enemyStats.SetEnergyAction(-1);
        StatesManager2();
        isBuffed = true;
    }

    private bool CheckAlliesLife()
    {
        for(int i = 0; i < gameM.enemys.Count; i++)
        {
            float e = gameM.enemys[i].GetComponent<EnemyStats>().GetLife() / gameM.enemys[i].GetComponent<EnemyStats>().maxLife;

            if (e <= 0.5f)
            {
                if (gameM.enemys[i].GetComponent<EnemyStats>().inmunity == true)
                {

                }
                else return true;
            }
        }

        return false;
    }

    private IEnumerator InmunityShield()
    {
        for (int i = 0; i < gameM.enemys.Count; i++)
        {
            float e = gameM.enemys[i].GetComponent<EnemyStats>().GetLife() / gameM.enemys[i].GetComponent<EnemyStats>().maxLife;

            if (e <= 0.5f)
            {
                target = gameM.enemys[i].transform;
            }
        }

        setTarget = true;

        yield return new WaitForSeconds(0.6f);

        animator.SetInteger("A_Shield", 1);

        yield return new WaitForSeconds(0.2f);

        animator.SetInteger("A_Shield", 0);

        yield return new WaitForSeconds(1.2f);

        setTarget = false;

        target.GetComponent<EnemyStats>().inmunity = true;

        target.GetComponent<EnemyStats>().AddAura(Instantiate(inmunityAura, target.transform.position, transform.rotation, target.transform));

        yield return new WaitForSeconds(0.8f);

        enemyStats.SetEnergyAction(-3);
        StarIA();
    }
}
