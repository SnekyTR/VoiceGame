using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class OptionsCombat : MonoBehaviour
{
    private GameObject optionsPanel;
    private Dictionary<string, Action> optOrders = new Dictionary<string, Action>();
    public KeywordRecognizer optDict;
    private LoadingScreen loadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = GetComponent<LoadingScreen>();
        AddOptOrders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AddOptOrders()
    {
        optOrders.Add("Continuar", Continue);
        optOrders.Add("Opciones", Options);
        optOrders.Add("ir al mapa", GotoMap);
        optOrders.Add("salir del juego", ExitGame);
        optDict = new KeywordRecognizer(optOrders.Keys.ToArray());
        optDict.OnPhraseRecognized += RecognizedOptions;
        optDict.Start();
    }
    private void RecognizedOptions(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        optOrders[speech.text].Invoke();
    }
    private void Continue()
    {
        optionsPanel.SetActive(false);
    }
    private void Options()
    {
        
    }
    private void GotoMap()
    {
        loadingScreen.LoadScene(1);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
    public void CloseOrders()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, ExitGame);
        optDict = new KeywordRecognizer(zero1.Keys.ToArray());
        optDict.OnPhraseRecognized += RecognizedOptions;
    }
}
