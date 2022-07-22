using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class UIMovement : MonoBehaviour
{
    private Dictionary<string, Action> firstCanvasLvl = new Dictionary<string, Action>();
    private KeywordRecognizer firstCanvas;
    private Dictionary<string, Action> partyInf = new Dictionary<string, Action>();
    private KeywordRecognizer party;
    private Dictionary<string, Action> charInf = new Dictionary<string, Action>();
    private KeywordRecognizer character;
    private Dictionary<string, Action> optionsOrders = new Dictionary<string, Action>();
    private KeywordRecognizer optionsRecognizer;

    [SerializeField] private PartyInformation partyInformation;
    [SerializeField] private Character_skills character_Skills;

    [SerializeField] private GameObject partyPannel;
    [SerializeField] private GameObject characterPannel;
    [SerializeField] private GameObject victoryResult;
    [SerializeField] private GameObject optionsPannel;

    [SerializeField] private GameObject mainHelpPannel;
    [SerializeField] private GameSave gameSave;

    private string charSelected;

    [SerializeField] private FTUE_Progresion fTUE_Progresion;
    // Start is called before the first frame update
    void Start()
    {
        /*partyInformation = GameObject.Find("PartyInformation").GetComponent<PartyInformation>();
        character_Skills = GameObject.Find("Skills").GetComponent<Character_skills>();
        partyPannel = GameObject.Find("PartyInformation");
        characterPannel = GameObject.Find("CharacterInformation");*/

        AddFirstLvl();
        AddPartyInf();
        OptionsOrders();
    }
    private void Update()
    {
    }
    private void AddFirstLvl()
    {
        firstCanvasLvl.Add("grupo", ActivatePartyInformation);
        firstCanvasLvl.Add("cerrar", CloseWindows);
        firstCanvasLvl.Add("opciones", OpenOptions);
        firstCanvas = new KeywordRecognizer(firstCanvasLvl.Keys.ToArray());
        firstCanvas.OnPhraseRecognized += RecognizedVoiceFirst;
        firstCanvas.Start();
    }
    private void AddPartyInf()
    {

        for (int i = 0; i < partyInformation.players.Length; i++)
        {
            partyInf.Add(partyInformation.players[i].name, LoadCharacter);
            print(partyInformation.players[i].name);
        }

        party = new KeywordRecognizer(partyInf.Keys.ToArray());
        party.OnPhraseRecognized += RecognizedVoiceParty;
    }
    private void OptionsOrders()
    {
        optionsOrders.Add("salir", CloseGame);
        optionsOrders.Add("guardar", Save);
        optionsRecognizer = new KeywordRecognizer(optionsOrders.Keys.ToArray());
        optionsRecognizer.OnPhraseRecognized += RecognizedOptionsOrders;
        optionsRecognizer.Start();
    }

    private void LoadCharacter()
    {
        party.Stop();
        characterPannel.SetActive(true);
        GameObject actualCharacter = GameObject.Find(charSelected);
        LevelSystem level = actualCharacter.GetComponent<LevelSystem>();
        level.ActivateButtons();
        character_Skills.DisplayCharacterInf(actualCharacter);
        if (fTUE_Progresion.ftueProgression == 2)
        {
            fTUE_Progresion.ftueProgression++;
            fTUE_Progresion.FTUEProgression();
        }
    }
    private void CloseGame()
    {
        gameSave.SaveGame();
        Application.Quit();
    }
    private void OpenOptions()
    {
        optionsPannel.SetActive(true);
        optionsRecognizer.Start();
    }
    private void Save()
    {
        gameSave.SaveGame();
    }
    private void RecognizedVoiceFirst(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        firstCanvasLvl[speech.text].Invoke();
    }
    private void RecognizedOptionsOrders(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        optionsOrders[speech.text].Invoke();
    }
    private void RecognizedVoiceHelpPannels(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        optionsOrders[speech.text].Invoke();
    }
    private void RecognizedVoiceParty(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        charSelected = speech.text;
        partyInf[speech.text].Invoke();

    }
    private void HelpPannels()
    {
        if (firstCanvas.IsRunning)
        {
            StartCoroutine(ShowHelpPannel(mainHelpPannel));
        }
    }
    IEnumerator ShowHelpPannel(GameObject actualPannel)
    {
        actualPannel.SetActive(true);
        yield return new WaitForSeconds(4f);
        actualPannel.SetActive(false);
    }
    private void ActivatePartyInformation()
    {
        partyPannel.SetActive(true);
        party.Start();
        if(fTUE_Progresion.ftueProgression == 1)
        {
            fTUE_Progresion.ftueProgression++;
            fTUE_Progresion.FTUEProgression();
        }
    }
    private void CloseWindows()
    {
        if (characterPannel.activeInHierarchy)
        {
            characterPannel.SetActive(false);
            party.Start();
        }
        else if(partyPannel.activeInHierarchy)
        {
            partyPannel.SetActive(false);
            party.Stop();
            if (fTUE_Progresion.ftueProgression == 5)
            {
                fTUE_Progresion.ftueProgression++;
                fTUE_Progresion.FTUEProgression();
            }
        }else if (victoryResult.activeInHierarchy)
        {
            fTUE_Progresion.NextPannel();
            victoryResult.SetActive(false);
        }else if(optionsPannel.activeInHierarchy){
            optionsPannel.SetActive(false);
            optionsRecognizer.Stop();
        }
        
    }
    private void DeActivatePartyInformation()
    {
        party.Stop();
        partyPannel.SetActive(false);
    }
    private void ActivateCharacterInformation()
    {
        characterPannel.SetActive(true);
        character.Start();
        party.Stop();

    }
    private void DeActivateCharacterInformation()
    {
        characterPannel.SetActive(false);
        character.Stop();
        party.Start();
    }
    private void DeActivateCharInformation()
    {
        characterPannel.SetActive(false);
        character.Stop();
        party.Start();
    }
}
