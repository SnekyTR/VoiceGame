using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int lifeValue;
    [SerializeField] private int shieldValue;
    [SerializeField] private int atkValue;
    [SerializeField] private int range;
    [SerializeField] private float energy;
    private Animator animator;

    private WinLoose winLoose;

    private float maxLife;
    private float maxEnergy;
    public int xp = 50;

    [SerializeField] private Scrollbar lifeSld1;
    [SerializeField] private Slider lifeSld2;
    private CameraFollow gameM;
    private MoveDataToMain moveData;

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        maxLife = lifeValue;
        maxEnergy = energy;
        winLoose = GameObject.Find("GameManager").GetComponent<WinLoose>();
       // moveData = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
        //moveData.totalEXP = moveData.totalEXP + xp;

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
                /*structure.transform.GetChild(2).GetComponent<Scrollbar>().size = 0;
                selected.transform.GetChild(1).GetComponent<Scrollbar>().size = 0;
                selected.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "";

                structure.transform.GetChild(2).gameObject.SetActive(false);
                selected.transform.GetChild(1).gameObject.SetActive(false);

                animator.SetInteger("A_Recieve", 1);*/

                n = shieldValue;
            }
            else
            {
                /*structure.transform.GetChild(2).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
                selected.transform.GetChild(1).GetComponent<Scrollbar>().size = (shieldValue / maxShield);
                selected.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = (shieldValue + " / " + maxShield);

                animator.SetInteger("A_Recieve", 1);*/

                return;
            }
        }


        lifeValue += n;

        if (lifeValue <= 0)                    //death
        {
            winLoose.LooseActivateVoice();
            animator.SetInteger("A_Death", 1);
            gameM.EliminateElement(this.gameObject);
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);
        }
        /*structure.transform.GetChild(1).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetComponent<Scrollbar>().size = (lifeValue / maxLife);
        selected.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = (lifeValue + " / " + maxLife);*/

        lifeSld1.size = (lifeValue / maxLife);
        lifeSld2.value = (lifeValue / maxLife);
    }

    public float GetEnergy()
    {
        return energy;
    }

    public int GetAtk()
    {
        int newAtk = Random.Range((atkValue - 2), atkValue);
        return newAtk;
    }

    public int GetRange()
    {
        return range;
    }

    public void SetEnergy(float n)
    {
        energy += n;
    }

    public void FullEnergy()
    {
        energy = maxEnergy;
    }
}
