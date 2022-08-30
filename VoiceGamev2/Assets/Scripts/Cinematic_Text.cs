using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic_Text : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProText;
    private new List<string> listed = new List<string>();
    public float letterPause = 0.2f;
    private string contentText;
    private LoadingScreen loadingScreen;
    [SerializeField] private GameObject[] backGrounds;
    public int i = 0;
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
        StartText();
    }
    public void StartText()
    {
        print("Printea");
        
        backGrounds[i].SetActive(true);
        contentText = listed[i];
        string writeThis = contentText;

        StartCoroutine(TypeSentence(writeThis, i));
    }
    IEnumerator TypeSentence(string sentence, int a)
    {
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
            print("AAA");
        }
        else
        {
            print("todavia no");
            
            StartText();
        }
    }
}
