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

    [SerializeField] private Scrollbar lifeSld1;
    [SerializeField] private Slider lifeSld2;

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
        if(life <= 0)
        {
            Destroy(transform.gameObject);
        }
        lifeSld1.size = (life / maxLife);
        lifeSld2.value = (life / maxLife);
    }
}
