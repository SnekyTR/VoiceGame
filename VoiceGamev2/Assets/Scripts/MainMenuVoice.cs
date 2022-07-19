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
    [SerializeField] private GameObject loadGame;
    // Start is called before the first frame update
    private void Awake()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/progression.data"))
        {
            filesExist = true;
        }
        else
        {
            filesExist = false;
        }
    }
    void Start()
    {
        AddOrders();
        StartCoroutine(StartGame());
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
    private void AddOrders()
    {
        menuActions.Add("cargar partida", LoadGame);
        menuActions.Add("crear partida", CreateNewGame);
        menuActions.Add("crear", CreateNewGame);
        menuActions.Add("partida", CreateNewGame);
        menuActions.Add("kaka", CreateNewGame);
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
    private void LoadGame()
    {
        if (!filesExist) { return; }
        SceneManager.LoadScene(1);
    }
    private void CreateNewGame()
    {
        if (filesExist) { return; }
        print("Se ha cargado");
        SceneManager.LoadScene(2);
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(2);
    }

}
