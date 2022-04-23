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
    private Dictionary<string, Action> startCmd = new Dictionary<string, Action>();
    private KeywordRecognizer startCmdR;

    //voice commands movement
    private Dictionary<string, Action<string>> moveCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer moveCmdR;

    //voide commands attack
    private Dictionary<string, Action> atkCmd = new Dictionary<string, Action>();
    private KeywordRecognizer atkCmdR;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        turnTxt.text = "Turn " + turn;

        //start turn
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

        startCmdR.Start();

        //player attack
        atkCmd.Add(enemyNames[0], Enemy01);

        atkCmdR = new KeywordRecognizer(atkCmd.Keys.ToArray());
        atkCmdR.OnPhraseRecognized += RecognizedVoice3;
    }

    public void SetList(Transform[] go, string[] ns)
    {
        positionTr = go;
        posNames = ns;
    }

    private bool TurnEnergy(int n)
    {
        bool isEnergy = false;
        if (playerStats.GetEnergy() >= n)
        {
            playerStats.SetEnergy(-n);
            isEnergy = true;
        }

        return isEnergy;
    }

    void Update()
    {

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
        atkCmd[speech.text].Invoke();
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

    private void MoveCasilla(string i)
    {
        int dis = (int)(Vector3.Distance(GameObject.Find(i).transform.position, transform.position)/5);
        if (GameObject.Find(i).CompareTag("Section"))
        {
            if (TurnEnergy(dis))
            {
                playerNM.destination = GameObject.Find(i).transform.position;
                startCmdR.Start();
                moveCmdR.Stop();
                stateImg.color = Color.white;
                gridA.DisableGrid();
            }
            else
            {
                startCmdR.Start();
                moveCmdR.Stop();
                stateImg.color = Color.white;
                gridA.DisableGrid();
            }
        }
        else
        {
            startCmdR.Start();
            moveCmdR.Stop();
            stateImg.color = Color.white;
            gridA.DisableGrid();
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
            GetComponent<SphereCollider>().enabled = false;
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
                GetComponent<SphereCollider>().enabled = true;
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
