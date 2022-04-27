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
    public GridActivation gridA;
    [SerializeField] private Image stateImg;
    [SerializeField] private Text turnTxt;
    [SerializeField] private Image range;
    private int turn = 1;

    [Header("Locations")]
    private Transform[] positionTr;
    private string[] posNames;

    [Header("Enemys")]
    [SerializeField] private Transform[] enemyTr;
    [SerializeField] private string[] enemyNames;
    private GameObject target;
    private bool inAtk;

    //voice commands start turn
    private Dictionary<string, Action<string>> selectPJCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer selectPJCmdR;

    //voice commands select action
    private Dictionary<string, Action> startCmd = new Dictionary<string, Action>();
    private KeywordRecognizer startCmdR;

    //voice commands movement
    private Dictionary<string, Action<string>> moveCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer moveCmdR;

    //voice commands attack
    private Dictionary<string, Action<string>> atkCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer atkCmdR;

    //voice commands spells
    private Dictionary<string, Action<string>> spellCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer spellCmdR;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        turnTxt.text = "Turn " + turn;

        //start turn
        selectPJCmd.Add("player uno", SelectPJ);

        selectPJCmdR = new KeywordRecognizer(selectPJCmd.Keys.ToArray());
        selectPJCmdR.OnPhraseRecognized += RecognizedVoice4;

        //select action
        startCmd.Add("mover", StartMove);
        startCmd.Add("muevete a", StartMove);
        startCmd.Add("atacar", StartAttack);
        startCmd.Add("ataca", StartAttack);
        startCmd.Add("ataca a", StartAttack);
        startCmd.Add("atacar a", StartAttack);
        startCmd.Add("nuevo turno", NewTurn);

        //movement
        playerNM = GetComponent<NavMeshAgent>();

        for(int i = 0; i < posNames.Length; i++)
        {
            moveCmd.Add(posNames[i], MoveCasilla);
        }

        startCmdR = new KeywordRecognizer(startCmd.Keys.ToArray());
        startCmdR.OnPhraseRecognized += RecognizedVoice1;
        moveCmdR = new KeywordRecognizer(moveCmd.Keys.ToArray());
        moveCmdR.OnPhraseRecognized += RecognizedVoice2;

        //player attack
        atkCmd.Add(enemyNames[0], Enemy);

        atkCmdR = new KeywordRecognizer(atkCmd.Keys.ToArray());
        atkCmdR.OnPhraseRecognized += RecognizedVoice3;

        //spells
        spellCmd.Add("meteor atack", Spells);

        spellCmdR = new KeywordRecognizer(spellCmd.Keys.ToArray());
        spellCmdR.OnPhraseRecognized += RecognizedVoice5;

        //initial voice
        selectPJCmdR.Start();
    }

    public void SetList(Transform[] go, string[] ns)
    {
        positionTr = go;
        posNames = ns;
    }

    private bool TurnEnergy(float n)
    {
        bool isEnergy = false;
        if (playerStats.GetEnergy() >= n)
        {
            playerStats.SetEnergy(-n);
            isEnergy = true;
        }

        return isEnergy;
    }

    void LateUpdate()
    {
        if (playerNM.isStopped)
        {
            print("hi");
        }
    }

    public void RecognizedVoice1(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        startCmd[speech.text].Invoke();
    }

    public void RecognizedVoice2(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        
        moveCmd[speech.text].Invoke(speech.text);
    }
    public void RecognizedVoice3(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        atkCmd[speech.text].Invoke(speech.text);
    }
    public void RecognizedVoice4(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        selectPJCmd[speech.text].Invoke(speech.text);
    }
    public void RecognizedVoice5(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        spellCmd[speech.text].Invoke(speech.text);
    }

    //select pj actions
    private void SelectPJ(string n)
    {
        selectPJCmdR.Stop();
        startCmdR.Start();
    }

    //move actions
    private void StartMove()
    {
        startCmdR.Stop();
        moveCmdR.Start();
        gridA.EnableGrid();
        stateImg.color = Color.blue;

    }

    private void NewTurn()
    {
        playerStats.FullEnergy();
        turn++;
        turnTxt.text = "Turno " + turn;
    }
    //movement
    private void MoveCasilla(string i)
    {
        float energy = (Vector3.Distance(GameObject.Find(i).transform.position, transform.position) / 2);
        if (energy > ((int)energy + 0.1f) && energy < ((int)energy + 0.7f))
        {
            energy = (int)energy + 0.5f;
        }
        else if (energy > ((int)energy + 0.7f)) energy = (int)energy + 1;
        else energy = (int)energy;   //formula energia distancia
        if (GameObject.Find(i).CompareTag("Section"))
        {
            if (TurnEnergy(energy))
            {
                playerNM.destination = GameObject.Find(i).transform.position;
                selectPJCmdR.Start();
                moveCmdR.Stop();
                stateImg.color = Color.white;
                gridA.DisableGrid();
            }
            else
            {
                selectPJCmdR.Start();
                moveCmdR.Stop();
                stateImg.color = Color.white;
                gridA.DisableGrid();
            }
        }
        else
        {
            selectPJCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
            gridA.DisableGrid();
        }
    }
    //attack actions
    private void StartAttack()
    {
        startCmdR.Stop();
        spellCmdR.Start();
        atkCmdR.Start();
        stateImg.color = Color.red;
    }
    //spells actions
    private void Spells(string n)
    {
        spellCmdR.Stop();
        stateImg.color = Color.red;
    }
    private void Enemy(string n)
    {
        if (Vector3.Distance(playerNM.transform.position, enemyTr[0].position) < 5)
        {
            if (TurnEnergy(5))
            {
                target = enemyTr[0].gameObject;
                target.GetComponent<EnemyStats>().SetLife(-playerStats.GetAtk());
                selectPJCmdR.Start();
                atkCmdR.Stop();
                stateImg.color = Color.white;
            }
            else
            {
                selectPJCmdR.Start();
                atkCmdR.Stop();
                stateImg.color = Color.white;
            }
        }
        else
        {
            selectPJCmdR.Start();
            atkCmdR.Stop();
            stateImg.color = Color.white;
            print("fuera de alcance");
        }

        if (spellCmdR.IsRunning) spellCmdR.Stop();
    }
}
