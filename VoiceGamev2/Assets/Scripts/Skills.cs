using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    private PlayerMove plyMove;
    private CameraFollow gameM;
    private Animator plyAnim;
    private PlayerStats plyStats;
    private SkillsColocation skillCo;
    public GameObject rangeObj;

    private List<string> nameSkill = new List<string>();
    private List<string> weapons = new List<string>();

    private List<Transform> skillMagnus = new List<Transform>();
    private List<Transform> skillVagnar = new List<Transform>();
    private List<Transform> skillHammun = new List<Transform>();

    private List<int> magnusTimer = new List<int>();
    private List<float> magnusTimer2 = new List<float>();
    private List<int> vagnarTimer = new List<int>();
    private List<float> vagnarTimer2 = new List<float>();
    private List<int> hamunTimer = new List<int>();
    private List<float> hamunTimer2 = new List<float>();

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

        for(int i = 0; i < 3;i++)
        {
            magnusTimer.Add(0);
            magnusTimer2.Add(0);
            vagnarTimer.Add(0);
            vagnarTimer2.Add(0);
            hamunTimer.Add(0);
            hamunTimer2.Add(0);
        }
    }

    void Start()
    {
        plyMove = GetComponent<PlayerMove>();
        gameM = GetComponent<CameraFollow>();
        skillCo = GameObject.Find("CanvasManager").GetComponent<SkillsColocation>();

        for(int i = 0; i < 3; i++)
        {
            skillMagnus.Add(skillCo.GetSkillsMagnus()[i].parent);
            skillVagnar.Add(skillCo.GetSkillsVagnar()[i].parent);
            skillHammun.Add(skillCo.GetSkillsHammun()[i].parent);
        }
    }

    void Update()
    {
        
    }

    public void SetActualPlayer(PlayerStats pl)
    {
        plyStats = pl;
        actualWeapon = plyStats.actualWeapon;
        plyAnim = plyStats.GetComponent<Animator>();

        EliminateSkillSelection();
    }

    public List<string> GetSkillList()
    {
        return nameSkill;
    }

    public bool ValidationSkill(string n)
    {
        int n1;
        int n2;
        int n3;

        if(plyStats.name == "Magnus")
        {
            n1 = magnusTimer[0];
            n2 = magnusTimer[1];
            n3 = magnusTimer[2];
        }
        else if(plyStats.name == "Vagnar")
        {
            n1 = vagnarTimer[0];
            n2 = vagnarTimer[1];
            n3 = vagnarTimer[2];
        }
        else
        {
            n1 = hamunTimer[0];
            n2 = hamunTimer[1];
            n3 = hamunTimer[2];
        }

        if (plyStats.strengthPoints >= 6 && nameSkill[0] == n && actualWeapon == weapons[0] && n1 == 0) return true;
        if (plyStats.strengthPoints >= 6 && nameSkill[0] == n && actualWeapon == weapons[1] && n1 == 0) return true;
        else if (plyStats.strengthPoints >= 8 && nameSkill[1] == n && actualWeapon == weapons[0] && n2 == 0) return true;
        else if (plyStats.strengthPoints >= 8 && nameSkill[1] == n && actualWeapon == weapons[1] && n2 == 0) return true;
        else if (plyStats.strengthPoints >= 10 && nameSkill[2] == n && actualWeapon == weapons[0] && n3 == 0) return true;
        else if (plyStats.strengthPoints >= 10 && nameSkill[2] == n && actualWeapon == weapons[1] && n3 == 0) return true;
        else if (plyStats.agilityPoints >= 6 && nameSkill[3] == n && actualWeapon == weapons[2] && n1 == 0) return true;
        else if (plyStats.agilityPoints >= 6 && nameSkill[3] == n && actualWeapon == weapons[3] && n1 == 0) return true;
        else if (plyStats.agilityPoints >= 8 && nameSkill[4] == n && actualWeapon == weapons[2] && n2 == 0) return true;
        else if (plyStats.agilityPoints >= 8 && nameSkill[4] == n && actualWeapon == weapons[3] && n2 == 0) return true;
        else if (plyStats.agilityPoints >= 10 && nameSkill[5] == n && actualWeapon == weapons[2] && n3 == 0) return true;
        else if (plyStats.agilityPoints >= 10 && nameSkill[5] == n && actualWeapon == weapons[3] && n3 == 0) return true;
        else if (plyStats.intellectPoints >= 6 && nameSkill[6] == n && actualWeapon == weapons[4] && n1 == 0) return true;
        else if (plyStats.intellectPoints >= 8 && nameSkill[7] == n && actualWeapon == weapons[4] && n2 == 0) return true;
        else if (plyStats.intellectPoints >= 10 && nameSkill[8] == n && actualWeapon == weapons[4] && n3 == 0) return true;
        else if (plyStats.intellectPoints >= 6 && nameSkill[9] == n && actualWeapon == weapons[5] && n1 ==0) return true;
        else if (plyStats.intellectPoints >= 8 && nameSkill[10] == n && actualWeapon == weapons[5] && n2 == 0) return true;
        else if (plyStats.intellectPoints >= 10 && nameSkill[11] == n && actualWeapon == weapons[5] && n3 == 0) return true;
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

    public void ShowRanges(int e)
    {
        rangeObj.SetActive(true);

        rangeObj.transform.localScale = new Vector3((2f + e), 0.0001f, (2f + e));
    }

    public void UnShowRange()
    {
        rangeObj.SetActive(false);
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
            plyAnim.SetInteger("A_Bow", 1);

            yield return new WaitForSeconds(0.8f);

            plyAnim.SetInteger("A_Bow", 0);

            yield return new WaitForSeconds(2f);

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

    private int GetTimerSkills(int n)
    {
        if (n == 0) return 1;
        else if (n == 1) return 3;
        else if (n == 2) return 2;
        else if (n == 3) return 2;
        else if (n == 4) return 3;
        else if (n == 5) return 2;
        else if (n == 6) return 1;
        else if (n == 7) return 3;
        else if (n == 8) return 3;
        else if (n == 9) return 1;
        else if (n == 10) return 10000;
        else if (n == 11) return 2;
        else return 1;
    }

    public void SetSkillsTimers()
    {
        for (int i = 0; i < 3; i++)
        {
            if (magnusTimer[i] > 0)
            {
                magnusTimer[i] -= 1;

                skillMagnus[i].GetChild(4).GetComponent<Image>().fillAmount = (magnusTimer[i]/magnusTimer2[i]);
                skillMagnus[i].GetChild(3).GetChild(0).GetComponent<Text>().text = magnusTimer[i].ToString();

                if (magnusTimer[i] == 0)
                {
                    skillMagnus[0].GetChild(3).gameObject.SetActive(false);
                    skillMagnus[0].GetChild(4).gameObject.SetActive(false);
                }
            }

            if (vagnarTimer[i] > 0)
            {
                vagnarTimer[i] -= 1;

                skillVagnar[i].GetChild(4).GetComponent<Image>().fillAmount = (vagnarTimer[i]/vagnarTimer2[i]);
                skillVagnar[i].GetChild(3).GetChild(0).GetComponent<Text>().text = vagnarTimer[i].ToString();

                if (vagnarTimer[i] == 0)
                {
                    skillVagnar[i].GetChild(3).gameObject.SetActive(false);
                    skillVagnar[i].GetChild(4).gameObject.SetActive(false);
                }
            }

            if (hamunTimer[i] > 0)
            {
                hamunTimer[i] -= 1;

                skillHammun[i].GetChild(4).GetComponent<Image>().fillAmount = (hamunTimer[i]/hamunTimer2[i]);
                skillHammun[i].GetChild(3).GetChild(0).GetComponent<Text>().text = hamunTimer[i].ToString();

                if (hamunTimer[i] == 0)
                {
                    skillHammun[i].GetChild(3).gameObject.SetActive(false);
                    skillHammun[i].GetChild(4).gameObject.SetActive(false);
                }
            }
        }
    }

    public void EliminateSkillSelection()
    {
        skillMagnus[0].GetChild(2).gameObject.SetActive(false);
        skillMagnus[1].GetChild(2).gameObject.SetActive(false);
        skillMagnus[2].GetChild(2).gameObject.SetActive(false);

        skillVagnar[0].GetChild(2).gameObject.SetActive(false);
        skillVagnar[1].GetChild(2).gameObject.SetActive(false);
        skillVagnar[2].GetChild(2).gameObject.SetActive(false);

        skillHammun[0].GetChild(2).gameObject.SetActive(false);
        skillHammun[1].GetChild(2).gameObject.SetActive(false);
        skillHammun[2].GetChild(2).gameObject.SetActive(false);
    }

    public void SetSkillSelected(string n)      //remarca la skill seleccionada por el player
    {
        int f = 0;

        switch (n)
        {
            case "Partir":
                f = 1;
                break;
            case "Aumento de fuerza":
                f = 2;
                break;
            case "Demacia":
                f = 3;
                break;
            case "Demolicion":
                f = 1;
                break;
            case "Instinto asesino":
                f = 2;
                break;
            case "Lluvia de flechas":
                f = 3;
                break;
            case "Bola de fuego":
                f = 1;
                break;
            case "Sacrificio de sangre":
                f = 2;
                break;
            case "Lluvia de meteoritos":
                f = 3;
                break;
            case "Curar":
                f = 1;
                break;
            case "Revivir":
                f = 2;
                break;
            case "Juicio final":
                f = 3;
                break;
            default:
                break;
        }

        if(plyStats.name == "Magnus")
        {
            if (f == 1)
            {
                skillMagnus[0].GetChild(2).gameObject.SetActive(true);
                skillMagnus[1].GetChild(2).gameObject.SetActive(false);
                skillMagnus[2].GetChild(2).gameObject.SetActive(false);
            }
            else if(f == 2)
            {
                skillMagnus[0].GetChild(2).gameObject.SetActive(false);
                skillMagnus[1].GetChild(2).gameObject.SetActive(true);
                skillMagnus[2].GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                skillMagnus[0].GetChild(2).gameObject.SetActive(false);
                skillMagnus[1].GetChild(2).gameObject.SetActive(false);
                skillMagnus[2].GetChild(2).gameObject.SetActive(true);
            }
        }
        else if(plyStats.name == "Vagnar")
        {
            if (f == 1)
            {
                skillVagnar[0].GetChild(2).gameObject.SetActive(true);
                skillVagnar[1].GetChild(2).gameObject.SetActive(false);
                skillVagnar[2].GetChild(2).gameObject.SetActive(false);
            }
            else if (f == 2)
            {
                skillVagnar[0].GetChild(2).gameObject.SetActive(false);
                skillVagnar[1].GetChild(2).gameObject.SetActive(true);
                skillVagnar[2].GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                skillVagnar[0].GetChild(2).gameObject.SetActive(false);
                skillVagnar[1].GetChild(2).gameObject.SetActive(false);
                skillVagnar[2].GetChild(2).gameObject.SetActive(true);
            }
        }
        else
        {
            if (f == 1)
            {
                skillHammun[0].GetChild(2).gameObject.SetActive(true);
                skillHammun[1].GetChild(2).gameObject.SetActive(false);
                skillHammun[2].GetChild(2).gameObject.SetActive(false);
            }
            else if (f == 2)
            {
                skillHammun[0].GetChild(2).gameObject.SetActive(false);
                skillHammun[1].GetChild(2).gameObject.SetActive(true);
                skillHammun[2].GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                skillHammun[0].GetChild(2).gameObject.SetActive(false);
                skillHammun[1].GetChild(2).gameObject.SetActive(false);
                skillHammun[2].GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    private void SkillSelection(string n, int a, int b)     //empieza los timers de las habilidades usadas
    {
        if(n == "Magnus")
        {
            if(a == 1)
            {
                magnusTimer[0] = b;
                magnusTimer2[0] = b;

                skillMagnus[0].GetChild(3).gameObject.SetActive(true);
                skillMagnus[0].GetChild(4).gameObject.SetActive(true);

                skillMagnus[0].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillMagnus[0].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
            else if (a == 2)
            {
                magnusTimer[1] = b;
                magnusTimer2[1] = b;

                skillMagnus[1].GetChild(3).gameObject.SetActive(true);
                skillMagnus[1].GetChild(4).gameObject.SetActive(true);

                skillMagnus[1].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillMagnus[1].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
            else
            {
                magnusTimer[2] = b;
                magnusTimer2[2] = b;

                skillMagnus[2].GetChild(3).gameObject.SetActive(true);
                skillMagnus[2].GetChild(4).gameObject.SetActive(true);

                skillMagnus[2].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillMagnus[2].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
        }
        else if (n == "Vagnar")
        {
            if (a == 1)
            {
                vagnarTimer[0] = b;
                vagnarTimer2[0] = b;

                skillVagnar[0].GetChild(3).gameObject.SetActive(true);
                skillVagnar[0].GetChild(4).gameObject.SetActive(true);

                skillVagnar[0].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillVagnar[0].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
            else if (a == 2)
            {
                vagnarTimer[1] = b;
                vagnarTimer2[1] = b;

                skillVagnar[1].GetChild(3).gameObject.SetActive(true);
                skillVagnar[1].GetChild(4).gameObject.SetActive(true);

                skillVagnar[1].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillVagnar[1].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
            else
            {
                vagnarTimer[2] = b;
                vagnarTimer2[2] = b;

                skillVagnar[2].GetChild(3).gameObject.SetActive(true);
                skillVagnar[2].GetChild(4).gameObject.SetActive(true);

                skillVagnar[2].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillVagnar[2].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
        }
        else
        {
            if (a == 1)
            {
                hamunTimer[0] = b;
                hamunTimer2[0] = b;

                skillHammun[0].GetChild(3).gameObject.SetActive(true);
                skillHammun[0].GetChild(4).gameObject.SetActive(true);

                skillHammun[0].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillHammun[0].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
            else if (a == 2)
            {
                hamunTimer[1] = b;
                hamunTimer2[1] = b;

                skillHammun[1].GetChild(3).gameObject.SetActive(true);
                skillHammun[1].GetChild(4).gameObject.SetActive(true);

                skillHammun[1].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillHammun[1].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
            else
            {
                hamunTimer[2] = b;
                hamunTimer2[2] = b;

                skillHammun[2].GetChild(3).gameObject.SetActive(true);
                skillHammun[2].GetChild(4).gameObject.SetActive(true);

                skillHammun[2].GetChild(4).GetComponent<Image>().fillAmount = 1;
                skillHammun[2].GetChild(3).GetChild(0).GetComponent<Text>().text = b.ToString();
            }
        }
    }

    private IEnumerator Cleave()
    {
        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(0));

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
        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(1));

        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);

        Instantiate(buffFX01, gameM.playerParent.position, transform.rotation);

        plyStats.SetStrenght(1.25f);
    }

    private IEnumerator Demacia()
    {
        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(2));

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
        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(3));

        if (actualWeapon == weapons[2])
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
            plyAnim.SetInteger("A_Bow", 1);

            yield return new WaitForSeconds(0.8f);

            plyAnim.SetInteger("A_Bow", 0);

            yield return new WaitForSeconds(2f);

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
        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(4));

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
        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(1.3f);
        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(5));

        Instantiate(arrowRain, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(1.6f);
    }

    private IEnumerator FireBall()
    {
        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(6));

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
        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(7));

        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);

        Instantiate(buffFX03, gameM.playerParent.position, transform.rotation);

        plyStats.SetIntellect(1.25f);
    }

    private IEnumerator MeteorAtk()
    {
        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(8));

        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(0.7f);
        Instantiate(meteorRain, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator Heal()
    {
        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(9));

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
        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(10));

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
        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(11));

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
