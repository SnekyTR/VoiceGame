using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    public GameObject floatingText;
    public Spells spells;
    [SerializeField] private String text1;
    [SerializeField] private String text2;
    [SerializeField] private String text3;
    [SerializeField] private String text4;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private bool hola;
    TextMesh caca;
    private bool nope;
    // Start is called before the first frame update
    void Start()
    {
        caca = floatingText.GetComponent<TextMesh>();
        actions.Add("ola", Hola);
        actions.Add("si", Yep);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player"){
            keywordRecognizer.Start();
        }
    }
    // Start NPC Conversation #1
    private void Hola()
    {
        if (nope)
        {
            StopCoroutine(DeleteConversations());
            
            Debug.Log("Recuerda dirigirte al a iglesia y decir las palabras secretas");
            caca.text = text4;
            StartCoroutine(DeleteConversations());
        }
        else
        {
            caca.text = text1; StartCoroutine(DeleteConversations());
            Debug.Log("Hola David estas bien?");
            hola = true;
        }
    }
    private void Yep()
    {
        StopCoroutine(DeleteConversations());
        bool yep = true;
        if (hola)
        {
            
            caca.text = text2;
            StartCoroutine(DeleteConversations());
            hola = false;
            Debug.Log("Me parece fabuloso, Tengo algo que pedirte, podrias? ");
            yep = true;
            
        }else if (yep) { StopCoroutine(DeleteConversations()); yep = false; caca.text = text3; Debug.Log("Ves a ver al viejo en la iglesia y recuerda decirle las palabras"); nope = true; StartCoroutine(DeleteConversations()); }
    }
    //End NPC COnversation #1
    public void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    IEnumerator DeleteConversations()
    {
        yield return new WaitForSeconds(10f);
        caca.text = "";
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Ha Salido");
            keywordRecognizer.Stop();
        }
    }
}
