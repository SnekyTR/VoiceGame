using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int lifePoints;
    [SerializeField] public int strengthPoints;
    [SerializeField] public int intellectPoints;
    [SerializeField] public int agilityPoints;
    [SerializeField] private float energy;
    [SerializeField] private EnergyScript energyLo;
    private Animator animator;

    private int lifeValue;
    private int strengthValue;
    private int agilityValue;
    private int intellectValue;
    private int manaValue;

    private float maxLife;
    private float maxMana;
    public float maxEnergy;

    [SerializeField] private Scrollbar lifeSld;
    [SerializeField] private Scrollbar manaSld;
    private CameraFollow gameM;
    void Start()
    {
        //LoadStatsPlayer();
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();

        //calculo de valor de los stats
        lifeValue = 5;
        for(int i = 2; i <= lifePoints; i++)
        {
            lifeValue += (int)i / 2;
        }

        strengthValue = 3;
        for(int i = 2; i <= strengthPoints; i++)
        {
            strengthValue += (int)i / 3;
        }

        agilityValue = 3;
        for(int i = 2;i <= agilityPoints; i++)
        {
            agilityValue += (int)i / 3;
        }

        intellectValue = 5;
        manaValue = 2 * intellectPoints;
        for(int i = 2; i <= intellectPoints; i++)
        {
            intellectValue += (int)i / 3;
        }

        animator = GetComponent<Animator>();
        maxLife = lifeValue;
        maxMana = manaValue;
        maxEnergy = energy;
    }
    //Coge los stats guardados en el fichero antes de abrir el nivel
    private void LoadStatsPlayer()
    {
        GameData data = SaveSystem.LoadPlayer();

        lifePoints = data.healthStat;
        strengthPoints = data.strength;
        agilityPoints = data.agility;
        intellectPoints = data.intellectStat;
    }
    void Update()
    {
        
    }

    public void SetLife(int n)
    {
        
        lifeValue += n;
        
        if (lifeValue <= 0)                    //death
        {
            animator.SetInteger("A_Death", 1);
            gameM.EliminateElement(this.gameObject);
        }
        else if (n < 0)                         //dmg recieve
        {
            animator.SetInteger("A_Recieve", 1);
        }
        lifeSld.size = (lifeValue / maxLife);
    }

    public void SetMana(int n)
    {
        manaValue += n;
        manaSld.size = (manaValue / maxMana);
    }

    public void SetEnergy(float n)
    {
        energy += n;
        energyLo.NewEnergyIcon(energy);
    }

    public void FullEnergy()
    {
        energy = maxEnergy;
        energyLo.NewEnergyIcon(energy);
    }

    public int GetAtk()
    {
        int newAtk = Random.Range((strengthValue - 2), strengthValue);
        return newAtk;
    }

    public float GetEnergy()
    {
        return energy;
    }
}
