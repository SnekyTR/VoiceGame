using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class FTUE_Progresion : MonoBehaviour
{
    private int ftueProgression;
    private Dictionary<string, Action> ftueActions = new Dictionary<string, Action>();
    private KeywordRecognizer ftueRecognizer;

    private GameObject pannel1;
    private GameObject pannel2;
    private GameObject pannel3;
    private GameObject pannel4;
    private GameObject pannel5;
    private GameObject pannel6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FTUEProgression()
    {
        if(ftueProgression == 0)
        {
            pannel1.SetActive(true);
        }else if(ftueProgression == 1)
        {
            pannel2.SetActive(true);
        }
        else if (ftueProgression == 2)
        {
            pannel3.SetActive(true);
        }
        else if (ftueProgression == 3)
        {
            pannel4.SetActive(true);
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
            ftueProgression++;
        }else if(ftueProgression == 1)
        {
            pannel2.SetActive(false);
            ftueProgression++;
        }
        else if (ftueProgression == 2)
        {
            pannel3.SetActive(false);
            ftueProgression++;
        }
        else if (ftueProgression == 3)
        {
            pannel4.SetActive(false);
            ftueProgression++;
        }
    }
}
