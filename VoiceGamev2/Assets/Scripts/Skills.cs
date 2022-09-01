using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] private Transform arrowStart;
    private PlayerMove plyMove;
    private CameraFollow gameM;
    private Animator plyAnim;
    [SerializeField]
    private HelpPannel helpPannel;
    private PlayerStats plyStats;
    private SkillsColocation skillCo;
    public AudioSource audioSource;

    private List<string> nameSkill = new List<string>();
    private List<string> weapons = new List<string>();
    private List<GameObject> ranges = new List<GameObject>();

    private List<Transform> skillMagnus = new List<Transform>();
    private List<Transform> skillVagnar = new List<Transform>();
    private List<Transform> skillHammun = new List<Transform>();
    [SerializeField]public List<AudioClip> attacksFX = new List<AudioClip>();

    private List<int> magnusTimer = new List<int>();
    private List<float> magnusTimer2 = new List<float>();
    private List<int> vagnarTimer = new List<int>();
    private List<float> vagnarTimer2 = new List<float>();
    private List<int> hamunTimer = new List<int>();
    private List<float> hamunTimer2 = new List<float>();
    private CameraFollow cameraFollow;

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
    public GameObject rangeFX;
    public GameObject finalJ;

    [Header("Areas")]
    public Transform slashArea;
    public Transform demaciaArea;

    int mask;

    private void Awake()
    {
        nameSkill.Add("Partir");
        nameSkill.Add("Aumento de fuerza");
        nameSkill.Add("Demacia");
        nameSkill.Add("Demolicion");
        nameSkill.Add("Instinto asesino");
        nameSkill.Add("Lluvia de flechas");
        nameSkill.Add("Bola de fuego");
        nameSkill.Add("Sacrificio");
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

        mask = 1 << LayerMask.NameToLayer("Occlude");
        mask |= 1 << LayerMask.NameToLayer("Enemy");
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        plyMove = GetComponent<PlayerMove>();
        gameM = GetComponent<CameraFollow>();
        skillCo = GameObject.Find("CanvasManager").GetComponent<SkillsColocation>();
        helpPannel = skillCo.gameObject.GetComponent<HelpPannel>();
        cameraFollow = GetComponent<CameraFollow>();

        for (int i = 0; i < 3; i++)
        {
            skillMagnus.Add(skillCo.GetSkillsMagnus()[i].parent);
            skillVagnar.Add(skillCo.GetSkillsVagnar()[i].parent);
            skillHammun.Add(skillCo.GetSkillsHammun()[i].parent);
        }
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
                else return 14;
            case "Instinto asesino":
                return 10000;
            case "Lluvia de flechas":
                return 21;
            case "Bola de fuego":
                return 13;
            case "Sacrificio":
                return 10000;
            case "Lluvia de meteoritos":
                return 21;
            case "Curar":
                return 10;
            case "Revivir":
                return 5;
            case "Juicio final":
                return 21;
            default:
                if (actualWeapon == weapons[0]) return 3;
                else if (actualWeapon == weapons[1]) return 4;
                else if (actualWeapon == weapons[2]) return 6;
                else if (actualWeapon == weapons[3]) return 14;
                else if (actualWeapon == weapons[4]) return 12;
                else if (actualWeapon == weapons[5]) return 10;
                return 3;
        }
    }                       

    public void ShowRanges(int e)
    {
        for(int i = 0;i < gameM.enemys.Count; i++)
        {
            if(Vector3.Distance(gameM.enemys[i].transform.position, gameM.playerParent.position) <= e)
            {
                RaycastHit hit;
                Vector3 newPos = plyStats.transform.position;
                newPos.y += 1;
                Vector3 newDir = gameM.enemys[i].transform.position - plyStats.transform.position;
                if (Physics.Raycast(newPos, newDir, out hit, 1000f, mask))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        Vector3 newPos2 = gameM.enemys[i].transform.position;
                        newPos2.y += 0.6f;

                        GameObject rg = Instantiate(rangeFX, newPos2, transform.rotation);
                        ranges.Add(rg);
                    }
                }
            }
        }
    }

    public void ShowRangesAllie(int e)
    {
        for (int i = 0; i < gameM.players.Count; i++)
        {
            if (Vector3.Distance(gameM.players[i].position, gameM.playerParent.position) <= e)
            {
                Vector3 newPos = gameM.players[i].position;
                newPos.y += 0.6f;

                GameObject rg = Instantiate(rangeFX, newPos, transform.rotation);
                ranges.Add(rg);
            }
        }
    }

    public void UnShowRange()
    {
        for(int i = 0;i < ranges.Count; i++)
        {
            Destroy(ranges[i]);
        }

        ranges = new List<GameObject>();
    }

    public float GetCost(string e)
    {
        switch (e)
        {
            case "Partir":
                return 3f;
            case "Aumento de fuerza":
                return 1.5f;
            case "Demacia":
                return 4f;
            case "Demolicion":
                return 2.5f;
            case "Instinto asesino":
                return 1.5f;
            case "Lluvia de flechas":
                return 3.5f;
            case "Bola de fuego":
                return 2.5f;
            case "Sacrificio":
                return 1.5f;
            case "Lluvia de meteoritos":
                return 5f;
            case "Curar":
                return 3f;
            case "Revivir":
                return 5f;
            case "Juicio final":
                return 4f;
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
            case "Sacrificio":
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
            gameM.CameraSkillPlayer(1);

            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);
            audioSource.clip = attacksFX[0];
            audioSource.volume = 0.8f;
            audioSource.Play();

            int dmg = Random.Range((int)(plyStats.GetStrenght() * 0.9f), (int)(plyStats.GetStrenght() * 1.3f));

            int crit = Random.Range(0, 100);

            bool c = false;

            if(crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);

                c = true;
            }

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg, c);
        }
        else if (actualWeapon == weapons[1])
        {
            gameM.CameraSkillPlayer(1);

            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetStrenght() * 1.2f), (int)(plyStats.GetStrenght() * 1.6f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            //plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
        }
        else if (actualWeapon == weapons[2])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);

            yield return new WaitForSeconds(0.7f);

            int dmg = Random.Range((int)(plyStats.GetAgility() * 1f), (int)(plyStats.GetAgility() * 1.2f));

            int crit = Random.Range(0, 100);

            bool c = false;

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);

                c = true;
            }

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg, c);
        }
        else if (actualWeapon == weapons[3])
        {
            gameM.CameraSkillPlayer(2);

            plyAnim.SetInteger("A_Bow", 1);
            yield return new WaitForSeconds(1f);
            GameObject go =  Instantiate(arrowPrefab);
            go.transform.parent = arrowStart;
            go.transform.localPosition = new Vector3(-4.2f, 38f, 1);
            go.transform.localRotation = Quaternion.Euler(0, 0, 0);
            go.transform.localScale = new Vector3(266.863f, 266.863f, 266.863f);

            yield return new WaitForSeconds(1f);
            audioSource.clip = attacksFX[4];
            audioSource.volume = 0.09f;
            audioSource.Play();
            
            yield return new WaitForSeconds(0.8f);

            plyAnim.SetInteger("A_Bow", 0);

            yield return new WaitForSeconds(0.3f);
            audioSource.clip = attacksFX[5];
            audioSource.volume = 0.17f;
            audioSource.Play();

            int dmg = Random.Range((int)(plyStats.GetAgility() * 0.8f), (int)(plyStats.GetAgility() * 1.5f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            float dis = Vector3.Distance(plyStats.transform.position, plyMove.target.transform.position);

            if(dis >= 7)
            {
                dis -= 6;

                dis = dis / 10;

                dis = 1 - dis;

                dmg = (int)(dmg * dis);
            }

            isBlood = false;

            RaycastHit hit;
            Vector3 newPos = plyStats.transform.position;
            newPos.y += 1;
            Vector3 newDir = plyMove.target.transform.position - plyStats.transform.position;
            
            if (Physics.Raycast(newPos, newDir, out hit, 1000f, mask))
            {
                
                print(hit.transform.name);
                if (hit.transform.tag == "Enemy")
                {
                    go.transform.parent = null;
                    go.transform.position = Vector3.MoveTowards(go.transform.position, hit.transform.position, 0.5f);
                    Destroy(go);
                    //hit.transform.GetComponent<EnemyStats>().SetLife(-dmg);
                    Destroy(Instantiate(blood, plyMove.target.transform.position, transform.rotation), 2);
                    
                }
            }
            yield return new WaitForSeconds(0.6f);
            audioSource.volume = 1f;
        }
        else if (actualWeapon == weapons[4])
        {
            gameM.CameraSkillPlayer(2);

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
            gameM.CameraSkillPlayer(2);

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
            if(plyMove.target.GetComponent<SkeletonArcherAI>() || plyMove.target.GetComponent<SkeletonGuardianAI>())
            {

            }
            else
            {
                Destroy(Instantiate(blood, plyMove.target.transform.position, transform.rotation), 1.2f);
            }

            yield return new WaitForSeconds(1.2f);
        }

        gameM.CameraPos1();
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

                if(hamunTimer[i] > 20)
                {
                    skillHammun[i].GetChild(4).GetComponent<Image>().fillAmount = 1;
                    skillHammun[i].GetChild(3).GetChild(0).GetComponent<Text>().text = "-";
                }
                else
                {
                    skillHammun[i].GetChild(4).GetComponent<Image>().fillAmount = (hamunTimer[i] / hamunTimer2[i]);
                    skillHammun[i].GetChild(3).GetChild(0).GetComponent<Text>().text = hamunTimer[i].ToString();
                }

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
        skillMagnus[0].GetChild(1).gameObject.SetActive(false);
        skillMagnus[1].GetChild(1).gameObject.SetActive(false);
        skillMagnus[2].GetChild(1).gameObject.SetActive(false);

        skillVagnar[0].GetChild(1).gameObject.SetActive(false);
        skillVagnar[1].GetChild(1).gameObject.SetActive(false);
        skillVagnar[2].GetChild(1).gameObject.SetActive(false);

        skillHammun[0].GetChild(1).gameObject.SetActive(false);
        skillHammun[1].GetChild(1).gameObject.SetActive(false);
        skillHammun[2].GetChild(1).gameObject.SetActive(false);
    }

    public void SetSkillSelected(string n)      //remarca la skill seleccionada por el player
    {
        int f = 0;
        helpPannel.AttackState();
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
            case "Sacrificio":
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
                skillMagnus[0].GetChild(1).gameObject.SetActive(true);
                skillMagnus[1].GetChild(1).gameObject.SetActive(false);
                skillMagnus[2].GetChild(1).gameObject.SetActive(false);
            }
            else if(f == 2)
            {
                skillMagnus[0].GetChild(1).gameObject.SetActive(false);
                skillMagnus[1].GetChild(1).gameObject.SetActive(true);
                skillMagnus[2].GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                skillMagnus[0].GetChild(1).gameObject.SetActive(false);
                skillMagnus[1].GetChild(1).gameObject.SetActive(false);
                skillMagnus[2].GetChild(1).gameObject.SetActive(true);
            }
        }
        else if(plyStats.name == "Vagnar")
        {
            if (f == 1)
            {
                skillVagnar[0].GetChild(1).gameObject.SetActive(true);
                skillVagnar[1].GetChild(1).gameObject.SetActive(false);
                skillVagnar[2].GetChild(1).gameObject.SetActive(false);
            }
            else if (f == 2)
            {
                skillVagnar[0].GetChild(1).gameObject.SetActive(false);
                skillVagnar[1].GetChild(1).gameObject.SetActive(true);
                skillVagnar[2].GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                skillVagnar[0].GetChild(1).gameObject.SetActive(false);
                skillVagnar[1].GetChild(1).gameObject.SetActive(false);
                skillVagnar[2].GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            if (f == 1)
            {
                skillHammun[0].GetChild(1).gameObject.SetActive(true);
                skillHammun[1].GetChild(1).gameObject.SetActive(false);
                skillHammun[2].GetChild(1).gameObject.SetActive(false);
            }
            else if (f == 2)
            {
                skillHammun[0].GetChild(1).gameObject.SetActive(false);
                skillHammun[1].GetChild(1).gameObject.SetActive(true);
                skillHammun[2].GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                skillHammun[0].GetChild(1).gameObject.SetActive(false);
                skillHammun[1].GetChild(1).gameObject.SetActive(false);
                skillHammun[2].GetChild(1).gameObject.SetActive(true);
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

                if (b > 20)
                {
                    skillHammun[1].GetChild(3).GetChild(0).GetComponent<Text>().text = "-";
                }
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
        gameM.CameraSkillPlayer(1);

        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(0));

        plyAnim.SetInteger("A_Slash", 1);

        yield return new WaitForSeconds(0.2f);


        plyAnim.SetInteger("A_Slash", 0);
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = attacksFX[1];
        audioSource.volume = 0.8f;
        audioSource.Play();

        yield return new WaitForSeconds(0.2f);
        Vector3 newPos = plyMove.target.transform.position;

        slashArea.gameObject.SetActive(true);
        slashArea.position = newPos;
        slashArea.rotation = gameM.playerParent.rotation;

        yield return new WaitForSeconds(0.5f);
        slashArea.gameObject.SetActive(false);

        gameM.CameraPos1();
    }

    private IEnumerator StrenghtBuff()
    {
        gameM.CameraSkillPlayer(3);

        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(1));

        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);
        audioSource.clip = attacksFX[2];
        audioSource.volume = 0.15f;
        audioSource.Play();
        

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);
        
        GameObject buff = Instantiate(buffFX01, gameM.playerParent.position, transform.rotation);

        buff.transform.SetParent(gameM.playerParent);

        plyStats.SetStrenght(1.25f);

        gameM.CameraPos1();
        yield return new WaitForSeconds(1.5f);
        audioSource.volume = 1;
    }

    private IEnumerator Demacia()
    {
        gameM.CameraSkillPlayer(1);

        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(2));

        plyAnim.SetInteger("A_Demacia", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_Demacia", 0);
        yield return new WaitForSeconds(0.05f);
        audioSource.clip = attacksFX[3];
        audioSource.Play();
        yield return new WaitForSeconds(0.55f);

        Vector3 newPos = plyMove.target.transform.position;

        demaciaArea.gameObject.SetActive(true);
        demaciaArea.position = newPos;
        demaciaArea.rotation = gameM.playerParent.rotation;

        yield return new WaitForSeconds(2f);
        demaciaArea.gameObject.SetActive(false);

        gameM.CameraPos1();
    }

    private IEnumerator Demolition()
    {
        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(3));

        if (actualWeapon == weapons[2])
        {
            plyAnim.SetInteger("A_BasicAtk", 1);


            yield return new WaitForSeconds(0.1f);



            yield return new WaitForSeconds(0.6f);

            int dmg = Random.Range((int)(plyStats.GetAgility() * 1f), (int)(plyStats.GetAgility() * 1.2f));

            int crit = Random.Range(0, 100);

            bool c = false;

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);

                c = true;
            }

            int dmg2 = plyMove.target.GetComponent<EnemyStats>().GetShield();
            dmg = dmg + dmg2;

            plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg,c);
        }
        else if(actualWeapon == weapons[3])
        {
            gameM.CameraSkillPlayer(2);

            plyAnim.SetInteger("A_Bow", 1);
            yield return new WaitForSeconds(1f);
            GameObject go = Instantiate(arrowPrefab);
            go.transform.parent = arrowStart;
            go.transform.localPosition = new Vector3(-4.2f, 38f, 1);
            go.transform.localRotation = Quaternion.Euler(0, 0, 0);
            go.transform.localScale = new Vector3(266.863f, 266.863f, 266.863f);
            yield return new WaitForSeconds(1f);
            audioSource.clip = attacksFX[4];
            audioSource.Play();
            yield return new WaitForSeconds(0.8f);


            plyAnim.SetInteger("A_Bow", 0);

            yield return new WaitForSeconds(0.3f);
            audioSource.clip = attacksFX[6];
            audioSource.volume = 0.50f;
            audioSource.Play();
            audioSource.volume = 1;

            int dmg = Random.Range((int)(plyStats.GetAgility() * 0.5f), (int)(plyStats.GetAgility() * 0.8f));

            int crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            int dmg2 = plyMove.target.GetComponent<EnemyStats>().GetShield();
            dmg = dmg + dmg2;

            RaycastHit hit;
            Vector3 newPos = plyStats.transform.position;
            newPos.y += 1;
            Vector3 newDir = plyMove.target.transform.position - plyStats.transform.position;
            if (Physics.Raycast(newPos, newDir, out hit, 1000f, mask))
            {
                print(hit.transform.name);
                if (hit.transform.tag == "Enemy")
                {
                    go.transform.parent = null;
                    go.transform.position = Vector3.MoveTowards(go.transform.position, hit.transform.position, 0.5f);
                    Destroy(go);
                    //hit.transform.GetComponent<EnemyStats>().SetLife(-dmg);
                    //plyMove.target.GetComponent<EnemyStats>().SetLife(-dmg);
                    Destroy(Instantiate(blood, plyMove.target.transform.position, transform.rotation), 2);
                }
            }
        }
        yield return new WaitForSeconds(1.2f);

        gameM.CameraPos1();
    }

    private IEnumerator CriticalBuff()
    {
        gameM.CameraSkillPlayer(3);

        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(4));

        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);
        audioSource.clip = attacksFX[2];
        audioSource.volume = 0.15f;
        audioSource.Play();
        audioSource.volume = 1;

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1f);

        GameObject buff = Instantiate(buffFX02, gameM.playerParent.position, transform.rotation);

        buff.transform.SetParent(gameM.playerParent);

        plyStats.SetAgility(1.25f);
        plyStats.MoreCriticProb(50);

        gameM.CameraPos1();
    }

    private IEnumerator ArrowRain()
    {
        gameM.CameraSkillPlayer(4);

        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(1.3f);
        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(5));

        Instantiate(arrowRain, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(2.5f);

        gameM.CameraPos1();
    }

    private IEnumerator FireBall()
    {
        gameM.CameraSkillPlayer(2);

        SkillSelection(gameM.playerParent.name, 1, GetTimerSkills(6));

        plyAnim.SetInteger("A_Magic", 2);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);

        yield return new WaitForSeconds(0.4f);

        Vector3 newPos = gameM.playerParent.transform.position;

        newPos.y += 1.5f;

        Vector3 direction = plyMove.target.transform.position - gameM.playerParent.transform.position;

        Quaternion rotacion = Quaternion.LookRotation(direction);

        Instantiate(fireBall, newPos, rotacion);

        yield return new WaitForSeconds(2.2f);

        gameM.CameraPos1();
    }

    private IEnumerator BloodSacrifice()
    {
        gameM.CameraSkillPlayer(3);

        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(7));

        plyAnim.SetInteger("A_AutoBuff", 1);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_AutoBuff", 0);

        yield return new WaitForSeconds(1.5f);

         GameObject buff = Instantiate(buffFX03, gameM.playerParent.position, transform.rotation);

        buff.transform.SetParent(gameM.playerParent);

        plyStats.SetIntellect(1.25f);

        gameM.CameraPos1();
    }

    private IEnumerator MeteorAtk()
    {
        gameM.CameraSkillPlayer(4);

        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(8));

        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(0.7f);
        Instantiate(meteorRain, plyMove.target.transform.position, transform.rotation);

        yield return new WaitForSeconds(2f);

        gameM.CameraPos1();
    }

    private IEnumerator Heal()
    {
        gameM.CameraSkillPlayer(2);

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
        gameM.CameraPos1();
    }

    private IEnumerator Resurrect()
    {
        gameM.CameraSkillPlayer(2);

        SkillSelection(gameM.playerParent.name, 2, GetTimerSkills(10));

        plyAnim.SetInteger("A_Magic", 3);

        yield return new WaitForSeconds(0.2f);

        plyAnim.SetInteger("A_Magic", 0);

        yield return new WaitForSeconds(1f);

        gameM.players.Add(plyMove.target.transform);
        plyMove.target.GetComponent<Animator>().SetInteger("A_Death", 0);
        plyMove.target.GetComponent<NavMeshAgent>().enabled = true;
        plyMove.target.GetComponent<PlayerStats>().SetLife(plyStats.GetIntellect());

        yield return new WaitForSeconds(1.5f);
        gameM.CameraPos1();
    }

    private IEnumerator FinalJudgment()
    {
        gameM.CameraSkillPlayer(4);

        SkillSelection(gameM.playerParent.name, 3, GetTimerSkills(11));

        plyAnim.SetInteger("A_Magic", 4);
        yield return new WaitForSeconds(0.2f);
        plyAnim.SetInteger("A_Magic", 0);
        yield return new WaitForSeconds(1.3f);

        Vector3 newPos = plyMove.target.transform.position;
        newPos.y += 24;

        Quaternion newRot = gameM.playerParent.rotation;
        newRot.x = 180;

        Destroy(Instantiate(finalJ, newPos, newRot), 5);

        yield return new WaitForSeconds(2f);
        demaciaArea.gameObject.SetActive(false);
        gameM.CameraPos1();

    }
}
