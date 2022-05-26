using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStats : MonoBehaviour
{
    private LevelSystem levelSystem;
    public int playerLevel;
    public int playerExperience;
    public int lifePoints;
    public int strengthPoints;
    public int intellectPoints;
    public int agilityPoints;
    public int critStrikePoints;

    private void Awake()
    {
        levelSystem = gameObject.GetComponent<LevelSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadPlayer()
    {

        PlayerData data = SaveSystem.LoadPlayer();

        levelSystem.level = data.level;
        levelSystem.currentXp = data.experience;

        strengthPoints = data.strength;
        agilityPoints = data.agility;
        intellectPoints = data.intellectStat;
        lifePoints = data.healthStat;
        critStrikePoints = data.critStrikePoints;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
