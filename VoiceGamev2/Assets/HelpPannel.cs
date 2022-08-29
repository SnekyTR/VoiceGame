using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [HideInInspector] public string preSelectPlayer ;
    [HideInInspector] public string selectedPlayer;
    [HideInInspector] public string attackEnemies;
    [HideInInspector] public string revivePlayers;
    GameObject[] enemies;
    private SkillBook skillBook;
    private string actualPlayer;
    // Start is called before the first frame update
    void Start()
    {
        skillBook = gameObject.GetComponent<SkillBook>();
        text = GameObject.Find("PlayerActions").GetComponent<TextMeshProUGUI>();
        int a = GameObject.Find("Players").transform.childCount;
        for(int i = 0;i< a; i++)
        {
            preSelectPlayer += GameObject.Find("Players").transform.GetChild(i).name + "\n";
        }
        selectedPlayer = "Mover" + "\n" + "Atacar";
        int b = GameObject.Find("Enemys").transform.childCount;
        for (int i = 0; i < b; i++)
        {
            attackEnemies += GameObject.Find("Enemys").transform.GetChild(i).name + "\n";
        }
        UpdateTextHelp(preSelectPlayer);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public void UpdateTextHelp(string helpText)
    {
        text.text = helpText;
    }
    public void SelectedPlayer(string player)
    {
        actualPlayer = player;
        selectedPlayer = "Atacar" + "\n" + "Mover" + "\n";
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i].transform.name != player)
            {
                selectedPlayer += GameObject.Find("Players").transform.GetChild(i).name + "\n";
            }
        }
        selectedPlayer += "Habilidades";
        UpdateTextHelp(selectedPlayer);
    }
    public void AttackState()
    {
        print(actualPlayer);
        if(actualPlayer == "Torek")
        {
            print("Ha entrado en torek");
            attackEnemies = "Curar: " + "\n";
            GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < a.Length; i++)
            {
                attackEnemies += GameObject.Find("Players").transform.GetChild(i).name + "\n";
            }
            UpdateTextHelp(attackEnemies);
        }
        else
        {
            print("NO ha entrado en torek");
            attackEnemies = "Atacar: " + "\n";
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyStats enemyStats = enemies[i].GetComponent<EnemyStats>();
                int hp = enemyStats.GetLife();
                if (hp > 0)
                {
                    attackEnemies += enemies[i].name + "\n";
                }
            }
            attackEnemies += "Cancelar";
            UpdateTextHelp(attackEnemies);
        }
    }
    public void ReviveState()
    {
        revivePlayers = "Revivir: " + "\n";
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < a.Length; i++)
        {
            revivePlayers += GameObject.Find("Players").transform.GetChild(i).name + "\n";
        }
        UpdateTextHelp(revivePlayers);
    }

    public void GetSkillsName(string pname, int atrPoints)
    {
        selectedPlayer += skillBook.GetSkillName(pname, atrPoints) + "\n";
    }
}
