using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int healthStat;
    public int intellectStat;
    public int strength;
    public int agility;

    public float[] position;

    public PlayerData(GeneralStats player)
    {
        healthStat = player.lifePoints;
        intellectStat = player.intellectPoints;
        strength = player.strengthPoints;
        agility = player.agilityPoints;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
