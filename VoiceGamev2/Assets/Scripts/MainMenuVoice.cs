using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private GameObject createGame;
    [SerializeField] private GameObject Vagnar;
    [SerializeField] private GameObject loadGame;
    [SerializeField] private LoadingScreen loadingScreen;
    // Start is called before the first frame update
    private void Awake()
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
    }
    void Start()
    {
        AddOrders();
        //loadGame.SetActive(false);
        if (filesExist)
        {
            createGame.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            createGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(255, 255, 255, 0.5f);
        }
        else
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
        GameProgressionData data = SaveSystem.LoadProgression();
        if(data.progressionNumber >= 3)
        {
            Vagnar.SetActive(true);
        }
    }
    private void AddOrders()
    {
        menuActions.Add("cargar partida", LoadGame);
        menuActions.Add("crear partida", CreateNewGame);
        menuActions.Add("borrar datos", DeleteFiles);
        menuActions.Add("salir", ExitGame);
        menuKeyword = new KeywordRecognizer(menuActions.Keys.ToArray());
        menuKeyword.OnPhraseRecognized += RecognizedVoice;
        menuKeyword.Start();
        print("Se ha n aádido");
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        menuActions[speech.text].Invoke();
    }
    IEnumerator caca()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
    }
    private void DeleteFiles()
    {
        //UnityEditor.FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        //filesExist = false;
    }
    private void LoadGame()
    {
        if (!filesExist) { return; }
        CloseOrders();
        loadingScreen.LoadScene(1);
    }
    private void CreateNewGame()
    {
        if (filesExist) { return; }
        CloseOrders();
        loadingScreen.LoadScene(2);
       
    }
    private void ExitGame()
    {
        Application.Quit();
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
