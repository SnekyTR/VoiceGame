using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    //navmesh player
    private NavMeshAgent playerNM;

    [SerializeField] private Transform[] cubePos;

    //comandos voz
    private Dictionary<string, Action> moveCmd1 = new Dictionary<string, Action>();
    private Dictionary<string, Action> moveCmd2 = new Dictionary<string, Action>();
    private KeywordRecognizer moveCmd1R;
    private KeywordRecognizer moveCmd2R;

    void Start()
    {
        playerNM = GetComponent<NavMeshAgent>();

        moveCmd1.Add("muevete a", StartMove);
        moveCmd2.Add("el cubo uno", MoveCube01);
        moveCmd2.Add("el cubo dos", MoveCube02);
        moveCmd2.Add("el cubo tres", MoveCube03);

        moveCmd1R = new KeywordRecognizer(moveCmd1.Keys.ToArray());
        moveCmd1R.OnPhraseRecognized += RecognizedVoice1;
        moveCmd2R = new KeywordRecognizer(moveCmd2.Keys.ToArray());
        moveCmd2R.OnPhraseRecognized += RecognizedVoice2;

        moveCmd1R.Start();
    }

    void Update()
    {
        
    }

    public void RecognizedVoice1(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        moveCmd1[speech.text].Invoke();
    }

    public void RecognizedVoice2(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        moveCmd2[speech.text].Invoke();
    }

    private void StartMove()
    {
        moveCmd1R.Stop();
        moveCmd2R.Start();
        Debug.Log("ok");
    }

    private void MoveCube01()
    {
        playerNM.destination = cubePos[0].position;
        moveCmd1R.Start();
        moveCmd2R.Stop();
    }

    private void MoveCube02()
    {
        playerNM.destination = cubePos[1].position;
        moveCmd1R.Start();
        moveCmd2R.Stop();
    }
    private void MoveCube03()
    {
        playerNM.destination = cubePos[2].position;
        moveCmd1R.Start();
        moveCmd2R.Stop();
    }
}
