using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int mana;
    [SerializeField] private int atk;
    [SerializeField] private int energy;

    private float maxLife;
    private float maxMana;
    public int maxEnergy;

    [SerializeField] private Slider lifeSld;
    [SerializeField] private Slider manaSld;
    [SerializeField] private Text energyTxt;

    void Start()
    {
        maxLife = life;
        maxMana = mana;
        maxEnergy = energy;
        energyTxt.text = energy.ToString();
    }

    void Update()
    {
        
    }

    public void SetLife(int n)
    {
        life += n;
        lifeSld.value = (life / maxLife);
    }

    public void SetMana(int n)
    {
        mana += n;
        manaSld.value = (mana / maxMana);
    }

    public void SetEnergy(int n)
    {
        energy += n;
        energyTxt.text = energy.ToString();
    }

    public void FullEnergy()
    {
        energy = maxEnergy;
        energyTxt.text = energy.ToString();
    }

    public int GetAtk()
    {
        return atk;
    }

    public int GetEnergy()
    {
        return energy;
    }
}
