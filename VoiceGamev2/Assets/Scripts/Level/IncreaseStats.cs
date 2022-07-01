using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class IncreaseStats : MonoBehaviour
{
    [SerializeField] private LevelSystem level;
    [SerializeField] private GeneralStats general;
    [SerializeField] private FTUE_Progresion fTUE_Progresion;

    //[SerializeField] private CharacterInformation character;
    [SerializeField] private Character_skills character_Skills;
    private Dictionary<string, Action> statsActions = new Dictionary<string, Action>();
    public KeywordRecognizer statOrders;
    private GameObject player;
    public bool levelear;
    private void Start()
    {
        AddControls();
        player = level.PlayerLvlUp();
        statOrders.Start();
    }
    public void GetPlayer(GameObject player)
    {
        level = player.GetComponent<LevelSystem>();
        general = player.GetComponent<GeneralStats>();
        
        if (levelear)
        {
            statOrders.Start();
        }
        else { levelear = true; }
        //character = player.GetComponent<CharacterInformation>();
        //character_Skills = GameObject.Find("Skills").GetComponent<Character_skills>();
    }
    private void AddControls()
    {
        statsActions.Add("fuerza", IncreaseSRT);
        statsActions.Add("vida", IncreaseHealth);
        statsActions.Add("agilidad", IncreaseAGI);
        statsActions.Add("intelecto", IncreaseINT);
        statsActions.Add("critico", IncreaseCRIT);
        statOrders = new KeywordRecognizer(statsActions.Keys.ToArray());
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
        level.DeactivateButtons();
        statOrders.Stop();
    }
    public void IncreaseHealth()
    {
        general.lifePoints++;
        character_Skills.UpdateHP(general);
        level.DeactivateButtons();
        statOrders.Stop();
    }
    public void IncreaseAGI()
    {
        general.agilityPoints++;
        character_Skills.UpdateAGI(general);
        level.DeactivateButtons();
        statOrders.Stop();
    }
    public void IncreaseINT()
    {
        general.intellectPoints++;
        character_Skills.UpdateINT(general);
        level.DeactivateButtons();
        statOrders.Stop();
    }
    public void IncreaseCRIT()
    {
        general.critStrikePoints++;
        character_Skills.UpdateCRIT(general);
        level.DeactivateButtons();
        statOrders.Stop();
    }
}
