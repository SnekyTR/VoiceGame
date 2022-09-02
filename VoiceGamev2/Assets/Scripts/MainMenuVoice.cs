using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class MainMenuVoice : MonoBehaviour
{
    private Dictionary<string, Action> menuActions = new Dictionary<string, Action>();
    private KeywordRecognizer menuKeyword;
    public bool filesExist;
    private bool deleting = false;

    [SerializeField] private GameObject createGame;
    [SerializeField] private GameObject Vagnar;
    [SerializeField] private GameObject loadGame;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject confirmPanel;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/progression.data"))
        {
            filesExist = true;
            LoadData();
        }
        else
        {
            filesExist = false;
        }
        AddOrders();
        //loadGame.SetActive(false);
        if (!filesExist)
        {
            loadGame.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            loadGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadData()
    {
        GameProgressionData data =  SaveSystem.LoadProgression();
        if(data.progressionNumber >= 3)
        {
            Vagnar.SetActive(true);
        }
    }
    private void AddOrders()
    {
        menuActions.Add("cargar partida", LoadGame);
        menuActions.Add("crear partida", CreateNewGame);
        menuActions.Add("borrar", ConfirmDelete);
        menuActions.Add("no", BackNormal);
        menuActions.Add("salir", ExitGame);
        menuKeyword = new KeywordRecognizer(menuActions.Keys.ToArray());
        menuKeyword.OnPhraseRecognized += RecognizedVoice;
        menuKeyword.Start();
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        menuActions[speech.text].Invoke();
    }

    private void DeleteFiles()
    {
        loadGame.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        loadGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, 0.5f);
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach(FileInfo file in di.GetFiles())
        {
            if (file.Name != "Player.log")
            {
                print(file.Name);
                file.Delete();
            }           
        }
        filesExist = false;

        CreateNewGame();
    }
    private void LoadGame()
    {
        if (!filesExist) { return; }
        CloseOrders();
        loadingScreen.LoadScene(1);
    }
    private void CreateNewGame()
    {

        if (filesExist) {
            print("Se elimina");
            PopDelete();
        }
        else
        {
            CloseOrders();
            print("se crea");
            loadingScreen.LoadScene(7);
        }
    }
    private void ExitGame()
    {
        Application.Quit();
    }
    private void PopDelete()
    {
        confirmPanel.SetActive(true);
        deleting = true;
    }
    private void ConfirmDelete()
    {
        if (!deleting) return;
        confirmPanel.SetActive(false);
        DeleteFiles();
    }
    private void BackNormal()
    {
        if (!deleting) return;
        confirmPanel.SetActive(false);
        deleting = false;
    }
    private void CloseOrders()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, CreateNewGame);
        menuKeyword = new KeywordRecognizer(zero1.Keys.ToArray());
        menuKeyword.OnPhraseRecognized += RecognizedVoice;
    }

}
