using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class WinLoose : MonoBehaviour
{
    
    private Dictionary<string, Action> orders = new Dictionary<string, Action>();
    private KeywordRecognizer UIOrders;
    private Progression progression;
    // Start is called before the first frame update
    void Start()
    {
        progression = GameObject.Find("GameSaver").GetComponent<Progression>();
    }

    private void AsignOrders()
    {
        orders.Add("Continuar", Continue);
        orders.Add("Reintentar", Retry);
        orders.Add("Salir", Exit);
        UIOrders = new KeywordRecognizer(orders.Keys.ToArray());
        UIOrders.OnPhraseRecognized += RecognizedVoice;
        UIOrders.Start();
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        orders[speech.text].Invoke();
    }
    private void Continue()
    {
        progression.IncrementProgresion(1);
        SceneManager.LoadScene(0);
        UIOrders.Stop();
    }
    private void Retry()
    {
        UIOrders.Stop();
    }
    private void Exit()
    {
        UIOrders.Stop();
    }
}
