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

    private float maxLife;

    [SerializeField] private Slider lifeSld;

    void Start()
    {
        maxLife = life;
    }
    void Update()
    {
        
    }
    public void SetLife(int n)
    {
        life += n;
        lifeSld.value = (life / maxLife);
    }
}
