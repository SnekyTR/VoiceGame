using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int mana;
    [SerializeField] private int atk;
    [SerializeField] private float energy;

    private float maxLife;
    private float maxEnergy;

    [SerializeField] private Scrollbar lifeSld1;
    [SerializeField] private Slider lifeSld2;

    void Start()
    {
        maxLife = life;
        maxEnergy = energy;
    }
    void Update()
    {
        
    }
    public void SetLife(int n)
    {
        life += n;
        if(life <= 0)
        {
            Destroy(transform.gameObject);
        }
        lifeSld1.size = (life / maxLife);
        lifeSld2.value = (life / maxLife);
    }

    public float GetEnergy()
    {
        return energy;
    }

    public int GetAtk()
    {
        int newAtk = Random.Range((atk - 2), atk);
        return newAtk;
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
