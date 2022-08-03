using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    //[SerializeField] private GeneralStats general;
    //[SerializeField] private LevelSystem level;
    [SerializeField] private Progression pro;
    [SerializeField] private FTUE_Progresion fTUE_Progresion;
    //[SerializeField] private SingleCombatChanger single;
    // Start is called before the first frame update
    void Awake()
    {
        //general = GameObject.Find("Magnus").GetComponent<GeneralStats>();
        //level = GameObject.Find("Magnus").GetComponent<LevelSystem>();
        pro = gameObject.GetComponent<Progression>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            GeneralStats general = players[i].GetComponent<GeneralStats>();
            LevelSystem level = players[i].GetComponent<LevelSystem>();
            Transform trans = players[i].GetComponent<Transform>();
            SaveSystem.SavePlayer(general, level, trans);
        }
        SaveSystem.SaveProgression(pro,fTUE_Progresion);
    }
    public void LoadGame()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            GeneralStats general = GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<GeneralStats>();
            LevelSystem level = GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<LevelSystem>();
            general.LoadPlayer();
            level.LoadLevel();
        }
    }
}
