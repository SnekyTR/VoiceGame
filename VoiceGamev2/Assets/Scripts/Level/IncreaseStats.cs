using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class IncreaseStats : MonoBehaviour
{
    [SerializeField] private LevelSystem level;
    [SerializeField] private GeneralStats general;
    [SerializeField] private FTUE_Progresion fTUE_Progresion;
    [SerializeField] private GameSave gameSave;

    //[SerializeField] private CharacterInformation character;
    [SerializeField] private Character_skills character_Skills;
    private Dictionary<string, Action> statsActions = new Dictionary<string, Action>();
    public KeywordRecognizer statOrders;
    private GameObject player;
    //public bool levelear;
    private void Start()
    {
        player = level.PlayerLvlUp();
        statOrders.Start();
    }
    public void GetPlayer(GameObject player)
    {
        level = player.GetComponent<LevelSystem>();
        general = player.GetComponent<GeneralStats>();
        
        if(level.amountOfLvl!= 0)
        {
            statOrders.Start();
        }
        //character = player.GetComponent<CharacterInformation>();
        //character_Skills = GameObject.Find("Skills").GetComponent<Character_skills>();
    }
    public void AddControls()
    {
        statsActions.Add("fuerza", IncreaseSRT);
        statsActions.Add("vida", IncreaseHealth);
        statsActions.Add("agilidad", IncreaseAGI);
        statsActions.Add("intelecto", IncreaseINT);
        statsActions.Add("critico", IncreaseCRIT);
        statOrders = new KeywordRecognizer(statsActions.Keys.ToArray());
        statOrders.OnPhraseRecognized += RecognizedVoice;
        
    }
    public void CloseOrders()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, IncreaseSRT);
        statOrders = new KeywordRecognizer(zero1.Keys.ToArray());
        statOrders.OnPhraseRecognized += RecognizedVoice;
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        statsActions[speech.text].Invoke();
        if(fTUE_Progresion.ftueProgression == 3)
        {
            fTUE_Progresion.ftueProgression++;
            fTUE_Progresion.FTUEProgression();
        }
    }
    public void IncreaseSRT()
    {
        general.strengthPoints++;
        character_Skills.UpdateSRT(general);
        UpdateLevelAmount();
        level.DeactivateButtons();

    }
    public void IncreaseHealth()
    {
        general.lifePoints++;
        character_Skills.UpdateHP(general);
        UpdateLevelAmount();
        level.DeactivateButtons();

    }
    public void IncreaseAGI()
    {
        general.agilityPoints++;
        character_Skills.UpdateAGI(general);
        UpdateLevelAmount();
        level.DeactivateButtons();

    }
    public void IncreaseINT()
    {
        general.intellectPoints++;
        character_Skills.UpdateINT(general);
        UpdateLevelAmount();
        level.DeactivateButtons();

    }
    public void IncreaseCRIT()
    {
        general.critStrikePoints++;
        character_Skills.UpdateCRIT(general);
        UpdateLevelAmount();
        level.DeactivateButtons();

    }
    private void UpdateLevelAmount()
    {
        level.amountOfLvl--;
        statOrders.Stop();
        character_Skills.amountofLvl.text = "Puntos restantes: " + level.amountOfLvl.ToString();
        if(fTUE_Progresion.ftueProgression != 3)
        {
            gameSave.SaveGame();
        }
        

    }
}
