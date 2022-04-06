using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using WarriorAnimsFREE;

public class Spells : MonoBehaviour
{
    public KeywordRecognizer keywordRecognizer;
    public SkillLearning skillLearning;
    public  Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public CharacterStats stat;
    private List<BaseStat> stats = new List<BaseStat>();
    public GameObject heal;
    public GameObject dmg;
    public GameObject deff;
    public GameObject move;
    private int bonusSpeed = 5;



    private int damageBuff = 15;
    public WarriorMovementController player;

    private bool lighting = false;

    private void Awake()
    {
        //GameObject heal = GameObject.FindGameObjectWithTag("Heal");
        stat = GetComponent<CharacterStats>();
    }
    void Start()
    {
        //Buffs
        actions.Add("tira", DeffBuff);
        actions.Add("exura", HealBonus);
        actions.Add("me", MoveSpeedBonus);
        actions.Add("maka", DamageBuff);

        //WeaponEnchants
        actions.Add("duma", LightningEnchant);


        //Unlocking Spells
        actions.Add("koko", UnLockLighting);
        //actions.Add("koko", MoveSpeedBonus);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    public void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    public void MoveSpeedBonus()
    {
        if(stat.manaBar.value > 0)
        {
            stat.manaBar.value -= 5;
            move.SetActive(true);
            player.runSpeed = player.runSpeed + bonusSpeed;
            StartCoroutine(FadeMoveSpeed());
        }     
    }
    IEnumerator FadeMoveSpeed()
    {
        yield return new WaitForSeconds(5f);
        move.SetActive(false);
        player.runSpeed = player.runSpeed - bonusSpeed;
        StopCoroutine(FadeMoveSpeed());
    }
    public void HealBonus()
    {
        if (stat.manaBar.value > 0)
        {
            stat.manaBar.value -= 5;
            StartCoroutine(Healing());
            if (stat.hpPoints >= stat.maxHealth)
            {
                stat.hpPoints = stat.maxHealth;
                Debug.Log("Se ha curado maximo " + stat.hpPoints);
            }
            else
            {
                stat.hpPoints = stat.hpPoints + 5;
                Debug.Log("Se ha curado normal " + stat.hpPoints);
            }
        }
       
        
    }
    private void DamageBuff()
    {
        if(stat.manaBar.value > 0)
        {
            StartCoroutine(DamageTimer());
            stat.damage = stat.damage - damageBuff;
            stat.manaBar.value -= 15;
        }
              
    }
    private void DeffBuff()
    {
        if(stat.manaBar.value > 0)
        {
            StartCoroutine(DeffTimer());
            stat.damage = stat.damage - damageBuff;
            stat.manaBar.value -= 7;
        }
        
        
    }
    IEnumerator Healing()
    {

            heal.SetActive(true);
            yield return new WaitForSeconds(2f);
            heal.SetActive(false);
            StopCoroutine(Healing());
        
    }
    IEnumerator DamageTimer()
    {
        stat.damage = stat.damage + damageBuff;
        Debug.Log("Ha entrado a corrutina");
        dmg.SetActive(true);
        yield return new WaitForSeconds(10f);
        dmg.SetActive(false);
        StopCoroutine(DamageTimer());
    }
    IEnumerator DeffTimer()
    {
        //.deff = stat.damage + damageBuff;
        deff.SetActive(true);
        yield return new WaitForSeconds(10f);
        deff.SetActive(false);
        StopCoroutine(DeffTimer());
    }
    private void UnLockLighting()
    {
        if (skillLearning.lightingSkill)
        {
            lighting = true;
        }
        
    }
    private void LightningEnchant()
    {
        if (lighting = true)
        {
            Debug.Log("caca");
        }
    }
}
