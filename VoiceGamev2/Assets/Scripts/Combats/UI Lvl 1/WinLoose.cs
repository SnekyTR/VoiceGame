using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class WinLoose : MonoBehaviour
{
    [SerializeField] private GameObject wPanel;
    [SerializeField] private GameObject lPanel;

    private Dictionary<string, Action> winOrders = new Dictionary<string, Action>();
    private KeywordRecognizer wOrders;
    private Dictionary<string, Action> looseOrders = new Dictionary<string, Action>();
    private KeywordRecognizer lOrders;
    [SerializeField] private MoveDataToMain moveData;
    public int totalEnemies;
    // Start is called before the first frame update
    void Start()
    {
        Assign();
        WinAsignOrders();
        LooseAsignOrders();
    }
    private void Update()
    {
        if(totalEnemies == 0)
        {
            WinActivateVoice();
        }
    }
    private void WinAsignOrders()
    {
        winOrders.Add("Continuar", Continue);

        wOrders = new KeywordRecognizer(winOrders.Keys.ToArray());
        wOrders.OnPhraseRecognized += RecognizedVoice;
    }
    private void LooseAsignOrders()
    {
        looseOrders.Add("Salir", Exit);
        lOrders = new KeywordRecognizer(looseOrders.Keys.ToArray());
        lOrders.OnPhraseRecognized += RecognizedVoice;
    }
    public void WinActivateVoice()
    {
        wPanel.SetActive(true);
        wOrders.Start();
    }
    public void LooseActivateVoice()
    {
        lPanel.SetActive(true);
        lOrders.Start();
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        winOrders[speech.text].Invoke();
    }
    public void LooseRecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        looseOrders[speech.text].Invoke();
    }
    private void Continue()
    {
        wOrders.Stop();
        moveData.IncrementProgresion();
        
    }

    private void Exit()
    {
        lOrders.Stop();
        SceneManager.LoadScene(0);
    }
    private void Assign()
    {
        moveData = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
        //wPanel = GameObject.Find("VPanel");
        //lPanel = GameObject.Find("LPanel");
    }
}
