using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarriorAnimsFREE;

public class WeaponAttack : MonoBehaviour
{
    public WarriorController player;
    //public Enemy enemy;
    private int damage = 100;
    private bool damageOne = true;
    public bool damageTwo;
    void Update()
    {
        /*if (player.inputAttack)
        {
            Debug.Log("PR");
            damageTwo = true;
            StartCoroutine(DamageTwo());
        }*/
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy" && damageOne &&damageTwo)
        {
            StartCoroutine(AttackCD());
            col.GetComponent<Enemy>().TakeDamage(damage);

        }
    }

    IEnumerator AttackCD()
    {
        Debug.Log("Cou");
        damageOne = false;
        yield return new WaitForSeconds(0.8f);
        damageOne = true;

    } public IEnumerator DamageTwo()
    {
        yield return new WaitForSeconds(0.8f);
        damageTwo = false;
    }
}
