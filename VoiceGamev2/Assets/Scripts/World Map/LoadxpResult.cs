using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadxpResult : MonoBehaviour
{
    private GameSave gameSave;
    private int exp;
    private string actualLevel;
    private float actualXP;
    [SerializeField] private GameObject victoryPannel;
    private bool loadvictory =true;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerLevel;
    [SerializeField] private GameObject hasLevelUp;
    [SerializeField] private Image frontXpBar;
    [SerializeField] private Image backXpBar;
    [SerializeField] public TextMeshProUGUI actualHP;
    [SerializeField] private TextMeshProUGUI newHP;
    [SerializeField] private TextMeshProUGUI amountofLvl;
    private void Update()
    {
        /*if (victoryPannel)
        {
            if (loadvictory)
            {
                loadvictory = false;
            }
        }*/
    }
    private void Start()
    {
        VictoryPannelReward();

    }
    public void VictoryPannelReward()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if(playerName.text == GameObject.FindGameObjectsWithTag("Player")[i].transform.name)
            {
                print("Se ejecuta: " + playerName.text);
                playerPanel.SetActive(true);
                //PlayerData data = SaveSystem.LoadPlayer(playerName.transform);
                LevelSystem level = GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<LevelSystem>();
                GeneralStats general = level.gameObject.GetComponent<GeneralStats>();
                actualLevel = "Level: " + level.level;
                actualXP = level.currentXp;
                frontXpBar.fillAmount = level.currentXp / level.requiredXp;
                backXpBar.fillAmount = level.currentXp / level.requiredXp;
                int lifeValue = 25;
                int nowhp;
                int newhp;
                for (int a = 2; a <= general.lifePoints; a++)
                {
                    lifeValue += (int)(a * 1.5f);
                    if((a+1)== general.lifePoints)
                    {
                        nowhp = lifeValue;
                        actualHP.text = nowhp.ToString();
                    }
                }
                newhp = lifeValue;
                newHP.text = newhp.ToString();
                
                playerLevel.text = actualLevel.ToString();
                if (level.hasLvlUP)
                {
                    //amountofLvl.text = level.amountOfLvl.ToString();
                    //print("Cantidad de puntos: " + level.amountOfLvl);
                    hasLevelUp.SetActive(true);
                }
            }
        }
    }
}