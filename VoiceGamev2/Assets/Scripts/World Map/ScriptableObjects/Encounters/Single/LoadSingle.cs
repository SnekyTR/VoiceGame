using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSingle : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Image image;
    [SerializeField] private Text reward;
    [SerializeField] private Text level;
    [SerializeField] private Text enemies;

    public void DisplayLevelInf(SingleEncounterStructure single)
    {
        title.text = single.combatTitle;
        description.text = single.combatDescription;
        level.text = single.level;
        reward.text = single.reward;
        int i;
        for (i = 0; i < single.enemyList.Length; i++)
        {
            enemies.text = enemies.text + "\n" + single.enemyList[i];
        }
        
    }
}
