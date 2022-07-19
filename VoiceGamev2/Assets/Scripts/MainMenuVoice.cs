using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class MainMenuVoice : MonoBehaviour
{
    private Dictionary<string, Action> menuActions = new Dictionary<string, Action>();
    private KeywordRecognizer menuKeyword;
    // Start is called before the first frame update
    void Start()
    {
        AddOrders();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AddOrders()
    {
        menuActions.Add("cargar partida", CreateNewGame);
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
        SceneManager.LoadScene(2);
    }
    private void CreateNewGame()
    {
        print("Se ha cargado");
        SceneManager.LoadScene(2);
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(2);
    }
}
