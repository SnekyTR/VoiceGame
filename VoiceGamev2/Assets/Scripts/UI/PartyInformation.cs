using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartyInformation : MonoBehaviour
{
    [SerializeField] private LevelSystem levelSystem;
    string actualLevel;

    [Header("Player 1")]
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI level;
    

    [Header("Player 2")]
    [SerializeField] private GameObject playerName2;
    [SerializeField] private Transform player2;
    [SerializeField] private GameObject Level2;

    [Header("Player 3")]
    [SerializeField] private GameObject playerName3;
    [SerializeField] private Transform player3;
    [SerializeField] private GameObject Level3;

    // Start is called before the first frame update
    void Start()
    {
        AssignPlayer1();
    }
    private void AssignPlayer1()
    {
        /*TextMeshPro textplayer = playerName.GetComponent<TextMeshPro>();
        textplayer.text = player.name;
        
        TextMeshPro textLevel = playerName.GetComponent<TextMeshPro>();
        textLevel.text = actualLevel.ToString();*/
        actualLevel = levelSystem.UpdateLevel();
        playerName.text = player.name;
        level.text = "Level " + actualLevel.ToString();

    }
    public void UpdateLevel()
    {
        
        level.text = "Level " + actualLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
