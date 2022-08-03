using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public float experience;
    public int healthStat;
    public int intellectStat;
    public int strength;
    public int agility;
    public int coins;
    public int critStrikePoints;
    public int amountofLevel;
    public string weaponType;
    public string weaponEquiped;

    public float[] position;

    public PlayerData(GeneralStats player, LevelSystem levelSystem)
    {
        level = levelSystem.level;
        experience = levelSystem.currentXp;
        healthStat = player.lifePoints;
        intellectStat = player.intellectPoints;
        strength = player.strengthPoints;
        agility = player.agilityPoints;
        critStrikePoints = player.critStrikePoints;
        amountofLevel = levelSystem.amountOfLvl;
        weaponType = player.weaponType;
        weaponEquiped = player.weaponequiped;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}