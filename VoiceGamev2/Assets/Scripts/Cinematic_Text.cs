using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class Cinematic_Text : MonoBehaviour
{
    private Dictionary<string, Action> skipActions = new Dictionary<string, Action>();
    public KeywordRecognizer skip;
    public TextMeshProUGUI[] textMeshProText;
    private new List<string> listed = new List<string>();
    public float letterPause = 0.2f;
    private string contentText;
    private LoadingScreen loadingScreen;
    private PlayableDirector skippedText;
    [SerializeField] private GameObject[] backGrounds;
    public int i = 0;
    private bool skipped;
    private void Awake()
    {
        loadingScreen = gameObject.GetComponent<LoadingScreen>();
        listed.Add("Este es el reino de Laryan, la tierra de los cuatro ducados. Al norte está el ducado de Krenid, " +
    "hogar de la orden de los hijos del gigante, protectores del reino contra las tierras oscuras.");
        listed.Add("Laryan está protegido por la magia del árbol de la luz, que detiene el avance de la oscuridad con su poder, " +
            "Se cree que una vez que el árbol muera, la oscuridad invadirá el reino y lo destruirá.");
        listed.Add("Nuestro protagonista, llamado Magnus, ha conseguido escapar de un ataque de las criaturas de la oscuridad para llevar la última semilla del árbol a la capital." +
            "Por el camino se enfrentará a grandes peligros y conseguirá poderosos aliados.");
    }
    /*public void StartText()
    {
        print("Printea");
        string writeThis = listed[actualText];
        StartCoroutine(TypeSentence(writeThis));
    }
    IEnumerator TypeSentence(string sentence)
    {
        yield return new WaitForSeconds(1.5f);
        foreach (char letter in sentence.ToCharArray())
        {
            textMeshProText.text = sentence;
            textMeshProText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }
    public void IncrementText()
    {
        actualText++;
        StartText();

    }*/
    private void Start()
    {
        skippedText = GameObject.Find("TimeLine").GetComponent<PlayableDirector>();
        StartText();
        skipActions.Add("Saltar", SkipScene);
        skip = new KeywordRecognizer(skipActions.Keys.ToArray());
        skip.OnPhraseRecognized += RecognizedVoice;
    }
    private void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        skipActions[speech.text].Invoke();
    }
    private void SkipScene()
    { 
        if (skipped) { return; }
        skipped = true;
        print("Se esquipea");
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, SkipScene);
        skip = new KeywordRecognizer(zero1.Keys.ToArray());
        skip.OnPhraseRecognized += RecognizedVoice;
        loadingScreen.LoadScene(2);
    }
    public void StartText()
    {        
        backGrounds[i].SetActive(true);
        contentText = listed[i];
        string writeThis = contentText;
        StartCoroutine(skipText());
        StartCoroutine(TypeSentence(writeThis, i));
    }
    IEnumerator TypeSentence(string sentence, int a)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in sentence.ToCharArray())
        {
            textMeshProText[a].text += letter;
            yield return new WaitForSeconds(letterPause);
        }
        yield return new WaitForSeconds(3f);
        backGrounds[a].SetActive(false);
        print(backGrounds[a].name);
        i++;
        if (i >= backGrounds.Length)
        {
            loadingScreen.LoadScene(2);
        }
        else
        {            
            StartText();
        }
    }
    IEnumerator skipText()
    {

        
        yield return new WaitForSeconds(1.5f);
        skippedText.Play();
        skip.Start();
    }
}
