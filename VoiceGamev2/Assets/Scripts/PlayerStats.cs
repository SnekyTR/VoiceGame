using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public int lifePoints;
    [SerializeField] public int mana;
    [SerializeField] public int strengthPoints;
    [SerializeField] public int intellectPoints;
    [SerializeField] public int agilityPoints;
    [SerializeField] private float energy;
    [SerializeField] private EnergyScript energyLo;
    private Animator animator;

    private float maxLife;
    private float maxMana;
    public float maxEnergy;

    [SerializeField] private Scrollbar lifeSld;
    [SerializeField] private Scrollbar manaSld;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        maxLife = lifePoints;
        maxMana = mana;
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
        
        lifePoints += n;
        
        if (lifePoints <= 0)
        {
            animator.SetInteger("A_Death", 1);
        }
        else if (n < 0)
        {
            animator.SetInteger("A_Recieve", 1);
        }
        lifeSld.size = (lifePoints / maxLife);
    }

    public void SetMana(int n)
    {
        mana += n;
        manaSld.size = (mana / maxMana);
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
        int newAtk = Random.Range((strengthPoints - 2), strengthPoints);
        return newAtk;
    }

    public float GetEnergy()
    {
        return energy;
    }
}
