using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //player
    private NavMeshAgent playerNM;
    private PlayerStats playerStats;
    [SerializeField] private Image stateImg;
    [SerializeField] private Text turnTxt;
    private int turn = 1;

    [Header("Locations")]
    [SerializeField] private Transform[] positionTr;
    [SerializeField] private string[] posNames;

    [Header("Enemys")]
    [SerializeField] private Transform[] enemyTr;
    [SerializeField] private string[] enemyNames;
    private GameObject target;
    private bool inAtk;

    //voice commands start turn
    private Dictionary<string, Action> startCmd1 = new Dictionary<string, Action>();
    private KeywordRecognizer startCmdR;

    //voice commands movement
    private Dictionary<string, Action> moveCmd = new Dictionary<string, Action>();
    private KeywordRecognizer moveCmdR;

    //voide commands attack
    private Dictionary<string, Action> atkCmd = new Dictionary<string, Action>();
    private KeywordRecognizer atkCmdR;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        turnTxt.text = "Turn " + turn;

        //start turn
        startCmd1.Add("mover", StartMove);
        startCmd1.Add("atacar", StartAttack);

        //movement
        playerNM = GetComponent<NavMeshAgent>();

        moveCmd.Add(posNames[0], Move01);
        moveCmd.Add(posNames[1], Move02);
        moveCmd.Add(posNames[2], Move03);

        startCmdR = new KeywordRecognizer(startCmd1.Keys.ToArray());
        startCmdR.OnPhraseRecognized += RecognizedVoice1;
        moveCmdR = new KeywordRecognizer(moveCmd.Keys.ToArray());
        moveCmdR.OnPhraseRecognized += RecognizedVoice2;

        startCmdR.Start();

        //player attack
        atkCmd.Add(enemyNames[0], Enemy01);

        atkCmdR = new KeywordRecognizer(atkCmd.Keys.ToArray());
        atkCmdR.OnPhraseRecognized += RecognizedVoice3;
    }

    private bool TurnEnergy(int n)
    {
        bool isEnergy = false;
        if (playerStats.GetEnergy() > n)
        {
            playerStats.SetEnergy(-n);
            isEnergy = true;
        }
        else
        {
            playerStats.FullEnergy();
            isEnergy = false;
            turn++;
            turnTxt.text = "Turno " + turn;
        }

        return isEnergy;
    }

    void Update()
    {

    }

    public void RecognizedVoice1(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        startCmd1[speech.text].Invoke();
    }

    public void RecognizedVoice2(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        moveCmd[speech.text].Invoke();
    }
    public void RecognizedVoice3(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        atkCmd[speech.text].Invoke();
    }

    //move actions
    private void StartMove()
    {
        startCmdR.Stop();
        moveCmdR.Start();
        stateImg.color = Color.blue;
    }
    private void Move01()
    {
        if (TurnEnergy(3))
        {
            playerNM.destination = positionTr[0].position;
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
        }
        else
        {
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
        }
    }
    private void Move02()
    {
        if (TurnEnergy(4))
        {
            playerNM.destination = positionTr[1].position;
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
        }
        else
        {
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
        }
    }
    private void Move03()
    {
        if (TurnEnergy(5))
        {
            playerNM.destination = positionTr[2].position;
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
        }
        else
        {
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
        }
    }

    //attack actions
    private void StartAttack()
    {
        startCmdR.Stop();
        atkCmdR.Start();
        stateImg.color = Color.red;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (inAtk && other.gameObject == target)
        {
            target.GetComponent<EnemyStats>().SetLife(-playerStats.GetAtk());
            startCmdR.Start();
            atkCmdR.Stop();
            stateImg.color = Color.white;
            playerNM.destination = transform.position;
        }
    }
    private void Enemy01()
    {
        if (Vector3.Distance(playerNM.transform.position, enemyTr[0].position) < 10)
        {
            if (TurnEnergy(5))
            {
                playerNM.destination = enemyTr[0].position;
                target = enemyTr[0].gameObject;
                inAtk = true;
            }
            else
            {
                startCmdR.Start();
                atkCmdR.Stop();
                stateImg.color = Color.white;
            }
        }
        else
        {
            startCmdR.Start();
            atkCmdR.Stop();
            stateImg.color = Color.white;
            print("fuera de alcance");
        }
    }
}
