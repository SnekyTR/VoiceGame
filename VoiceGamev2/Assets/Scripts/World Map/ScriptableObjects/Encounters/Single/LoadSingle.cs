using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSingle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private TextMeshProUGUI enemies;

    public void DisplayLevelInf(SingleEncounterStructure single)
    {
        title.text = single.combatTitle;
        description.text = single.combatDescription;
        reward.text = single.reward;
        int i;
        for (i = 0; i < single.enemyList.Length; i++)
        {
            enemies.text = enemies.text + "\n" + single.enemyList[i];
        }
        
    }
}
