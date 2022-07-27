using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveDataToMain : MonoBehaviour
{
    [SerializeField] private Progression pro;
    [SerializeField] private GameSave gameSave;
    private LevelSystem level;
    private CombatEnter combatEnter;
    private GameObject victoryResults;
    private GameObject[] player;
    VoiceDestinations voices;
    public LoadingScreen loadingScreen;
    public bool loadVictory;
    public int totalEXP;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncrementProgresion()
    {
        loadingScreen = GameObject.Find("CanvasManager").GetComponent<LoadingScreen>();
        loadingScreen.LoadScene(1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        StartCoroutine(ReChargeObjects());  
    }
    IEnumerator ReChargeObjects()
    {
        yield return new WaitForSeconds(0.1f);
        pro = GameObject.Find("GameSaver").GetComponent<Progression>();
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
        
        //level = GameObject.Find("Magnus").GetComponent<LevelSystem>();
        //StartCoroutine(IncrementTheProgression());
        //SceneManager.UnloadSceneAsync(1);

    }
    
   
}
