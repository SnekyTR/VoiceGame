using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class CameraFollow : MonoBehaviour
{
    private Transform playerParent;
    public bool whoTurn;
    [SerializeField] private PlayerMove[] players;
    [SerializeField] private StateManager[] enemys;

    private Dictionary<string, Action<string>> selectPJCmd = new Dictionary<string, Action<string>>();
    private KeywordRecognizer selectPJCmdR;

    void Start()
    {
        whoTurn = true;         //player turn

        selectPJCmd.Add(players[0].name, SelectPJ);

        for (int i = 0; i < enemys.Length; i++)
        {
            selectPJCmd.Add(enemys[i].name, SelectPJ);
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
            if(whoTurn) players[0].PlayerSelect();

            NewParent(players[0].transform);
        }
        else
        {
            for(int i = 0; i < players.Length; i++)
            {
                if(whoTurn) players[i].PlayerDeselect();
            }
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
                players[i].PlayerDeselect();
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
            }
            else if(n == enemys[(enemys.Length) - 1])
            {
                NextTurn();
            }
        }
    }
}
