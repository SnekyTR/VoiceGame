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
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerLevel;
    [SerializeField] private GameObject hasLevelUp;
    [SerializeField] private Image frontXpBar;
    [SerializeField] private Image backXpBar;
    private void Update()
    {
        if (victoryPannel)
        {
            VictoryPannelReward();
        }
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
                actualLevel = "Level: " + level.level;
                actualXP = level.currentXp;
                frontXpBar.fillAmount = level.currentXp / level.requiredXp;
                backXpBar.fillAmount = level.currentXp / level.requiredXp;
                playerLevel.text = actualLevel.ToString();
                if (level.hasLvlUP)
                {
                    hasLevelUp.SetActive(true);
                }
            }
        }
    }

}
