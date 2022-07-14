using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform playerParent = null;
    public bool whoTurn;
    public List<Transform> players = new List<Transform>();
    public List<StateManager> enemys = new List<StateManager>();
    public List<GameObject> playerSelected = new List<GameObject>();
    public List<GameObject> playerStructure = new List<GameObject>();
    private List<string> playersNames = new List<string>();
    private PlayerMove moveLogic;
    private SkillBook skBook;
    private GameObject playerTurn, enemyTurn;
    private Transform cam;

    public Transform pos1, pos2;
    private Transform pos3, pos4;
    private int cameraParent = 1;
    private Vector3 initPos;

    private Dictionary<string, Action<string>> selectPJCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer selectPJCmdR;

    private Dictionary<string, Action> passCmd = new Dictionary<string, Action>();
    private KeywordRecognizer passCmdR;

    [HideInInspector] public bool selectPjRestriction, nextTurnRestriction, cancelRestriction, sbookRestriction;
    [HideInInspector] public bool selectPjActive, nextTurnActive, cancelActive, sbookActive;

    private void Awake()
    {
        whoTurn = true;         //player turn
        initPos = transform.position;

        cam = transform.GetChild(0);
        pos3 = GameObject.Find("Center").transform;

        moveLogic = GetComponent<PlayerMove>();
        playerTurn = GameObject.Find("PlayerTurn");
        enemyTurn = GameObject.Find("EnemyTurn");
        enemyTurn.SetActive(false);

        Transform ply = GameObject.Find("Players").transform;
        Transform cnv = GameObject.Find("CanvasManager").transform;
        skBook = cnv.GetComponent<SkillBook>();

        for (int i = 0; i < ply.childCount; i++)
        {
            players.Add(ply.GetChild(i));
            skBook.playerS.Add(ply.GetChild(i).GetComponent<PlayerStats>());
            playerSelected.Add(cnv.GetChild(i+3).gameObject);
            playerStructure.Add(cnv.GetChild(i).gameObject);

            playerSelected[i].SetActive(false);
        }

        Transform eny = GameObject.Find("Enemys").transform;

        for (int i = 0; i < eny.childCount; i++)
        {
            enemys.Add(eny.GetChild(i).GetComponent<StateManager>());
        }
    }

    void Start()
    {
        for (int i = 0; i < players.Count; i++)                         //speech
        {
            selectPJCmd.Add(players[i].name, SelectPJ);
            playersNames.Add(players[i].name);
        }

        for (int i = 0; i < enemys.Count; i++)
        {
            selectPJCmd.Add("mira a " + enemys[i].name, SelectPJ);
        }

        passCmd.Add("pasar", NextTurn);
        passCmd.Add("cancelar", CancelOrder);
        passCmd.Add("pergamino", SkillBookOpen);
        passCmdR = new KeywordRecognizer(passCmd.Keys.ToArray());
        passCmdR.OnPhraseRecognized += RecognizedVoice4;

        selectPJCmdR = new KeywordRecognizer(selectPJCmd.Keys.ToArray());
        selectPJCmdR.OnPhraseRecognized += RecognizedVoice;

        selectPJCmdR.Start();
        passCmdR.Start();

        CameraCenter();
    }
    
    void LateUpdate()
    {
        if(playerParent != null)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position,new Vector3(playerParent.position.x, transform.position.y, playerParent.position.z), 6 * Time.deltaTime);
            transform.position = newPos;

            if(cameraParent == 1)
            {
                Vector3 newPos1 = Vector3.MoveTowards(transform.GetChild(0).position, pos1.position, 9 * Time.deltaTime);
                cam.position = newPos1;

                cam.rotation = Quaternion.Slerp(transform.GetChild(0).rotation, pos1.rotation, 1.3f * Time.deltaTime);
            }
            else if(cameraParent == 2)
            {
                Vector3 newPos2 = Vector3.MoveTowards(transform.GetChild(0).position, pos2.position, 9 * Time.deltaTime);
                cam.position = newPos2;

                cam.rotation = Quaternion.Slerp(transform.GetChild(0).rotation, pos2.rotation, 1.5f * Time.deltaTime);
            }
        }
    }

    public void CloseCameraFollow()
    {
        if (!PlayerPrefs.HasKey("cf")) PlayerPrefs.SetInt("cf", 0);

        int ns = PlayerPrefs.GetInt("cf");
        PlayerPrefs.SetInt("cf", (ns + 1));

        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasdadfsasdf" + SceneManager.GetActiveScene().name + ns, CancelOrder);
        Dictionary<string, Action> zero2 = new Dictionary<string, Action>();
        zero2.Add("ghjghasdfasdf4qw" + SceneManager.GetActiveScene().name + ns, CancelOrder);
        
        passCmdR = new KeywordRecognizer(zero1.Keys.ToArray());
        selectPJCmdR = new KeywordRecognizer(zero2.Keys.ToArray());
        passCmdR.OnPhraseRecognized -= RecognizedVoice4;
        selectPJCmdR.OnPhraseRecognized -= RecognizedVoice;
    }

    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        selectPJCmd[speech.text].Invoke(speech.text);
        string selectedChar = speech.text;

    }

    public void RecognizedVoice4(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        passCmd[speech.text].Invoke();
    }

    private void SelectPJ(string n)
    {
        if (selectPjRestriction) return;
        selectPjActive = true;
        nextTurnActive = false;
        cancelActive = false;
        sbookActive = false;

        string[] names = n.Split(' ');
        if(names.Length > 2)
        {
            for (int i = 0; i < enemys.Count; i++)
            {
                if (names[2] + " " + names[3] == enemys[i].name)
                {
                    NewParent(enemys[i].transform, 1);
                    print(names[2]);
                    moveLogic.PlayerDeselect();

                    playerSelected[0].SetActive(false);
                    if (players.Count > 1) playerSelected[1].SetActive(false);
                    if (players.Count > 2) playerSelected[2].SetActive(false);

                    playerStructure[0].SetActive(true);
                    if (players.Count > 1) playerStructure[1].SetActive(true);
                    if (players.Count > 2) playerStructure[2].SetActive(true);

                    return;
                }
            }
        }

        Transform actualPlayer = null;
        for(int i = 0; i < players.Count; i++)
        {
            if(n == players[i].name)
            {
                actualPlayer = players[i];
                GetComponent<Skills>().SetActualPlayer(actualPlayer.GetComponent<PlayerStats>());
                break;
            }
            else if((i+1) == players.Count)
            {
                return;
            }
        }

        if (moveLogic.allieSpell)
        {
            moveLogic.Allie(n);
            moveLogic.allieSpell = false;
            return;
        }

        if (skBook.isStarted)
        {
            int e = 0;

            if (playerParent != null)
            {
                if (actualPlayer == players[0]) e = 1;
                else if (actualPlayer == players[1]) e = 2;
                else if (actualPlayer == players[1]) e = 2;
            }

            if (e != 0)
            {
                skBook.ChangeSkillBookPage(e);
                return;
            }
        }

        if (whoTurn)
        {
            moveLogic.NewPlayer(actualPlayer.gameObject);
            moveLogic.ReasignateGrid();
        }

        if(!moveLogic.IsMoving())NewParent(actualPlayer.transform, 1);
        else NewParent(actualPlayer.transform, 2);

        if(n == playersNames[0])
        {
            playerSelected[0].SetActive(true);
            if (players.Count > 1) playerSelected[1].SetActive(false);
            if (players.Count > 2) playerSelected[2].SetActive(false);

            playerStructure[0].SetActive(false);
            if (players.Count > 1) playerStructure[1].SetActive(true);
            if (players.Count > 2) playerStructure[2].SetActive(true);
        }
        else if (playersNames.Count > 1 && n == playersNames[1])
        {
            playerSelected[1].SetActive(true);
            playerSelected[0].SetActive(false);
            if (players.Count > 2) playerSelected[2].SetActive(false);

            playerStructure[1].SetActive(false);
            playerStructure[0].SetActive(true);
            if (players.Count > 2) playerStructure[2].SetActive(true);
        }
        else if (playersNames.Count > 2 && n == playersNames[2])
        {
            playerSelected[2].SetActive(true);
            playerSelected[0].SetActive(false);
            playerSelected[1].SetActive(false);

            playerStructure[2].SetActive(false);
            playerStructure[0].SetActive(true);
            playerStructure[1].SetActive(true);
        }
        else
        {
            moveLogic.PlayerDeselect();
            moveLogic.PlayerSelect();
        }
    }

    public void NewParent(Transform tr, int o)
    {
        playerParent = tr;

        cameraParent = o;

        if (tr.GetComponent<PlayerStats>()) pos4 = tr.GetComponent<PlayerStats>().cinemaCam;
        else if (tr.GetComponent<EnemyStats>()) pos4 = tr.GetComponent<EnemyStats>().cinemaCam;
    }

    public void CameraCenter()
    {
        transform.position = initPos;
        cam.position = pos3.position;
        cam.rotation = pos3.rotation;

        playerParent = null;
    }

    public void CameraCinematic()
    {
        cam.position = pos4.position;
        cam.rotation = pos4.rotation;
        cameraParent = 0;
    }

    public void CameraPos1()
    {
        cam.position = pos1.position;
        cam.rotation = pos1.rotation;
        transform.position = new Vector3(playerParent.position.x, transform.position.y, playerParent.position.z);
    }

    public void CameraPos2()
    {
        cam.position = pos2.position;
        cam.rotation = pos2.rotation;
        transform.position = new Vector3(playerParent.position.x, transform.position.y, playerParent.position.z);
    }

    private void SkillBookOpen()
    {
        if (sbookRestriction) return;
        selectPjActive = false;
        nextTurnActive = false;
        cancelActive = false;
        sbookActive = true;

        int e = 0;

        if(playerParent != null)
        {
            if (playerParent == players[0]) e = 1;
            else if (playerParent == players[1]) e = 2;
            else if (playerParent == players[1]) e = 2;
        }

        if(e != 0)
        {
            skBook.StartSkillBook(e, players.Count);
        }
    }

    public void CancelOrder()
    {
        if (cancelRestriction) return;
        selectPjActive = false;
        nextTurnActive = false;
        cancelActive = true;
        sbookActive = false;

        moveLogic.PlayerDeselect();
        moveLogic.PlayerSelect();
        GetComponent<Skills>().UnShowRange();

        NewParent(playerParent, 1);

        //NavMeshBuilder.ClearAllNavMeshes();
        //NavMeshBuilder.BuildNavMesh();

        GetComponent<Skills>().EliminateSkillSelection();
    }

    public void NextTurn()
    {
        if (nextTurnRestriction) return;
        selectPjActive = false;
        nextTurnActive = true;
        cancelActive = false;
        sbookActive = false;

        if (whoTurn)
        {
            whoTurn = false;
            passCmdR.Stop();
            selectPJCmdR.Stop();

            playerSelected[0].SetActive(false);
            if (players.Count > 1) playerSelected[1].SetActive(false);
            if (players.Count > 2) playerSelected[2].SetActive(false);

            playerStructure[0].SetActive(true);
            if (players.Count > 1) playerStructure[1].SetActive(true);
            if (players.Count > 2) playerStructure[2].SetActive(true);

            for (int i = 0; i<players.Count; i++)
            {
                moveLogic.PlayerDeselect();
            }

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].GetComponent<EnemyStats>().FullEnergy();
            }

            enemys[0].StarIA();
            NewParent(enemys[0].transform, 2);
            CameraPos2();

            playerTurn.SetActive(false);
            enemyTurn.SetActive(true);
        }
        else
        {
            whoTurn = true;
            passCmdR.Start();
            selectPJCmdR.Start();

            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerStats>().FullEnergy();
            }

            CameraCenter();

            playerTurn.SetActive(true);
            enemyTurn.SetActive(false);

            GetComponent<Skills>().SetSkillsTimers();
        }
    }

    public void NextIA(StateManager n)
    {
        for(int i = 0; i < enemys.Count; i++)
        {
            if(n == enemys[i] && n != enemys[(enemys.Count)-1])
            {
                enemys[(i + 1)].StarIA();
                NewParent(enemys[(i + 1)].transform, 2);
                CameraPos2();
                return;
            }
            else if(n == enemys[(enemys.Count) - 1])
            {
                NextTurn();
                return;
            }
        }
    }

    public void EliminateElement(GameObject el)
    {
        if (el.GetComponent<PlayerStats>())
        {
            players.Remove(el.transform);
        }
        else if (el.GetComponent<EnemyStats>())
        {
            enemys.Remove(el.GetComponent<StateManager>());
        }
    }
}
