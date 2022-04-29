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

    private float maxLife;
    private float maxMana;
    public float maxEnergy;

    [SerializeField] private Scrollbar lifeSld;
    [SerializeField] private Scrollbar manaSld;

    void Start()
    {
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
        return atk;
    }

    public float GetEnergy()
    {
        return energy;
    }
}
