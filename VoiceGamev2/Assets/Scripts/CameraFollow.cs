using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class CameraFollow : MonoBehaviour
{
    public Transform playerParent = null;
    public bool whoTurn;
    public List<Transform> players;
    public List<StateManager> enemys;
    public GameObject[] playerSelected;
    public GameObject[] playerStructure;
    private List<string> playersNames = new List<string>();
    private PlayerMove moveLogic;
    [SerializeField] private Image turnImg;

    private Dictionary<string, Action<string>> selectPJCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer selectPJCmdR;

    void Start()
    {
        whoTurn = true;         //player turn
        turnImg.color = Color.blue;

        moveLogic = GetComponent<PlayerMove>();

        for (int i = 0; i < players.Count; i++)
        {
            selectPJCmd.Add(players[i].name, SelectPJ);
            playersNames.Add(players[i].name);
        }

        for (int i = 0; i < enemys.Count; i++)
        {
            selectPJCmd.Add("mira a " + enemys[i].name, SelectPJ);
        }

        selectPJCmdR = new KeywordRecognizer(selectPJCmd.Keys.ToArray());
        selectPJCmdR.OnPhraseRecognized += RecognizedVoice;

        selectPJCmdR.Start();
    }
    
    void LateUpdate()
    {
        if(playerParent != null)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position,new Vector3(playerParent.position.x, transform.position.y, playerParent.position.z), 5*Time.deltaTime);
            transform.position = newPos;
        }
    }

    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        selectPJCmd[speech.text].Invoke(speech.text);
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
                    NewParent(enemys[i].transform);
                    print(names[2]);
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
            moveLogic.PlayerDeselect();
            moveLogic.PlayerSelect();
            moveLogic.NewPlayer(actualPlayer.gameObject);
        }

        NewParent(actualPlayer.transform);

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

    public void NewParent(Transform tr)
    {
        playerParent = tr;
    }

    public void NextTurn()
    {
        if (whoTurn)
        {
            whoTurn = false;

            for(int i = 0; i<players.Count; i++)
            {
                moveLogic.PlayerDeselect();
            }

            for (int i = 0; i < enemys.Count; i++)
            {
                enemys[i].GetComponent<EnemyStats>().FullEnergy();
            }

            enemys[0].StarIA();
            turnImg.color = Color.red;
        }
        else
        {
            whoTurn = true;

            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerStats>().FullEnergy();
            }

            turnImg.color = Color.blue;
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
