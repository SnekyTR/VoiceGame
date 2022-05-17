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
    public Transform[] players;
    public StateManager[] enemys;
    public GameObject[] playerSelected;
    private PlayerMove moveLogic;

    private Dictionary<string, Action<string>> selectPJCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer selectPJCmdR;

    void Start()
    {
        whoTurn = true;         //player turn

        moveLogic = GetComponent<PlayerMove>();

        for (int i = 0; i < players.Length; i++)
        {
            selectPJCmd.Add(players[i].name, SelectPJ);
        }

        for (int i = 0; i < enemys.Length; i++)
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
        if(n == players[0].name)
        {
            if (whoTurn)
            {
                moveLogic.PlayerDeselect();
                moveLogic.PlayerSelect();
                moveLogic.NewPlayer(players[0].gameObject);
                print("hola");
            }

            NewParent(players[0].transform);
            playerSelected[0].SetActive(true);
            playerSelected[1].SetActive(false);
            playerSelected[2].SetActive(false);
        }
        else if(players.Length > 1 && n == players[1].name)
        {
            if (whoTurn)
            {
                moveLogic.PlayerDeselect();
                moveLogic.PlayerSelect();
                moveLogic.NewPlayer(players[1].gameObject);
            }

            NewParent(players[1].transform);
            playerSelected[1].SetActive(true);
            playerSelected[0].SetActive(false);
            playerSelected[2].SetActive(false);
        }
        else if(players.Length > 2 &&n == players[2].name)
        {
            if (whoTurn)
            {
                moveLogic.PlayerDeselect();
                moveLogic.PlayerSelect();
                moveLogic.NewPlayer(players[2].gameObject);
            }

            NewParent(players[2].transform);
            playerSelected[2].SetActive(true);
            playerSelected[0].SetActive(false);
            playerSelected[1].SetActive(false);
        }
        else
        {
            moveLogic.PlayerDeselect();
            moveLogic.PlayerSelect();
        }

        for(int i = 0;i < enemys.Length; i++)
        {
            if(n == enemys[i].name)
            {
                NewParent(enemys[i].transform);

                return;
            }
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

            for(int i = 0; i<players.Length; i++)
            {
                moveLogic.PlayerDeselect();
            }

            for (int i = 0; i < players.Length; i++)
            {
                enemys[i].GetComponent<EnemyStats>().FullEnergy();
            }

            enemys[0].StarIA();
        }
        else
        {
            whoTurn = true;

            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerStats>().FullEnergy();
            }
        }
    }

    public void NextIA(StateManager n)
    {
        for(int i = 0; i < enemys.Length; i++)
        {
            if(n == enemys[i] && n != enemys[(enemys.Length)-1])
            {
                enemys[(i + 1)].StarIA();
                return;
            }
            else if(n == enemys[(enemys.Length) - 1])
            {
                NextTurn();
                return;
            }
        }
    }
}
