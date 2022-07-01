using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartyInformation : MonoBehaviour
{
    [SerializeField] private LevelSystem levelSystem;

    [SerializeField] public Transform[] players;
    [SerializeField] private TextMeshProUGUI[] playersName;
       //string actualLevel;

    //[Header("Player 1")]
    //[SerializeField] private TextMeshProUGUI playerName1;
    /*[SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI level;
    

    [Header("Player 2")]
    [SerializeField] private GameObject playerName2;
    [SerializeField] private Transform player2;
    [SerializeField] private GameObject Level2;

    [Header("Player 3")]
    [SerializeField] private GameObject playerName3;
    [SerializeField] private Transform player3;
    [SerializeField] private GameObject Level3;*/

    // Start is called before the first frame update
    void Start()
    {
        AssignPlayer1();
    }
    private void AssignPlayer1()
    {
        for(int i = 0; i < playersName.Length; i++){
            playersName[i].text = players[i].name;
            //levelSystem.UpdateLevel();
            levelSystem = players[i].gameObject.GetComponent<LevelSystem>();
            levelSystem.UpdateLevel();
        }
    }
    public void UpdateLevel(string actualLvl, string actualPlayer)
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(actualPlayer == players[i].name)
            {
                if(players[i].name == playersName[i].text){
                   playersName[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + actualLvl;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
