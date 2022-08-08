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
    private bool isEnter = false;
    private PlayerMove plyMove;
    private CameraFollow gameM;
    private Dictionary<string, Action> winOrders = new Dictionary<string, Action>();
    private KeywordRecognizer wOrders;
    private Dictionary<string, Action> looseOrders = new Dictionary<string, Action>();
    private KeywordRecognizer lOrders;
    [SerializeField] private MoveDataToMain moveData;
    [SerializeField] LoadingScreen loadingScreen;

    public int totalEnemies;
    public int totalPlayers;
    // Start is called before the first frame update
    void Start()
    {
        Assign();
        WinAsignOrders();
        LooseAsignOrders();

        plyMove = GetComponent<PlayerMove>();
        gameM = GetComponent<CameraFollow>();
        loadingScreen = GameObject.Find("CanvasManager").GetComponent<LoadingScreen>();
        if (GameObject.Find("SceneConector")) moveData = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
        //wPanel = GameObject.Find("CanvasManager").transform.GetChild(7).gameObject;
        //lPanel = GameObject.Find("CanvasManager").transform.GetChild(8).gameObject;
    }
    private void Update()
    {
        if(totalEnemies == 0 && !isEnter)
        {
            WinActivateVoice();
            plyMove.ClosePlayerMove();
            gameM.CloseCameraFollow();
            isEnter = true;
        }
        if(totalPlayers == 0 && !isEnter)
        {
            LooseActivateVoice();
            plyMove.ClosePlayerMove();
            gameM.CloseCameraFollow();
            isEnter = true;
        }
    }

    public void OverTime()
    {
        LooseActivateVoice();
        plyMove.ClosePlayerMove();
        gameM.CloseCameraFollow();
        isEnter = true;
    }

    private void CloseWinLose()
    {
        if(!PlayerPrefs.HasKey("wl")) PlayerPrefs.SetInt("wl", 0);

        int ns = PlayerPrefs.GetInt("wl");
        PlayerPrefs.SetInt("wl", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasdadffsasdf" + SceneManager.GetActiveScene().name + ns, WinAsignOrders);
        Dictionary<string, Action> zero2 = new Dictionary<string, Action>();
        zero2.Add("ghjghasdfasdfqw" + SceneManager.GetActiveScene().name + ns, WinAsignOrders);

        wOrders = new KeywordRecognizer(zero1.Keys.ToArray());
        lOrders = new KeywordRecognizer(zero2.Keys.ToArray());
        wOrders.OnPhraseRecognized -= RecognizedVoice;
        lOrders.OnPhraseRecognized -= LooseRecognizedVoice;
    }
    private void WinAsignOrders()
    {
        winOrders.Add("Seguir", Continue);
        
        wOrders = new KeywordRecognizer(winOrders.Keys.ToArray());
        wOrders.OnPhraseRecognized += RecognizedVoice;
    }
    private void LooseAsignOrders()
    {
        looseOrders.Add("Salir", Retry);
        looseOrders.Add("Reintentar", Retry);
        lOrders = new KeywordRecognizer(looseOrders.Keys.ToArray());
        lOrders.OnPhraseRecognized += LooseRecognizedVoice;
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
        moveData.loadVictory = true;
        wOrders.Stop();
        //moveData.IncrementProgresion();
        CloseWinLose();
        loadingScreen.LoadScene(1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);

    }
    private void Retry()
    {
        lOrders.Stop();
        CloseWinLose();
        FailLevel();
    }
    private void Assign()
    {
        if(GameObject.Find("SceneConector")) moveData = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
    }
    public void FailLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }
}
