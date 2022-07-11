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
    [SerializeField] private float energy;
    private Animator animator;

    private WinLoose winLoose;

    [HideInInspector] public float maxLife;
    [HideInInspector] public float maxShield;
    [HideInInspector] public float maxEnergy;
    public int xp = 50;

    [SerializeField] private GameObject intBars;
    private GameObject extBars;

    private CameraFollow gameM;
    private MoveDataToMain moveData;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        maxLife = lifeValue;
        maxShield = shieldValue;
        maxEnergy = energy;
        winLoose = gameM.GetComponent<WinLoose>();
        winLoose.totalEnemies++;
        extBars = GameObject.Find("CanvasManager").transform.GetChild(6).GetChild(nEnemy).gameObject;

        moveData = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
        moveData.totalEXP = moveData.totalEXP + xp;

        extBars.transform.GetChild(0).GetComponent<Text>().text = transform.name;
        extBars.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        intBars.transform.GetChild(0).GetComponent<Text>().text = transform.name;
        intBars.transform.GetChild(1).GetComponent<Slider>().value = (lifeValue / maxLife);
        intBars.transform.GetChild(2).GetComponent<Text>().text = (lifeValue + " / " + maxLife);

        if (shieldValue > 0) NewShield(shieldValue);
    }
    void Update()
    {
        
    }
    public void SetLife(int n)
    {
        if (shieldValue > 0 && n < 0)
        {
            shieldValue += n;

            if (shieldValue <= 0)
            {
                extBars.transform.GetChild(2).GetComponent<Scrollbar>().size = 0;
                intBars.transform.GetChild(3).GetComponent<Slider>().value = 0;
                intBars.transform.GetChild(4).GetComponent<Text>().text = "";

                extBars.transform.GetChild(2).gameObject.SetActive(false);
                intBars.transform.GetChild(3).gameObject.SetActive(false);
                intBars.transform.GetChild(4).gameObject.SetActive(false);

                animator.SetInteger("A_Recieve", 1);

                n = shieldValue;
            }
            else
            {
                extBars.transform.GetChild(2).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
                intBars.transform.GetChild(3).GetComponent<Slider>().value = (shieldValue / maxShield);
                intBars.transform.GetChild(4).GetComponent<Text>().text = (shieldValue + " / " + maxShield);

                animator.SetInteger("A_Recieve", 1);

                return;
            }
        }


        lifeValue += n;

        if (lifeValue <= 0)                    //death
        {
            winLoose.totalEnemies--;
            animator.SetInteger("A_Death", 1);
            gameM.EliminateElement(this.gameObject);
            GetComponent<NavMeshAgent>().enabled = false;

            intBars.SetActive(false);
            
        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);
        }
        

        extBars.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        intBars.transform.GetChild(1).GetComponent<Slider>().value = (lifeValue / maxLife);
        intBars.transform.GetChild(2).GetComponent<Text>().text = (lifeValue + " / " + maxLife);
    }
    public int GetLife()
    {
        return lifeValue;
    }

    public float GetEnergy()
    {
        return energy;
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

    public void SetEnergy(float n)
    {
        energy += n;
    }

    public void FullEnergy()
    {
        energy = maxEnergy;
    }

    public void NewShield(int s)
    {
        extBars.transform.GetChild(2).gameObject.SetActive(true);
        intBars.transform.GetChild(3).gameObject.SetActive(true);
        intBars.transform.GetChild(4).gameObject.SetActive(true);

        shieldValue = s;
        maxShield = s;

        extBars.transform.GetChild(2).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
        intBars.transform.GetChild(3).GetComponent<Slider>().value = (shieldValue / maxShield);
        intBars.transform.GetChild(4).GetComponent<Text>().text = (shieldValue + " / " + maxShield);
        intBars.transform.GetChild(2).GetComponent<Text>().text = "";
    }
}
