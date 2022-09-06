using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Windows.Speech;

public class UIMovement : MonoBehaviour
{
    private Dictionary<string, Action> firstCanvasLvl = new Dictionary<string, Action>();
    public KeywordRecognizer firstCanvas;
    private Dictionary<string, Action> partyInf = new Dictionary<string, Action>();
    public KeywordRecognizer party;
    private Dictionary<string, Action> charInf = new Dictionary<string, Action>();
    private KeywordRecognizer character;
    private Dictionary<string, Action> optionsOrders = new Dictionary<string, Action>();
    private KeywordRecognizer optionsRecognizer;
    //private Dictionary<string, Action> inventoryOptions = new Dictionary<string, Action>();
    //private KeywordRecognizer inventory;
    private PlayableDirector timeLine;
    private bool inGroup;
    public bool inOptions;
    [SerializeField] private PartyInformation partyInformation;
    [SerializeField] private Character_skills character_Skills;
    [SerializeField] private IncreaseStats increaseStats;
    //[SerializeField] private EquipObjects equipObjects;

    [SerializeField] public GameObject partyPannel;
    [SerializeField] private Animator animatorSave;
    //[SerializeField] private Animator inventoryAnimator;
    [SerializeField] public GameObject characterPannel;
    [SerializeField] private GameObject victoryResult;
    [SerializeField] private GameObject optionsPannel;
    [SerializeField] private TextMeshProUGUI textSave;
    //[SerializeField] private GameObject skillsZone;
    //[SerializeField] private GameObject inventoryZone;
    [SerializeField] private VoiceDestinations voiceDestinations;

    [SerializeField] private GameObject mainHelpPannel;
    [SerializeField] private GameSave gameSave;
    [HideInInspector] public bool canOpenGroup;
    private PlayableDirector openGroup;
    private PlayableDirector closeGroup;

    public string charSelected;

    [SerializeField] private FTUE_Progresion fTUE_Progresion;
    // Start is called before the first frame update
    private void Awake()
    {
        
        increaseStats.AddControls();
        timeLine = GameObject.Find("TimeLine").GetComponent<PlayableDirector>();
        openGroup = GameObject.Find("OpenGroup").GetComponent<PlayableDirector>();
        closeGroup = GameObject.Find("CloseGroup").GetComponent<PlayableDirector>();
    }
    void Start()
    {
        /*partyInformation = GameObject.Find("PartyInformation").GetComponent<PartyInformation>();
        character_Skills = GameObject.Find("Skills").GetComponent<Character_skills>();
        partyPannel = GameObject.Find("PartyInformation");
        characterPannel = GameObject.Find("CharacterInformation");*/
        canOpenGroup = true;
        //AddInventory();
    }
    private void Update()
    {
    }
    public void AddFirstLvl()
    {
        firstCanvasLvl.Add("grupo", ActivatePartyInformation);
        firstCanvasLvl.Add("cerrar", CloseWindows);
        firstCanvasLvl.Add("opciones", OpenOptions);
        firstCanvas = new KeywordRecognizer(firstCanvasLvl.Keys.ToArray());
        firstCanvas.OnPhraseRecognized += RecognizedVoiceFirst;
        firstCanvas.Start();
    }
    
    public void AddPartyInf()
    {

        for (int i = 0; i < partyInformation.players.Length; i++)
        {
            partyInf.Add(partyInformation.players[i].name, LoadCharacter);
            print(partyInformation.players[i].name);
        }

        party = new KeywordRecognizer(partyInf.Keys.ToArray());
        party.OnPhraseRecognized += RecognizedVoiceParty;
    }
    /*private void AddInventory()
    {
        inventoryOptions.Add("abilidades", ActivateSkillAnimation);
        inventoryOptions.Add("inventario", ActivateInventoryAnimation);
        inventoryOptions.Add("imventario", ActivateInventoryAnimation);
        inventory = new KeywordRecognizer(inventoryOptions.Keys.ToArray());
        inventory.OnPhraseRecognized += RecognizedInventory;
        //inventory.Start();
    }*/
    public void OptionsOrders()
    {
        optionsOrders.Add("salir", CloseGame);
        optionsOrders.Add("guardar", Save);
        optionsRecognizer = new KeywordRecognizer(optionsOrders.Keys.ToArray());
        optionsRecognizer.OnPhraseRecognized += RecognizedOptionsOrders;
        optionsRecognizer.Start();
    }

    private void LoadCharacter()
    {
        if (inOptions) return;
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
    public void ReLoadCharacter(string p)
    {
        //string p = fTUE_Progresion.GetPlayer();
        party.Stop();
        characterPannel.SetActive(true);
        GameObject actualCharacter = GameObject.Find(p);
        LevelSystem level = actualCharacter.GetComponent<LevelSystem>();
        level.ActivateButtons();
        character_Skills.DisplayCharacterInf(actualCharacter);
    }
    /*private void ActivateSkillAnimation()
    {
        //inventoryAnimator.SetBool("ReverseInventory", true);
        skillsZone.SetActive(true);
        inventoryZone.SetActive(false);
        equipObjects.weapons.Stop();
        equipObjects.isInventory = false;
        //inventoryAnimator.SetBool("ReverseInventory", false);


    }
    private void ActivateInventoryAnimation()
    {
        //inventoryAnimator.SetBool("Inventory", true);
        skillsZone.SetActive(false);
        inventoryZone.SetActive(true);
        equipObjects.isInventory = true;
        //inventoryAnimator.SetBool("Inventory", false);
    }*/
    private void CloseGame()
    {
        gameSave.SaveGame();
        Application.Quit();
    }
    private void OpenOptions()
    {
        optionsPannel.SetActive(true);
        optionsRecognizer.Start();
        canOpenGroup = false;
    }
    private void Save()
    {
        gameSave.SaveGame();
        StartCoroutine(FadeText());
    }
    IEnumerator FadeText()
    {
        textSave.gameObject.SetActive(true);
        textSave.GetComponent<Animation>().Play();
        animatorSave.enabled = true;
        animatorSave.SetFloat("float", 1);
        yield return new WaitForSeconds(1);
        animatorSave.SetFloat("float", 0);
        yield return new WaitForSeconds(3);
        animatorSave.enabled = false;

        textSave.gameObject.SetActive(false);
    }
    private void RecognizedVoiceFirst(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        firstCanvasLvl[speech.text].Invoke();
    }
    /*private void RecognizedInventory(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        inventoryOptions[speech.text].Invoke();
    }*/
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

    IEnumerator ShowHelpPannel(GameObject actualPannel)
    {
        actualPannel.SetActive(true);
        yield return new WaitForSeconds(4f);
        actualPannel.SetActive(false);
    }
    public void ActivatePartyInformation()
    {
        if (inOptions) return;
        if (canOpenGroup && !inGroup)
        {
            inGroup = true;
            voiceDestinations.mapDestinations.Stop();
            partyPannel.SetActive(true);
            openGroup.Play();
            party.Start();
            if (fTUE_Progresion.ftueProgression == 1)
            {
                fTUE_Progresion.ftueProgression++;
                fTUE_Progresion.FTUEProgression();
                gameSave.SaveGame();
            }
        }
    }
    private void CloseWindows()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        if (characterPannel.activeInHierarchy)
        {
            //skillsZone.SetActive(true);
            //inventoryZone.SetActive(false);
            characterPannel.SetActive(false);
            party.Start();
        }
        else if(partyPannel.activeInHierarchy)
        {
            inGroup = false;
            voiceDestinations.mapDestinations.Start();
            firstCanvas.Start();
            //partyPannel.SetActive(false);
            partyPannel.transform.GetChild(0).gameObject.SetActive(false);
            closeGroup.Play();
            party.Stop();
            if (fTUE_Progresion.ftueProgression == 5)
            {
                fTUE_Progresion.ftueProgression++;
                fTUE_Progresion.FTUEProgression();

            }
        }else if (victoryResult.activeInHierarchy)
        {
            victoryResult.SetActive(false);
            
            if (fTUE_Progresion.ftueProgression == 0 || fTUE_Progresion.ftueProgression == 1)
            {
                fTUE_Progresion.pannel1.SetActive(false);
                timeLine.Play();
                return;
            }
            voiceDestinations.mapDestinations.Start();
            canOpenGroup = true;

        }
        else if(optionsPannel.activeInHierarchy){
            optionsPannel.SetActive(false);
            optionsRecognizer.Stop();
            inOptions = false;
            canOpenGroup = true;
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
