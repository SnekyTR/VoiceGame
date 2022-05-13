using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int experience;
    public int healthStat;
    public int intellectStat;
    public int strength;
    public int agility;
    public int coins;
    public int critStrikePoints;

    public float[] position;

    public GameData(GeneralStats player)
    {
        level = player.playerLevel;
        experience = player.playerExperience;
        healthStat = player.lifePoints;
        intellectStat = player.intellectPoints;
        strength = player.strengthPoints;
        agility = player.agilityPoints;
        critStrikePoints = player.critStrikePoints;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
