using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class FTUE_Progresion : MonoBehaviour
{
    public int ftueProgression = 0;

    private Dictionary<string, Action> ftueActions = new Dictionary<string, Action>();
    private KeywordRecognizer ftueRecognizer;
    [SerializeField] private Progression pro;

    public GameObject pannel1;
    [SerializeField] private GameObject extrapannel;
    [SerializeField] private GameObject pannel2;
    [SerializeField] private GameObject pannel3;
    [SerializeField] private GameObject pannel4;
    [SerializeField] private GameObject pannel5;
    [SerializeField] private GameObject pannel6;
    [SerializeField] private GameObject pannel7;
    public GameObject downButtons;
    private UIMovement uIMovement;
    private void Awake()
    {

    }
    void Start()
    {
        uIMovement = GetComponent<UIMovement>();
    }
    public void LoadFTUEProgresion()
    {
        GameProgressionData data = SaveSystem.LoadProgression();
        ftueProgression = data.ftueProgressionNumber;
        
    }
    public void ReloadFTUE()
    {
        switch (ftueProgression)
        {
            case 1:
                pannel2.SetActive(true);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }
    public void AfterTimeLine()
    {
        ftueProgression = 1;
        FTUEProgression();
    }
    public void FTUEProgression()
    {
        if(ftueProgression == 0)
        {
            downButtons.SetActive(false);
            pannel1.SetActive(true);
        }
        else if(ftueProgression == 1)
        {
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
        }
        else if (ftueProgression == 4)
        {
            pannel4.SetActive(false);
            //pannel5.SetActive(true);
            StartCoroutine(ActivateTimer());
            //extrapannel.SetActive(true);
        }
        else if (ftueProgression == 5)
        {
            pannel6.SetActive(true);
        }
        else if (ftueProgression == 6)
        {
            pannel6.SetActive(false);
            pro.restAnimator.SetFloat("anim", 1);
            pannel7.SetActive(true);
        }
        else if (ftueProgression == 7)
        {
            pannel7.SetActive(false);
            
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
           
            ftueProgression++;
            FTUEProgression();
        }
        else if (ftueProgression == 4)
        {
            pannel5.SetActive(false);
            extrapannel.SetActive(false);
            ftueProgression++;
            FTUEProgression();
        }
        else if (ftueProgression == 3)
        {
            pannel4.SetActive(false);
            ftueProgression++;
        }
    }
}
