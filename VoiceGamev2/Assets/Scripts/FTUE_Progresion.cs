using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class FTUE_Progresion : MonoBehaviour
{
    public int ftueProgression = 0;

    private Dictionary<string, Action> ftueActions = new Dictionary<string, Action>();
    private KeywordRecognizer ftueRecognizer;
    [SerializeField] private Progression pro;

    public GameObject pannel1;
    [HideInInspector]public string actualPlayer;
    [SerializeField] private GameObject extrapannel;
    [SerializeField] private GameObject pannel2;
    [SerializeField] private GameObject pannel3;
    [SerializeField] private GameObject pannel4;
    [SerializeField] private GameObject pannel5;
    [SerializeField] private GameObject pannel6;
    [SerializeField] private GameObject pannel7;
    public GameObject downButtons;
    [SerializeField] private PlayableDirector timeLine;
    [SerializeField] private UIMovement uIMovement;
    [SerializeField]private GameSave gameSave;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }
    public void LoadFTUEProgresion()
    {
        GameProgressionData data = SaveSystem.LoadProgression();
        ftueProgression = data.ftueProgressionNumber;
        actualPlayer = data.actualPlayer;
        
    }
    public void ReloadFTUE()
    {
        timeLine = GameObject.Find("TimeLine").GetComponent<PlayableDirector>();
        print("se ha hecho reload");
        pannel2.SetActive(true);
        switch (ftueProgression)
        {
            case 1:
                timeLine.Play();
                downButtons.SetActive(false);
                pannel2.SetActive(false);
                break;
            case 2:
                uIMovement.partyPannel.SetActive(true);
                uIMovement.ActivatePartyInformation();
                uIMovement.firstCanvas.Start();
                pannel3.SetActive(true);
                pannel2.SetActive(false);
                break;
            case 3:
                uIMovement.partyPannel.SetActive(true);
                uIMovement.ActivatePartyInformation();
                pannel2.SetActive(false);
                uIMovement.firstCanvas.Start();
                uIMovement.ReLoadCharacter(actualPlayer);
                pannel4.SetActive(true);
                break;
            case 4:
                uIMovement.partyPannel.SetActive(true);
                uIMovement.ActivatePartyInformation();
                uIMovement.firstCanvas.Start();
                uIMovement.ReLoadCharacter(actualPlayer);
                pannel2.SetActive(false);
                StartCoroutine(ActivateTimer());
                break;
            case 5:
                uIMovement.partyPannel.SetActive(true);
                uIMovement.ActivatePartyInformation();
                uIMovement.firstCanvas.Start();
                uIMovement.ReLoadCharacter(actualPlayer);
                pannel2.SetActive(false);
                StartCoroutine(ActivateTimer());
                break;
            case 6:
                pannel7.SetActive(true);
                pro.restAnimator.SetFloat("anim", 1);
                VoiceDestinations voices = GameObject.Find("Magnus").GetComponent<VoiceDestinations>();
                voices.mapDestinations.Start();
                uIMovement.firstCanvas.Start();
                pannel2.SetActive(false);

                break;
            default:
                pannel2.SetActive(false);
                uIMovement.firstCanvas.Start();
                timeLine.Play();
                break;
        }
    }
    public string GetPlayer()
    {
        return actualPlayer;
    }
    public void AfterTimeLine()
    {
        FTUEProgression();
    }
    public void FTUEProgression()
    {
        print("NO se ha hecho reload");
        if (ftueProgression == 0)
        {
            downButtons.SetActive(false);
            pannel1.SetActive(true);

        }
        else if(ftueProgression == 1)
        {
            print("Se activan");
            downButtons.SetActive(true);
            uIMovement.canOpenGroup = true;
            pannel2.SetActive(true);
        }
        else if (ftueProgression == 2)
        {
            pannel2.SetActive(false);
            pannel3.SetActive(true);
        }
        else if (ftueProgression == 3)
        {
            pannel3.SetActive(false);
            pannel4.SetActive(true);
            gameSave.SaveGame();
        }
        else if (ftueProgression == 4)
        {
            pannel4.SetActive(false);
            //pannel5.SetActive(true);
            StartCoroutine(ActivateTimer());
            //extrapannel.SetActive(true);
            gameSave.SaveGame();
        }
        else if (ftueProgression == 5)
        {
            pannel6.SetActive(true);
        }
        else if (ftueProgression == 6)
        {
            gameSave.SaveGame();
            
            pannel6.SetActive(false);
            pro.restAnimator.SetFloat("anim", 1);
            pannel7.SetActive(true);
        }
        else if (ftueProgression == 7)
        {
            pannel7.SetActive(false);
            gameSave.SaveGame();
        }
    }
    IEnumerator ActivateTimer()
    {
        pannel5.SetActive(true);
        yield return new WaitForSeconds(4f);
        pannel5.SetActive(false);
        ftueProgression++;
        FTUEProgression();
    }
    /*private void AddOrders()
    {
        ftueActions.Add("Aceptar", NextPannel);
        ftueRecognizer = new KeywordRecognizer(ftueActions.Keys.ToArray());
        ftueRecognizer.OnPhraseRecognized += RecognizedVoice;
        ftueRecognizer.Start();
    }
    public void CloseOrders()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().weaponName + ns, NextPannel);
        ftueRecognizer = new KeywordRecognizer(zero1.Keys.ToArray());
    }*/
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        ftueActions[speech.text].Invoke();
    }
    public void NextPannel()
    {
        if(ftueProgression == 0)
        {
            pannel1.SetActive(false);
           
            //ftueProgression++;
            FTUEProgression();
        }
        else if (ftueProgression == 4)
        {
            pannel5.SetActive(false);
            extrapannel.SetActive(false);
            //ftueProgression++;
            FTUEProgression();
        }
        else if (ftueProgression == 3)
        {
            pannel4.SetActive(false);
            ftueProgression++;
        }
    }
}
