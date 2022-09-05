using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class OptionsCombat : MonoBehaviour
{
    public GameObject optionsPanel;
    private Dictionary<string, Action> optOrders = new Dictionary<string, Action>();
    public KeywordRecognizer optDict;
    private LoadingScreen loadingScreen;
    private bool isOnPause;
    private CameraFollow gameM;
    private PlayerMove plMove;

    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = GetComponent<LoadingScreen>();
        AddOptOrders();
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plMove = gameM.GetComponent<PlayerMove>();
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
        isOnPause = false;

        optionsPanel.SetActive(false);

        plMove.isPaused = false;
        gameM.isPaused = false;
        plMove.enabled = true;
        gameM.enabled = true;

        Time.timeScale = 1f;
    }
    private void Options()
    {
        isOnPause = true;

        Time.timeScale = 0f;

        plMove.isPaused = true;
        gameM.isPaused = true;
        plMove.enabled = false;
        gameM.enabled = false;

        optionsPanel.SetActive(true);
    }
    private void GotoMap()
    {
        if (isOnPause)
        {
            loadingScreen.LoadScene(1);
        }
    }
    private void ExitGame()
    {
        if (isOnPause)
        {
            Application.Quit();
        }
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

    public bool GetPause()
    {
        return isOnPause;
    }
}
