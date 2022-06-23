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
    private Skills skill;

    private GridActivation gridA;
    private Image range;
    private bool isOnRoute =false;
    private string atkState;

    [Header("Locations")]
    private string[] posNames;

    [Header("Enemys")]
    [HideInInspector] public GameObject target;
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

    //spells to allies
    [HideInInspector] public bool allieSpell = false;

    void Start()
    {
        gameM = GetComponent<CameraFollow>();
        skill = GetComponent<Skills>();
        gridA = GameObject.Find("RayCast").GetComponent<GridActivation>();

        //select action
        startCmd.Add("mover", StartMove);
        startCmd.Add("muevete a", StartMove);
        startCmd.Add("atacar", StartAttack);
        startCmd.Add("ataca", StartAttack);
        startCmd.Add("ataca a", StartAttack);
        startCmd.Add("atacar a", StartAttack);

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
        for (int i = 0; i < skill.GetSkillList().Count; i++)
        {
            spellCmd.Add(skill.GetSkillList()[i], Spells);
        }

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

    public void ReasignateGrid()
    {
        if (moveCmdR.IsRunning)
        {
            gridA.DisableGrid();
            gridA.EnableGrid(playerTr);
        }
        else if (atkCmdR.IsRunning)
        {

        }
        else if (!startCmdR.IsRunning)
        {
            startCmdR.Start();
            spellCmdR.Start();
        }
    }

    public void PlayerDeselect()
    {
        if(startCmdR.IsRunning) startCmdR.Stop();
        if(moveCmdR.IsRunning) moveCmdR.Stop();
        if(atkCmdR.IsRunning) atkCmdR.Stop();
        if(spellCmdR.IsRunning) spellCmdR.Stop();
        gridA.DisableGrid();
        if(playerTr != null)playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
        if(playerTr != null)playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.black;
    }

    public void PlayerSelect()
    {
        startCmdR.Start();
        spellCmdR.Start();
    }

    public void SetList(string[] ns)
    {
        posNames = ns;
    }

    private bool TurnEnergy(float n)
    {
        bool isEnergy = false;
        if (playerStats.GetEnergy(1) >= n)
        {
            playerStats.SetEnergy(-n);
            isEnergy = true;
        }
        
        return isEnergy;
    }

    private bool TurnEnergyActions(float n)
    {
        bool isEnergy = false;
        if (playerStats.GetEnergy(2) >= n)
        {
            playerStats.SetEnergyActions(-n);
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

    public bool IsMoving()
    {
        return moveCmdR.IsRunning;
    }

    //move actions
    private void StartMove()
    {
        startCmdR.Stop();

        moveCmdR.Start();
        print(playerTr.name);

        gridA.EnableGrid(playerTr);
        playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.blue;

        gameM.NewParent(playerTr, 2);
    }

    //movement
    private void MoveCasilla(string i)
    {
        float energy = (Vector3.Distance(GameObject.Find(i).transform.position, playerTr.position) / 2);
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

                gameM.NewParent(playerTr, 1);

                startCmdR.Start();

                moveCmdR.Stop();

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.black;
                gridA.DisableGrid();
            }
            else
            {
                startCmdR.Start();

                moveCmdR.Stop();
                gameM.NewParent(playerTr, 1);

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.black;
                gridA.DisableGrid();
            }
        }
        else
        {
            startCmdR.Start();

            moveCmdR.Stop();
            gameM.NewParent(playerTr, 1);

            playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.black;
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
        spellCmdR.Stop();

        atkCmdR.Start();

        playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;

        atkState = "atk";

        gameM.NewParent(playerTr, 1);
    }
    //spells actions
    private void Spells(string n)
    {
        spellCmdR.Stop();

        atkState = n;

        if(n == "curar" || n == "revivir")
        {
            allieSpell = true;
        }
        else
        {
            atkCmdR.Start();
        }
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

        if (target != null && Vector3.Distance(playerNM.transform.position, target.transform.position) < skill.GetRanges(atkState))
        {
            if (TurnEnergyActions(skill.GetCost(atkState)))
            {
                StartCoroutine(StartAtkAnim());
                atkCmdR.Stop();
            }
            else
            {
                startCmdR.Start();
                spellCmdR.Start();

                atkCmdR.Stop();

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
            }
        }
        
        else
        {
            startCmdR.Start();
            spellCmdR.Start();

            atkCmdR.Stop();

            playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
            print("fuera de alcance");
        }

        if (spellCmdR.IsRunning)
        {
            spellCmdR.Stop();
        }

    }

    public void Allie(string n)
    {
        for (int i = 0; i < gameM.players.Count; i++)
        {
            if (n == gameM.players[i].name)
            {
                target = gameM.players[i].gameObject;
                break;
            }
            else if (i == (gameM.players.Count - 1))
            {
                target = null;
                break;
            }
        }

        if(target != null && atkState == "curar")
        {
            if (TurnEnergyActions(3))
            {
                target.GetComponent<PlayerStats>().SetLife(5);
                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
            }
            else
            {
                startCmdR.Start();
                spellCmdR.Start();

                atkCmdR.Stop();

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
            }
        }
        else if(target == null && atkState == "revivir")
        {
            if (TurnEnergyActions(4.5f))
            {
                target = GameObject.Find(n);

                gameM.players.Add(target.transform);
                target.GetComponent<Animator>().SetInteger("A_Death", 0);
                target.GetComponent<NavMeshAgent>().enabled = true;
                target.GetComponent<PlayerStats>().SetLife(5);
                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
            }
            else
            {
                startCmdR.Start();
                spellCmdR.Start();

                atkCmdR.Stop();

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
            }
        }
    }

    private IEnumerator StartAtkAnim()
    {
        skill.SelectSkill(atkState);

        setTarget = true;

        yield return new WaitForSeconds(1.5f);

        setTarget = false;

        startCmdR.Start();
        spellCmdR.Start();

        playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
    }
}
