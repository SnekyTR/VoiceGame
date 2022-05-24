using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Encounters", menuName ="Encounters/Double")]
public class DoubleEncounterStructure : ScriptableObject
{
    public int index;
    public string combatTitle;
    public string combatDescription;
    public Image image;
    public string reward;
    public string level;
    public string level2;
    public string[] enemyList;
    public string[] enemyList2;
}
