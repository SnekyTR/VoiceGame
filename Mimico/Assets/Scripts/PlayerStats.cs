using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int mana;
    [SerializeField] private int atk;
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
        maxLife = life;
        maxMana = mana;
        maxEnergy = energy;
    }

    void Update()
    {
        
    }

    public void SetLife(int n)
    {
        
        life += n;
        
        if (life <= 0)
        {
            animator.SetInteger("A_Death", 1);
        }
        else if (n < 0)
        {
            animator.SetInteger("A_Recieve", 1);
        }
        lifeSld.size = (life / maxLife);
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
        int newAtk = Random.Range((atk - 2), atk);
        return newAtk;
    }

    public float GetEnergy()
    {
        return energy;
    }
}
