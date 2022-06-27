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

    [SerializeField] private PartyInformation partyInformation;
    [SerializeField] private Character_skills character_Skills;

    [SerializeField] private GameObject partyPannel;
    [SerializeField] private GameObject characterPannel;

    private string charSelected;
    // Start is called before the first frame update
    void Start()
    {
        /*partyInformation = GameObject.Find("PartyInformation").GetComponent<PartyInformation>();
        character_Skills = GameObject.Find("Skills").GetComponent<Character_skills>();
        partyPannel = GameObject.Find("PartyInformation");
        characterPannel = GameObject.Find("CharacterInformation");*/

        AddFirstLvl();
        //AddPartyInf();
        //AddCharacterInf();  
    }
    private void Update()
    {
    }
    private void AddFirstLvl()
    {
        firstCanvasLvl.Add("grupo", ActivatePartyInformation);
        firstCanvasLvl.Add("cerrar", CloseWindows);
        firstCanvas = new KeywordRecognizer(firstCanvasLvl.Keys.ToArray());
        firstCanvas.OnPhraseRecognized += RecognizedVoiceFirst;
        firstCanvas.Start();
        print("Esta activado");
    }
    private void AddPartyInf()
    {

        for (int i = 0; i < partyInformation.players.Length; i++)
        {

            partyInf.Add(partyInformation.players[i].name, LoadCharacter);
            print(partyInformation.players[i].name);
        }
        //partyInf.Add("salir", DeActivatePartyInformation);
        party = new KeywordRecognizer(partyInf.Keys.ToArray());
        party.OnPhraseRecognized += RecognizedVoiceParty;
    }
    private void AddCharacterInf()
    {
        
        character = new KeywordRecognizer(charInf.Keys.ToArray());
        character.OnPhraseRecognized += RecognizedVoiceParty;
    }
    private void LoadCharacter()
    {
        print("Loading");
        party.Stop();
        //character.Start();
        characterPannel.SetActive(true);
        GameObject actualCharacter = GameObject.Find(charSelected);
        character_Skills.DisplayCharacterInf(actualCharacter);
    }
    private void RecognizedVoiceFirst(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        firstCanvasLvl[speech.text].Invoke();
    }
    private void RecognizedVoiceParty(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        charSelected = speech.text;
        partyInf[speech.text].Invoke();
    }
    private void ActivatePartyInformation()
    {
        partyPannel.SetActive(true);
        //firstCanvas.Stop();
        party.Start();
    }
    private void CloseWindows()
    {
        if (characterPannel.activeInHierarchy)
        {
            characterPannel.SetActive(false);
        }
        else if(partyPannel.activeInHierarchy)
        {
            partyPannel.SetActive(false);
            party.Stop();
        }
    }
    private void DeActivatePartyInformation()
    {
        print("He dicho cosa hehehee");
        party.Stop();
        firstCanvas.Start();
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
