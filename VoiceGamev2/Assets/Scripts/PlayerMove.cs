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
    private Animator animator;
    private Transform playerTr;

    private CameraFollow gameM;
    private GridActivation gridA;
    private Image stateImg;
    private Image range;
    private bool isOnRoute =false;

    [Header("Locations")]
    private string[] posNames;

    [Header("Enemys")]
    private GameObject target;
    private bool setTarget;

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
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        gridA = GameObject.Find("RayCast").GetComponent<GridActivation>();
        stateImg = GameObject.Find("PasarTurno").GetComponent<Image>();

        //select action
        startCmd.Add("mover", StartMove);
        startCmd.Add("muevete a", StartMove);
        startCmd.Add("atacar", StartAttack);
        startCmd.Add("ataca", StartAttack);
        startCmd.Add("ataca a", StartAttack);
        startCmd.Add("atacar a", StartAttack);
        startCmd.Add("pasar", NewTurn);

        //movement

        for(int i = 0; i < posNames.Length; i++)
        {
            moveCmd.Add(posNames[i], MoveCasilla);
        }

        startCmdR = new KeywordRecognizer(startCmd.Keys.ToArray());
        startCmdR.OnPhraseRecognized += RecognizedVoice1;
        moveCmdR = new KeywordRecognizer(moveCmd.Keys.ToArray());
        moveCmdR.OnPhraseRecognized += RecognizedVoice2;

        //player attack
        for (int i = 0; i < gameM.enemys.Count; i++)
        {
            atkCmd.Add(gameM.enemys[i].name, Enemy);
        }

        atkCmdR = new KeywordRecognizer(atkCmd.Keys.ToArray());
        atkCmdR.OnPhraseRecognized += RecognizedVoice3;

        //spells
        spellCmd.Add("meteor atack", Spells);

        spellCmdR = new KeywordRecognizer(spellCmd.Keys.ToArray());
        spellCmdR.OnPhraseRecognized += RecognizedVoice5;

        //initial voice
        startCmdR.Start();

        PlayerDeselect();
    }

    public void NewPlayer(GameObject Pl)
    {
        animator = Pl.GetComponent<Animator>();
        playerStats = Pl.GetComponent<PlayerStats>();
        playerNM = Pl.GetComponent<NavMeshAgent>();
        playerTr = Pl.transform;
    }

    public void PlayerDeselect()
    {
        if(startCmdR.IsRunning) startCmdR.Stop();
        if(moveCmdR.IsRunning) moveCmdR.Stop();
        if(atkCmdR.IsRunning) atkCmdR.Stop();
        if(spellCmdR.IsRunning) spellCmdR.Stop();
        gridA.DisableGrid();
        stateImg.color = Color.white;
    }

    public void PlayerSelect()
    {
        startCmdR.Start();
    }

    public void SetList(string[] ns)
    {
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
        if (setTarget)
        {
            Vector3 direction = target.transform.position - playerTr.transform.position;
            Quaternion rotacion = Quaternion.LookRotation(direction);
            playerTr.transform.rotation = Quaternion.Slerp(playerTr.transform.rotation, rotacion, 6f * Time.deltaTime);
        }
       if (isOnRoute && playerNM.velocity == Vector3.zero)
        {
            print("ss");
            animator.SetInteger("A_Movement", 0);
            isOnRoute = false;
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
    public void RecognizedVoice5(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        spellCmd[speech.text].Invoke(speech.text);
    }

    //move actions
    private void StartMove()
    {
        startCmdR.Stop();

        moveCmdR.Start();
        print(playerTr.name);

        gridA.EnableGrid(playerTr);
        stateImg.color = Color.blue;

    }

    private void NewTurn()
    {
        gameM.NextTurn();
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
                animator.SetInteger("A_Movement", 1);
                StartCoroutine(StopAnimation());
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

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        isOnRoute = true;
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
        for(int i = 0; i < gameM.enemys.Count; i++)
        {
            if(n == gameM.enemys[i].name)
            {
                target = gameM.enemys[i].gameObject;
                break;
            }
            else if(i == (gameM.enemys.Count - 1))
            {
                target = null;
                break;
            }
        }

        if (target != null && Vector3.Distance(playerNM.transform.position, target.transform.position) < 4)
        {
            if (TurnEnergy(5))
            {
                StartCoroutine(StartAtkAnim());
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

        if (spellCmdR.IsRunning)
        {
            spellCmdR.Stop();
        }

    }

    private IEnumerator StartAtkAnim()
    {
        animator.SetInteger("A_FireBall", 1);
        atkCmdR.Stop();

        setTarget = true;

        yield return new WaitForSeconds(0.7f);

        setTarget = false;

        target.GetComponent<EnemyStats>().SetLife(-playerStats.GetAtk());
        startCmdR.Start();

        stateImg.color = Color.white;
    }
}
