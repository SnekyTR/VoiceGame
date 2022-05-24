using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDouble : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Image image;
    [SerializeField] private Text reward;
    [SerializeField] private Text level;
    [SerializeField] private Text level2;
    [SerializeField] private Text enemies;
    [SerializeField] private Text enemies2;
    public void DisplayLevelInf(DoubleEncounterStructure doub)
    {
        title.text = doub.combatTitle;
        description.text = doub.combatDescription;
        level.text = doub.level;
        level2.text = doub.level2;
        reward.text = doub.reward;
        int i;
        for (i = 0; i < doub.enemyList.Length; i++)
        {
            enemies.text = enemies.text + "\n" + doub.enemyList[i];
        }
        for (i = 0; i < doub.enemyList2.Length; i++)
        {
            enemies2.text = enemies2.text + "\n" + doub.enemyList2[i];
        }

    }

}
