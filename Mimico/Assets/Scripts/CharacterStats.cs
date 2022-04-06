
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    
    public List<BaseStat> stats = new List<BaseStat>();
    public int mnPoints;
    public int hpPoints;
    public int enPoints;

    public int maxHealth = 100;
    public int maxMana = 30;
    public int maxEnergy = 50;
    public int maxStrenght = 50;
    public int damage = 50;
    public Slider healthBar;
    public Slider manaBar;
    private void Awake()
    {
        hpPoints = maxHealth;
        mnPoints = maxMana;
        enPoints = maxEnergy;
    }
    private void Start()
    {
        stats.Add(new BaseStat(maxStrenght, "Fuerza", "Daño melee"));
        stats.Add(new BaseStat(maxHealth, "Health", "Amount of health"));
        stats.Add(new BaseStat(maxEnergy, "Energy", "Amount of Energy"));
        stats.Add(new BaseStat(maxMana, "Mana", "Magic Resource"));

        hpPoints = maxHealth;
        ChangeStrenght(10);
        //Debug.Log(stats[0].GetCalculatedStatValue());
        healthBar.value = hpPoints;
    }
    public void ChangeStrenght(int strenght)
    {
        stats[0].AddStatBonus(new StatBonus(strenght));
    }
    public void RecieveDamage()
    {
        
    }
    int CalculateHealth()
    {
        return hpPoints;
    }
}
