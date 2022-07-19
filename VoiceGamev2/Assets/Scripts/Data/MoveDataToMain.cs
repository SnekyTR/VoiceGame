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
    private GameObject victoryResults;
    public int totalEXP;
    private GameObject[] player;
    VoiceDestinations voices;
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
    public void IncrementProgresion()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        StartCoroutine(ReChargeObjects());  
    }
    IEnumerator ReChargeObjects()
    {
        yield return new WaitForSeconds(0.1f);
        print("Entra despeus de destroy");
        pro = GameObject.Find("GameSaver").GetComponent<Progression>();
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
        
        //level = GameObject.Find("Magnus").GetComponent<LevelSystem>();
        StartCoroutine(IncrementTheProgression());
        //SceneManager.UnloadSceneAsync(1);

    }
    IEnumerator IncrementTheProgression()
    {
        
        player = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < player.Length; i++)
        {
            if (player[i].name == "Magnus")
            {
                voices = player[i].GetComponent<VoiceDestinations>();
                voices.enabled = false;
            }
        }
        yield return new WaitForSeconds(0.1f);
        pro.progression += 1;
        gameSave.SaveGame();
        //gameSave.LoadGame();
        pro.CheckProgression();
        for(int i = 0; i< GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            level = GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<LevelSystem>();
            level.GainExperience(totalEXP);
            level.hasLvlUP = true;
            //gameSave.LoadGame(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<GeneralStats>(), GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<LevelSystem>()) ;
        }
        gameSave.SaveGame();
        voices.enabled = true;
        pro.Victory();
    }
    public void FailLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        int scene =  SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
        //StartCoroutine(FailMainScene());
    }
    IEnumerator FailMainScene()
    {
        yield return new WaitForSeconds(1f);
        print("Ha fallado");
        gameSave.LoadGame();
        pro.CheckProgression();
        gameSave.SaveGame();
    }
}
