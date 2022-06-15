using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEditor.AI;


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
    private Text turnTxt;

    private Transform pos1, pos2;
    private int cameraParent = 1;

    private Dictionary<string, Action<string>> selectPJCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer selectPJCmdR;

    private Dictionary<string, Action> passCmd = new Dictionary<string, Action>();
    private KeywordRecognizer passCmdR;

    private void Awake()
    {
        whoTurn = true;         //player turn

        pos1 = GameObject.Find("pos1").transform;
        pos2 = GameObject.Find("pos2").transform;

        moveLogic = GetComponent<PlayerMove>();
        turnTxt = GameObject.Find("Turno").GetComponent<Text>();

        Transform ply = GameObject.Find("Players").transform;
        Transform cnv = GameObject.Find("CanvasManager").transform;

        for (int i = 0; i < ply.childCount; i++)
        {
            players.Add(ply.GetChild(i));
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
        passCmdR = new KeywordRecognizer(passCmd.Keys.ToArray());
        passCmdR.OnPhraseRecognized += RecognizedVoice4;

        selectPJCmdR = new KeywordRecognizer(selectPJCmd.Keys.ToArray());
        selectPJCmdR.OnPhraseRecognized += RecognizedVoice;

        selectPJCmdR.Start();
        passCmdR.Start();
    }
    
    void LateUpdate()
    {
        if(playerParent != null)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position,new Vector3(playerParent.position.x, transform.position.y, playerParent.position.z), 5 * Time.deltaTime);
            transform.position = newPos;

            if(cameraParent == 1)
            {
                Vector3 newPos1 = Vector3.MoveTowards(transform.GetChild(0).position, pos1.position, 9 * Time.deltaTime);
                transform.GetChild(0).position = newPos1;

                transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, pos1.rotation, 2 * Time.deltaTime);
            }
            else if(cameraParent == 2)
            {
                Vector3 newPos2 = Vector3.MoveTowards(transform.GetChild(0).position, pos2.position, 9 * Time.deltaTime);
                transform.GetChild(0).position = newPos2;

                transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, pos2.rotation, 2 * Time.deltaTime);
            }
        }
    }

    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        selectPJCmd[speech.text].Invoke(speech.text);
    }

    public void RecognizedVoice4(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        passCmd[speech.text].Invoke();
    }

    private void SelectPJ(string n)
    {
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
                break;
            }
            else if((i+1) == players.Count)
            {
                return;
            }
        }

        if (whoTurn)
        {
            moveLogic.NewPlayer(actualPlayer.gameObject);
            moveLogic.ReasignateGrid();
        }

        NewParent(actualPlayer.transform, 1);

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
    }

    public void CancelOrder()
    {
        moveLogic.PlayerDeselect();
        moveLogic.PlayerSelect();

        NewParent(playerParent, 1);

        //NavMeshBuilder.ClearAllNavMeshes();
        //NavMeshBuilder.BuildNavMesh();

        
    }

    public void NextTurn()
    {
        if (whoTurn)
        {
            whoTurn = false;
            passCmdR.Stop();
            selectPJCmdR.Stop();

            Transform nw = GameObject.Find("Center").transform;
            NewParent(nw, 2);

            for(int i = 0; i<players.Count; i++)
            {
                moveLogic.PlayerDeselect();
            }

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].GetComponent<EnemyStats>().FullEnergy();
            }

            enemys[0].StarIA();
            turnTxt.text = "Enemy";
            turnTxt.color = Color.red;
            turnTxt.transform.GetChild(0).GetComponent<Text>().color = Color.red;
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

            playerSelected[0].SetActive(false);
            if (players.Count > 1) playerSelected[1].SetActive(false);
            if (players.Count > 2) playerSelected[2].SetActive(false);

            playerStructure[0].SetActive(true);
            if (players.Count > 1) playerStructure[1].SetActive(true);
            if (players.Count > 2) playerStructure[2].SetActive(true);

            turnTxt.text = "Player";
            turnTxt.color = Color.blue;
            turnTxt.transform.GetChild(0).GetComponent<Text>().color = Color.blue;
        }
    }

    public void NextIA(StateManager n)
    {
        for(int i = 0; i < enemys.Count; i++)
        {
            if(n == enemys[i] && n != enemys[(enemys.Count)-1])
            {
                enemys[(i + 1)].StarIA();
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
