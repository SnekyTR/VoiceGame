using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    [SerializeField] private GeneralStats general;
    [SerializeField] private LevelSystem level;
    [SerializeField] private Progression pro;
    //[SerializeField] private SingleCombatChanger single;
    // Start is called before the first frame update
    void Awake()
    {
        general = GameObject.Find("Player 1").GetComponent<GeneralStats>();
        level = GameObject.Find("Player 1").GetComponent<LevelSystem>();
        pro = gameObject.GetComponent<Progression>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame()
    {
        SaveSystem.SavePlayer(general, level);
        SaveSystem.SaveProgression(pro);
    }
    public void LoadGame()
    {
        general.LoadPlayer();
        level.LoadLevel();
    }
}
