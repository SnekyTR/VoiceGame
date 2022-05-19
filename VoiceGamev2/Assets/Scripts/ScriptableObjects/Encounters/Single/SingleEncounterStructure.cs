using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Encounters", menuName ="Encounters/Single")]
public class SingleEncounterStructure : ScriptableObject
{
    public int index;
    public string combatTitle;
    public string combatDescription;
    public Sprite Image;
    public string reward;
    public string level;
    public string[] enemyList;
}
