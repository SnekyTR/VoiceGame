using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skills : MonoBehaviour
{
    private PlayerMove plyMove;
    private CameraFollow gameM;
    private Animator plyAnim;
    private PlayerStats plyStats;

    private List<string> nameSkill = new List<string>();
    private List<string> weapons = new List<string>();

    private string actualWeapon;

    public GameObject blood;
    public GameObject buffFX01;
    public GameObject buffFX02;
    public GameObject buffFX03;
    public GameObject demaciaFX;
    public GameObject arrowRain;
    public GameObject fireBall;
    public GameObject fireBall2;
    public GameObject sacredBall;
    public GameObject meteorRain;
    public GameObject healFX;

    [Header("Areas")]
    public Transform slashArea;
    public Transform demaciaArea;

    private void Awake()
    {
        nameSkill.Add("Partir");
        nameSkill.Add("Aumento de fuerza");
        nameSkill.Add("Demacia");
        nameSkill.Add("Demolicion");
        nameSkill.Add("Instinto asesino");
        nameSkill.Add("Lluvia de flechas");
        nameSkill.Add("Bola de fuego");
        nameSkill.Add("Sacrificio de sangre");
        nameSkill.Add("Lluvia de meteoritos");
        nameSkill.Add("Curar");
        nameSkill.Add("Revivir");
        nameSkill.Add("Juicio final");

        weapons.Add("sword");
        weapons.Add("axe");
        weapons.Add("spear");
        weapons.Add("bow");
        weapons.Add("fire staff");
        weapons.Add("sacred staff");
    }

    void Start()
    {
        plyMove = GetComponent<PlayerMove>();
        gameM = GetComponent<CameraFollow>();
    }

    void Update()
    {
        string n = nameSkill[0];
    }

    public void SetActualPlayer(PlayerStats pl)
    {
        plyStats = pl;
        actualWeapon = plyStats.actualWeapon;
        plyAnim = plyStats.GetComponent<Animator>();
    }

    public List<string> GetSkillList()
    {
        return nameSkill;
    }

    public bool ValidationSkill(string n)
    {
        if (plyStats.strengthPoints >= 6 && nameSkill[0] == n && actualWeapon == weapons[0]) return true;
        if (plyStats.strengthPoints >= 6 && nameSkill[0] == n && actualWeapon == weapons[1]) return true;
        else if (plyStats.strengthPoints >= 8 && nameSkill[1] == n && actualWeapon == weapons[0]) return true;
        else if (plyStats.strengthPoints >= 8 && nameSkill[1] == n && actualWeapon == weapons[1]) return true;
        else if (plyStats.strengthPoints >= 10 && nameSkill[2] == n && actualWeapon == weapons[0]) return true;
        else if (plyStats.strengthPoints >= 10 && nameSkill[2] == n && actualWeapon == weapons[1]) return true;
        else if (plyStats.agilityPoints >= 6 && nameSkill[3] == n && actualWeapon == weapons[2]) return true;
        else if (plyStats.agilityPoints >= 6 && nameSkill[3] == n && actualWeapon == weapons[3]) return true;
        else if (plyStats.agilityPoints >= 8 && nameSkill[4] == n && actualWeapon == weapons[2]) return true;
        else if (plyStats.agilityPoints >= 8 && nameSkill[4] == n && actualWeapon == weapons[3]) return true;
        else if (plyStats.agilityPoints >= 10 && nameSkill[5] == n && actualWeapon == weapons[2]) return true;
        else if (plyStats.agilityPoints >= 10 && nameSkill[5] == n && actualWeapon == weapons[3]) return true;
        else if (plyStats.intellectPoints >= 6 && nameSkill[6] == n && actualWeapon == weapons[4]) return true;
        else if (plyStats.intellectPoints >= 8 && nameSkill[7] == n && actualWeapon == weapons[4]) return true;
        else if (plyStats.intellectPoints >= 10 && nameSkill[8] == n && actualWeapon == weapons[4]) return true;
        else if (plyStats.intellectPoints >= 6 && nameSkill[9] == n && actualWeapon == weapons[5]) return true;
        else if (plyStats.intellectPoints >= 8 && nameSkill[10] == n && actualWeapon == weapons[5]) return true;
        else if (plyStats.intellectPoints >= 10 && nameSkill[11] == n && actualWeapon == weapons[5]) return true;
        else return false;

    }               

    public int GetRanges(string e)
    {
        switch (e)
        {
            case "Partir":
                return 3;
            case "Aumento de fuerza":
                return 10000;
            case "Demacia":
                return 6;
            case "Demolicion":
                if (actualWeapon == weapons[2]) return 5;
                else return 12;
            case "Instinto asesino":
                return 10000;
            case "Lluvia de flechas":
                return 21;
            case "Bola de fuego":
                return 13;
            case "Sacrificio de sangre":
                return 10000;
            case "Lluvia de meteoritos":
                return 21;
            case "Curar":
                return 12;
            case "Revivir":
                return 3;
            case "Juicio final":
                return 21;
            default:
                if (actualWeapon == weapons[0]) return 3;
                else if (actualWeapon == weapons[1]) return 4;
                else if (actualWeapon == weapons[2]) return 6;
                else if (actualWeapon == weapons[3]) return 14;
                else if (actualWeapon == weapons[4]) return 12;
                else if (actualWeapon == weapons[5]) return 12;
                return 3;
        }
    }                       

    public float GetCost(string e)
    {
        switch (e)
        {
            case "Partir":
                if (actualWeapon == weapons[0]) return 2f;
                else if (actualWeapon == weapons[1]) return 3.5f;
                return 3f;
            case "Aumento de fuerza":
                return 1.5f;
            case "Demacia":
                if (actualWeapon == weapons[0]) return 3.5f;
                else if (actualWeapon == weapons[1]) return 4f;
                return 3.5f;
            case "Demolicion":
                return 2.5f;
            case "Instinto asesino":
                return 1.5f;
            case "Lluvia de flechas":
                return 3.5f;
            case "Bola de fuego":
                return 2.5f;
            case "Sacrificio de sangre":
                return 3f;
            case "Lluvia de meteoritos":
                return 5f;
            case "Curar":
                return 3f;
            case "Revivir":
                return 4f;
            case "Juicio final":
                return 3.5f;
            default:
                if (actualWeapon == weapons[0]) return 2f;
                else if (actualWeapon == weapons[1]) return 2.5f;
                else if (actualWeapon == weapons[2]) return 1.5f;
                else if (actualWeapon == weapons[3]) return 2f;
                else if (actualWeapon == weapons[4]) return 1.5f;
                else if (actualWeapon == weapons[5]) return 1f;
                return 2f;
        }
    }                       

    public void SelectSkill(string e)
    {
        switch (e)
        {
            case "Partir":
                StartCoroutine(Cleave());
                break;
            case "Aumento de fuerza":
                StartCoroutine(StrenghtBuff());
                break;
            case "Demacia":
                StartCoroutine(Demacia());
                break;
            case "Demolicion":
                StartCoroutine(Demolition());
                break;
            case "Instinto asesino":
                StartCoroutine(CriticalBuff());
                break;
            case "Lluvia de flechas":
                StartCoroutine(ArrowRain());
                break;
            case "Bola de fuego":
                StartCoroutine(FireBall());
                break;
            case "Sacrificio de sangre":
                StartCoroutine(BloodSacrifice());
                break;
            case "Lluvia de meteoritos":
                StartCoroutine(MeteorAtk());
                break;
            case "Curar":
                StartCoroutine(Heal());
                break;
            case "Revivir":
                StartCoroutine(Resurrect());
                break;
            case "Juicio final":
                StartCoroutine(FinalJudgment());
                break;
            default:
                StartCoroutine(BasicAtk());
                break;

        }
    }

    private IEnumerator BasicAtk()
    {
        bool isBlood = true;

        if (actualWeapon == weapons[0])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetStrenght() * 0.9f), (int)(plyStats.GetStrenght() * 1.3f));

            int crit = Random.Range(0, 100);

            if(crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }
        else if (actualWeapon == weapons[1])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetStrenght() * 1.2f), (int)(plyStats.GetStrenght() * 1.6f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }
        else if (actualWeapon == weapons[2])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetAgility() * 1f), (int)(plyStats.GetAgility() * 1.2f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }
        else if (actualWeapon == weapons[3])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetAgility() * 0.8f), (int)(plyStats.GetAgility() * 1.5f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }
        else if (actualWeapon == weapons[4])
        {
            isBlood = false;
            plyAnim.SetInteger("A_Magic", 2);
            yield return new WaitForSeconds(0.2f);
            plyAnim.SetInteger("A_Magic", 0);

            yield return new WaitForSeconds(0.8f);

            Vector3 newPos = gameM.playerParent.transform.position;

            newPos.y += 1;

            Vector3 direction = gameM.playerParent.transform.position - plyMove.target.transform.position;

            Quaternion rotacion = Quaternion.LookRotation(direction);

            GameObject fb = Instantiate(fireBall2, newPos, rotacion);

            Destroy(fb, 2);
        }
        else if (actualWeapon == weapons[5])
        {
            isBlood = false;
            plyAnim.SetInteger("A_Magic", 2);
            yield return new WaitForSeconds(0.2f);
            plyAnim.SetInteger("A_Magic", 0);

            yield return new WaitForSeconds(0.8f);

            Vector3 newPos = gameM.playerParent.transform.position;

            newPos.y += 1;

            Vector3 direction = gameM.playerParent.transform.position - plyMove.target.transform.position;

            Quaternion rotacion = Quaternion.LookRotation(direction);

            GameObject fb = Instantiate(sacredBall, newPos, rotacion);

            Destroy(fb, 2);
        }

        if (isBlood)
        {
            GameObject h = Instantiate(blood, plyMove.target.transform.position, transform.rotation);

            yield return new WaitForSeconds(1.2f);

            Destroy(h);
        }
    }

    private IEnumerator Cleave()
    {
        plyAnim.SetInteger("A_Slash", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_Slash", 0);

        yield return new WaitForSeconds(0.7f);
        Vector3 newPos = plyMove.target.transform.position;

        slashArea.gameObject.SetActive(true);
        slashArea.position = newPos;
        slashArea.rotation = gameM.playerParent.rotation;

        yield return new WaitForSeconds(0.5f);
        slashArea.gameObject.SetActive(false);
    }

    private IEnumerator StrenghtBuff()
    {
        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);

        Instantiate(buffFX01, gameM.playerParent.position, transform.rotation);

        plyStats.SetStrenght(1.25f);
    }

    private IEnumerator Demacia()
    {
        plyAnim.SetInteger("A_Demacia", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_Demacia", 0);

        yield return new WaitForSeconds(0.6f);

        Vector3 newPos = plyMove.target.transform.position;

        demaciaArea.gameObject.SetActive(true);
        demaciaArea.position = newPos;
        demaciaArea.rotation = gameM.playerParent.rotation;

        yield return new WaitForSeconds(2f);
        demaciaArea.gameObject.SetActive(false);
    }

    private IEnumerator Demolition()
    {
        if(actualWeapon == weapons[2])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetAgility() * 1f), (int)(plyStats.GetAgility() * 1.2f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            int dmg2 = plyMove.target.GetComponent<EnemyStats>().GetShield();
            dmg = dmg + dmg2;

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }
        else if(actualWeapon == weapons[3])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetAgility() * 0.8f), (int)(plyStats.GetAgility() * 1.5f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            int dmg2 = plyMove.target.GetComponent<EnemyStats>().GetShield();
            dmg = dmg + dmg2;

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }

        GameObject h = Instantiate(blood, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(1.2f);

        Destroy(h);
    }

    private IEnumerator CriticalBuff()
    {
        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);

        Instantiate(buffFX02, gameM.playerParent.position, transform.rotation);

        plyStats.SetAgility(1.25f);
        plyStats.MoreCriticProb(50);
    }

    private IEnumerator ArrowRain()
    {
        Instantiate(arrowRain, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(2f);
    }

    private IEnumerator FireBall()
    {
        plyAnim.SetInteger("A_Magic", 2);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);

        yield return new WaitForSeconds(0.4f);

        Vector3 newPos = gameM.playerParent.transform.position;

        newPos.y += 1;

        Vector3 direction = plyMove.target.transform.position - gameM.playerParent.transform.position;

        Quaternion rotacion = Quaternion.LookRotation(direction);

        Instantiate(fireBall, newPos, rotacion);

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator BloodSacrifice()
    {
        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);

        Instantiate(buffFX03, gameM.playerParent.position, transform.rotation);

        plyStats.SetIntellect(1.25f);
    }

    private IEnumerator MeteorAtk()
    {
        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(0.7f);
        Instantiate(meteorRain, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator Heal()
    {
        plyAnim.SetInteger("A_Magic", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_Magic", 0);

        yield return new WaitForSeconds(0.5f);

        int heal = Random.Range((int)(plyStats.GetAgility() * 0.6f), (int)(plyStats.GetAgility() * 1f));

        plyMove.target.GetComponent<PlayerStats>().SetLife(heal);

        GameObject h = Instantiate(healFX, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(1.2f);

        Destroy(h);
    }

    private IEnumerator Resurrect()
    {
        plyAnim.SetInteger("A_Magic", 3);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_Magic", 0);

        yield return new WaitForSeconds(1f);

        gameM.players.Add(plyMove.target.transform);
        plyMove.target.GetComponent<Animator>().SetInteger("A_Death", 0);
        plyMove.target.GetComponent<NavMeshAgent>().enabled = true;
        plyMove.target.GetComponent<PlayerStats>().SetLife(plyStats.GetIntellect());

        plyStats.SetIntellect(1.25f);
    }

    private IEnumerator FinalJudgment()
    {
        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(1.3f);

        Vector3 newPos = plyMove.target.transform.position;

        demaciaArea.gameObject.SetActive(true);
        demaciaArea.position = newPos;
        demaciaArea.rotation = gameM.playerParent.rotation;

        yield return new WaitForSeconds(2f);
        demaciaArea.gameObject.SetActive(false);
    }
}
