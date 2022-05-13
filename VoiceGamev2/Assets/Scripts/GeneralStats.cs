using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStats : MonoBehaviour
{
    public int playerLevel;
    public int playerExperience;
    public int lifePoints;
    public int strengthPoints;
    public int intellectPoints;
    public int agilityPoints;
    public int critStrikePoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    /*public void LoadPlayer(LoadCharacters characters)
    {
        PlayerData data = SaveSystem.LoadPlayer();

        lifePoints = data.healthStat;
        strengthPoints = data.strength;
        agilityPoints = data.agility;
        intellectPoints = data.intellectStat;


        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
    }*/
}
