using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int nEnemy;
    [SerializeField] private int lifeValue;
    [SerializeField] private int shieldValue;
    [SerializeField] private int atkValue;
    [SerializeField] private int range;
    private GameObject bars;

    [Header ("Energia")]
    public float energy = 5;
    public float energyActions = 5;
    private Animator animator;

    private WinLoose winLoose;
    private bool isStunned;
    private bool isOnFire = false;
    private int fireDmg = 0;

    [HideInInspector] public float maxLife;
    [HideInInspector] public float maxShield;
    [HideInInspector] public float maxEnergy;
    [HideInInspector] public float maxEnergyActions;
    [HideInInspector] public CanvasFollow canv;
    [Header("Extra")]
    public int xp = 50;

    [SerializeField] private GameObject intBars;
    private GameObject extBars;

    private CameraFollow gameM;
    private MoveDataToMain moveData;
    public Transform cinemaCam;
    public GameObject deathFX;
    public Text dmgTxt;
    public Color dmgColor;

    public GameObject stunFX;
    private GameObject stunPrefab;
    private List<GameObject> auraFX = new List<GameObject>();

    [HideInInspector] public bool inmunity = false;

    private AudioSource audioS;
    public AudioClip hurtS, dieS;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        maxLife = lifeValue;
        maxShield = shieldValue;
        maxEnergy = energy;
        maxEnergyActions = energyActions;
        winLoose = gameM.GetComponent<WinLoose>();
        winLoose.totalEnemies++;
        extBars = GameObject.Find("CanvasManager").transform.GetChild(7).GetChild(nEnemy).gameObject;

        if (GameObject.Find("SceneConector")) moveData = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
        if (GameObject.Find("SceneConector")) moveData.totalEXP = moveData.totalEXP + xp;

        extBars.transform.GetChild(0).GetComponent<Text>().text = transform.name;
        extBars.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        intBars.transform.GetChild(0).GetComponent<Text>().text = transform.name;
        intBars.transform.GetChild(1).GetChild(1).GetComponent<Slider>().value = (lifeValue / maxLife);
        intBars.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (lifeValue + "/" + maxLife);

        bars = intBars.transform.GetChild(1).gameObject;

        intBars.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = atkValue.ToString();

        if (shieldValue > 0) NewShield(shieldValue);

        audioS = GetComponent<AudioSource>();
    }
    public void SetLife(int n, bool t)
    {
        StartCoroutine(DmgTxtAnim(n, t));

        if (shieldValue > 0 && n < 0)
        {
            shieldValue += n;

            if (shieldValue <= 0)
            {
                intBars.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = "";

                extBars.transform.GetChild(2).gameObject.SetActive(false);
                intBars.transform.GetChild(1).GetChild(3).gameObject.SetActive(false);
                intBars.transform.GetChild(1).GetChild(4).gameObject.SetActive(false);

                animator.SetInteger("A_Recieve", 1);

                audioS.clip = hurtS;
                audioS.Play();

                n = shieldValue;
            }
            else
            {
                intBars.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = (shieldValue).ToString();

                animator.SetInteger("A_Recieve", 1);

                audioS.clip = hurtS;
                audioS.Play();

                return;
            }
        }


        lifeValue += n;

        if (lifeValue <= 0)                    //death
        {
            winLoose.totalEnemies--;
            animator.SetInteger("A_Death", 1);
            gameM.EliminateElement(this.gameObject);
            if(GetComponent<NavMeshAgent>()) GetComponent<NavMeshAgent>().enabled = false;

            intBars.transform.GetChild(0).gameObject.SetActive(false);
            intBars.transform.GetChild(1).gameObject.SetActive(false);
            intBars.transform.GetChild(3).gameObject.SetActive(false);
            Destroy(Instantiate(deathFX, transform.position, transform.rotation), 4);

            audioS.clip = dieS;
            audioS.Play();

            for(int i = 0; i < auraFX.Count; i++)
            {
                Destroy(auraFX[i]);
            }

            if(GetComponent<CapsuleCollider>()) GetComponent<CapsuleCollider>().enabled = false;
            else GetComponent<BoxCollider>().enabled = false;
        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);

            audioS.clip = hurtS;
            audioS.Play();
        }
        

        extBars.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        intBars.transform.GetChild(1).GetChild(1).GetComponent<Slider>().value = (lifeValue / maxLife);
        intBars.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (lifeValue + "/" + maxLife);
    }

    public void StunEnemy(bool t)
    {
        if (t)
        {
            Vector3 pos = transform.position;
            pos.y += 2;

            stunPrefab = Instantiate(stunFX, pos, transform.rotation);
            isStunned = true;
        }
        else
        {
            Destroy(stunPrefab);

            isStunned = false;
        }
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    private IEnumerator DmgTxtAnim(int i, bool t)
    {
        if (t)
        {
            dmgTxt.fontSize = 120;
            dmgTxt.color = Color.white;
            dmgTxt.text = i.ToString();
        }
        else
        {
            dmgTxt.fontSize = 80;
            dmgTxt.color = dmgColor;
            dmgTxt.text = i.ToString();
        }

        yield return new WaitForSeconds(2f);

        dmgTxt.text = "";
    }

    public int GetLife()
    {
        return lifeValue;
    }

    public float GetEnergy()
    {
        return energy;
    }

    public float GetEnergyActions()
    {
        return energyActions;
    }

    public int GetAtk()
    {
        return atkValue;
    }

    public int GetRange()
    {
        return range;
    }

    public int GetShield()
    {
        return shieldValue;
    }

    public void SetDmg(int i)
    {
        atkValue = i;
    }

    public void SetEnergy(float n)
    {
        energy += n;
    }

    public void SetEnergyAction(float n)
    {
        energyActions += n;
    }
    public void FullEnergy()
    {
        energy = maxEnergy;
        energyActions = maxEnergyActions;
    }

    public void NewShield(int s)
    {
        extBars.transform.GetChild(2).gameObject.SetActive(true);
        intBars.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
        intBars.transform.GetChild(1).GetChild(4).gameObject.SetActive(true);

        shieldValue = s;
        maxShield = s;

        intBars.transform.GetChild(1).GetChild(4).GetComponent<Text>().text = (shieldValue).ToString();
    }

    public void AddAura(GameObject obj)
    {
        auraFX.Add(obj);
    }

    public void OnFire(int e)
    {
        if (!isOnFire)
        {
            isOnFire = true;
            fireDmg = e;
        }
    }

    public void FireDmg()
    {
        if (fireDmg > 0) fireDmg *= -1;
        SetLife(fireDmg, false);
    }

    public bool IsOnFire()
    {
        return isOnFire;
    }
}
