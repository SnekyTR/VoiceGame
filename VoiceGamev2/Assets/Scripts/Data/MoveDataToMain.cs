using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveDataToMain : MonoBehaviour
{
    private Progression pro;
    private GameSave gameSave;
    private LevelSystem level;
    private CombatEnter combatEnter;
    public int totalEXP;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        ReChargeObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncrementProgresion(int num)
    {
        SceneManager.LoadScene(0);
        StartCoroutine(ReChargeObjects());  
    }
    IEnumerator ReChargeObjects()
    {
        
        yield return new WaitForSeconds(0.5f);
        print("Entra despeus de destroy");
        pro = GameObject.Find("GameSaver").GetComponent<Progression>();
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
        level = GameObject.Find("Player 1").GetComponent<LevelSystem>();
        StartCoroutine(IncrementTheProgression());
    }
    IEnumerator IncrementTheProgression()
    {
        
        yield return new WaitForSeconds(1f);
        pro.progression += 1;
        print("Incrementa");
        gameSave.LoadGame();
        pro.CheckProgression();
        level.GainExperience(totalEXP);
        gameSave.SaveGame();
    }
}
