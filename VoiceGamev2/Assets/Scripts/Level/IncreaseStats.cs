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
    //[SerializeField] private CharacterInformation character;
    private Character_skills character_Skills;
    private Dictionary<string, Action> statsActions = new Dictionary<string, Action>();
    private KeywordRecognizer statOrders;
    private GameObject player;

    private void Start()
    {
       player = level.PlayerLvlUp();
    }
    public void GetPlayer(GameObject player)
    {
        level = player.GetComponent<LevelSystem>();
        general = player.GetComponent<GeneralStats>();
        //character = player.GetComponent<CharacterInformation>();
        character_Skills = GameObject.Find("Skills").GetComponent<Character_skills>();
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
        statOrders.Start();
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        statsActions[speech.text].Invoke();
    }
    public void IncreaseSRT()
    {
        general.strengthPoints++;
        character_Skills.UpdateSRT(general);
        level.DeactivateButtons();
    }
    public void IncreaseHealth()
    {
        general.lifePoints++;
        character_Skills.UpdateHP(general);
        level.DeactivateButtons();
    }
    public void IncreaseAGI()
    {
        general.agilityPoints++;
        character_Skills.UpdateAGI(general);
        level.DeactivateButtons();
    }
    public void IncreaseINT()
    {
        general.intellectPoints++;
        character_Skills.UpdateINT(general);
        level.DeactivateButtons();
    }
    public void IncreaseCRIT()
    {
        general.critStrikePoints++;
        character_Skills.UpdateCRIT(general);
        level.DeactivateButtons();
    }
}
