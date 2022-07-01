using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class FTUE_Progresion : MonoBehaviour
{
    public int ftueProgression = 0;
    [SerializeField] Progression progression;

    private Dictionary<string, Action> ftueActions = new Dictionary<string, Action>();
    private KeywordRecognizer ftueRecognizer;

    [SerializeField] private GameObject pannel1;
    [SerializeField] private GameObject extrapannel;
    [SerializeField] private GameObject pannel2;
    [SerializeField] private GameObject pannel3;
    [SerializeField] private GameObject pannel4;
    [SerializeField] private GameObject pannel5;
    [SerializeField] private GameObject pannel6;
    [SerializeField] private GameObject pannel7;
    private void Awake()
    {

    }
    void Start()
    {

    }
    public void FTUEProgression()
    {
        if(ftueProgression == 0)
        {
            AddOrders();
            pannel1.SetActive(true);
            extrapannel.SetActive(true);
        }else if(ftueProgression == 1)
        {
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
            pannel5.SetActive(true);
        }
        else if (ftueProgression == 5)
        {
            pannel6.SetActive(true);
        }
        else if (ftueProgression == 6)
        {
            pannel6.SetActive(false);
            pannel7.SetActive(true);
        }
        else
        {
            pannel7.SetActive(false);
        }
    }
    private void AddOrders()
    {
        ftueActions.Add("Aceptar", NextPannel);
        ftueRecognizer = new KeywordRecognizer(ftueActions.Keys.ToArray());
        ftueRecognizer.OnPhraseRecognized += RecognizedVoice;
        ftueRecognizer.Start();
    }

    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        ftueActions[speech.text].Invoke();
    }
    private void NextPannel()
    {
        if(ftueProgression == 0)
        {
            pannel1.SetActive(false);
            extrapannel.SetActive(false);
            ftueProgression++;
            FTUEProgression();
        }
        else if (ftueProgression == 4)
        {
            pannel5.SetActive(false);
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
