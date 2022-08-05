using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //player
    private NavMeshAgent playerNM;
    private PlayerStats playerStats;
    private Animator animator;
    private Transform playerTr;
    private CameraFollow gameM;
    public AudioSource audioSource;
    private Skills skill;
    [HideInInspector]public CombatTimer timerC;
    public AudioClip moveSteps;

    private GridActivation gridA;
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

    //tutorial
    [HideInInspector] public bool moveRestriction, atkRestriction, spellRestriction, move2Restriction, atk2Restriction;
    [HideInInspector] public bool moveActive, atkActive, spellActive, move2Active, atk0Active;

    [SerializeField] private Color blueC, redC, defaultC;

    int mask;

    void Start()
    {
        gameM = GetComponent<CameraFollow>();
        skill = GetComponent<Skills>();
        gridA = GameObject.Find("RayCast").GetComponent<GridActivation>();
        timerC = GameObject.Find("CanvasManager").GetComponent<CombatTimer>();
        audioSource = gameM.gameObject.GetComponent<AudioSource>();

        //select action
        startCmd.Add("mover", StartMove);
        startCmd.Add("muevete a", StartMove);
        startCmd.Add("atacar", StartAttack);
        startCmd.Add("ataca", StartAttack);
        startCmd.Add("ataca a", StartAttack);
        startCmd.Add("atacar a", StartAttack);
        startCmd.Add("desbloquear", Unlook);

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

        mask = 1 << LayerMask.NameToLayer("Occlude");
        mask |= 1 << LayerMask.NameToLayer("Enemy");
    }

    public void ClosePlayerMove()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, Unlook);
        Dictionary<string, Action> zero2 = new Dictionary<string, Action>();
        zero2.Add("ghjgh" + SceneManager.GetActiveScene().name+ ns, Unlook);
        Dictionary<string, Action> zero3 = new Dictionary<string, Action>();
        zero3.Add("bfdgbdfbd" + SceneManager.GetActiveScene().name + ns, Unlook);
        Dictionary<string, Action> zero4 = new Dictionary<string, Action>();
        zero4.Add("qwerqwer" + SceneManager.GetActiveScene().name + ns, Unlook);
        startCmdR = new KeywordRecognizer(zero1.Keys.ToArray());
        moveCmdR = new KeywordRecognizer(zero2.Keys.ToArray());
        atkCmdR = new KeywordRecognizer(zero3.Keys.ToArray());
        spellCmdR = new KeywordRecognizer(zero4.Keys.ToArray());
        startCmdR.OnPhraseRecognized -= RecognizedVoice1;
        moveCmdR.OnPhraseRecognized -= RecognizedVoice2;
        atkCmdR.OnPhraseRecognized -= RecognizedVoice3;
        spellCmdR.OnPhraseRecognized -= RecognizedVoice5;
    }

    public void Unlook()
    {
        playerStats.strengthPoints = 10;
        playerStats.agilityPoints = 10;
        playerStats.intellectPoints = 10;
    }

    public void NewPlayer(GameObject Pl)
    {
        animator = Pl.GetComponent<Animator>();
        playerStats = Pl.GetComponent<PlayerStats>();
        playerNM = Pl.GetComponent<NavMeshAgent>();
        playerTr = Pl.transform;
    }

    public string GetAtkState()
    {
        return atkState;
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
        gridA.DisableAtkGrid();
        gridA.DisableGrid();
        if(playerTr != null)playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
        if(playerTr != null)playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.black;
    }

    public void PlayerSelect()
    {
        startCmdR.Start();
        spellCmdR.Start();
        print("Se ha seleccionado");
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
            gameM.CameraPos1();
            audioSource.Stop();

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
    private void StartMove()        //llamar a movimiento
    {
        if (moveRestriction) return;
        moveActive = true;
        atkActive = false;
        spellActive = false;
        move2Active = false;
        atk0Active = false;

        if(playerStats.GetEnergy(1) <= 0.5f)
        {
            timerC.NoEnergyMove();
            return;
        }

        startCmdR.Stop();

        moveCmdR.Start();
        spellCmdR.Stop();

        gridA.EnableGrid(playerTr);
        playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = blueC;

        gameM.CameraPos2();
        gameM.CameraAddEnemys();
    }

    //movement
    private void MoveCasilla(string i)
    {
        if (move2Restriction) return;
        audioSource.clip = moveSteps;
        audioSource.volume = 0.2f;
        audioSource.Play();
        float energy = (Vector3.Distance(GameObject.Find(i).transform.position, playerTr.position) / 2);
        if (energy > ((int)energy + 0.1f) && energy < ((int)energy + 0.7f))
        {
            energy = (int)energy + 0.5f;
        }
        else if (energy > ((int)energy + 0.7f)) energy = (int)energy + 1;       
        else energy = (int)energy;   //formula energia distancia

        print(energy);
        if (GameObject.Find(i).transform.tag == "Section")
        {
            print(GameObject.Find(i).transform.tag);
            if (TurnEnergy(energy))
            {
                animator.SetInteger("A_Movement", 1);
                StartCoroutine(StopAnimation());
                gameM.CameraPos2();
                playerNM.destination = GameObject.Find(i).transform.position;

                startCmdR.Start();
                spellCmdR.Start();

                moveCmdR.Stop();

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(1).GetComponent<Image>().color = defaultC;
                gridA.DisableGrid();

                moveActive = false;
                atkActive = false;
                spellActive = false;
                move2Active = true;
                atk0Active = false;
            }
            else
            {
                timerC.SectionIsFar();
            }
        }
        else
        {
            timerC.SectionIsFar();
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
        if (atkRestriction) return;

        moveActive = false;
        atkActive = false;
        spellActive = false;
        move2Active = false;
        atk0Active = true;

        if (playerStats.GetEnergy(2) < skill.GetCost("atk"))
        {
            timerC.NoEnergyAction();
            return;
        }

        gameM.CameraPos2();
        gameM.CameraAddEnemys();

        skill.ShowRanges(skill.GetRanges("atk"));
        gridA.EnableAtkGrid(playerTr, skill.GetRanges("atk"));


        startCmdR.Stop();
        spellCmdR.Stop();

        atkCmdR.Start();

        playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = redC;

        atkState = "atk";
    }

    //spells actions
    private void Spells(string n)
    {
        if (spellRestriction) return;

        if (!skill.ValidationSkill(n)) return;

        moveActive = false;
        atkActive = false;
        spellActive = true;
        move2Active = false;

        if (playerStats.GetEnergy(2) < skill.GetCost(n))
        {
            timerC.NoEnergyAction();
            return;
        }

        skill.SetSkillSelected(n);

        spellCmdR.Stop();
        startCmdR.Stop();

        atkState = n;

        if(n == "Curar" || n == "Revivir")
        {
            allieSpell = true;

            gameM.CameraPos2();

            skill.ShowRangesAllie(skill.GetRanges(n));
            gridA.EnableAtkGrid(playerTr, skill.GetRanges(n));
        }
        else if(n == "Aumento de fuerza" || n == "Instinto asesino" || n == "Sacrificio de sangre")
        {
            StartCoroutine(SelfBuffs());
            TurnEnergyActions(skill.GetCost(atkState));
            skill.UnShowRange();
            if (!gameM.playerUseMagic) gameM.playerUseMagic = true;
        }
        else
        {
            gameM.CameraPos2();
            gameM.CameraAddEnemys();

            skill.ShowRanges(skill.GetRanges(n));
            gridA.EnableAtkGrid(playerTr, skill.GetRanges(n));

            atkCmdR.Start();
        }
    }

    private void Enemy(string n)
    {
        if (atk2Restriction) return;

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

        RaycastHit hit;
        Vector3 newPos = playerTr.position;
        newPos.y += 1;
        Vector3 newDir = target.transform.position - playerTr.position;
        if (Physics.Raycast(newPos, newDir, out hit, 1000f, mask))
        {
            if (hit.transform.tag != "Enemy")
            {
                return;
            }
        }

        if (target != null && Vector3.Distance(playerNM.transform.position, target.transform.position) < skill.GetRanges(atkState))
        {
            if (TurnEnergyActions(skill.GetCost(atkState)))
            {
                StartCoroutine(StartAtkAnim());
                atkCmdR.Stop();

                moveActive = false;
                atkActive = true;
                spellActive = false;
                move2Active = false;

                skill.UnShowRange();
                gridA.DisableAtkGrid();

                if(atkState != "atk" && !gameM.playerUseMagic) gameM.playerUseMagic = true;
            }
            else
            {
                timerC.EnemyFar();
            }
        }
        else
        {
            timerC.EnemyFar();
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

        skill.UnShowRange();
        gridA.DisableAtkGrid();

        if (target != null || target == null)
        {
            if (target == null) target = GameObject.Find(n);

            if (TurnEnergyActions(skill.GetCost(atkState)))
            {
                StartCoroutine(StartAtkAnim());

                skill.UnShowRange();
                gridA.DisableAtkGrid();

                if (!gameM.playerUseMagic) gameM.playerUseMagic = true;
            }
            else
            {
                startCmdR.Start();
                spellCmdR.Start();

                atkCmdR.Stop();

                playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = defaultC;
            }
        }
        else
        {
            startCmdR.Start();
            spellCmdR.Start();

            atkCmdR.Stop();

            playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = defaultC;
            print("fuera de alcance");
        }
    }

    private IEnumerator StartAtkAnim()
    {
        setTarget = true;

        yield return new WaitForSeconds(1.1f);

        gameM.CameraSkillPlayer(2);
        skill.SelectSkill(atkState);

        setTarget = false;

        if (!startCmdR.IsRunning)startCmdR.Start();
        spellCmdR.Start();

        playerTr.GetComponent<PlayerStats>().selected.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = defaultC;

        skill.EliminateSkillSelection();
    }

    private IEnumerator SelfBuffs()
    {
        skill.SelectSkill(atkState);
        
        yield return new WaitForSeconds(1.7f);

        if (!startCmdR.IsRunning) startCmdR.Start();
        spellCmdR.Start();

        skill.EliminateSkillSelection();
    }
}
