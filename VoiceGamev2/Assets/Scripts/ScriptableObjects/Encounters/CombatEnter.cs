using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatEnter : MonoBehaviour
{
    public bool combat;
    [SerializeField] public bool combatSingle;
    VoiceDestinations voices;
    [SerializeField] private SingleCombatChanger single;
    [SerializeField] private GameObject panelSingle;
    [SerializeField] private GameObject panelDouble;
    [SerializeField] private Transform player;
    private GeneralStats general;
    private LevelSystem level;

    private void Start()
    {
        general = player.gameObject.GetComponent<GeneralStats>();
        level = player.gameObject.GetComponent<LevelSystem>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
           
            voices = other.gameObject.GetComponent<VoiceDestinations>();
            voices.combatEnter = GetComponent<CombatEnter>();
            if (combatSingle)
            {
                combatSingle = true;
                single.SelectLvl(combatSingle);
                panelSingle.SetActive(true);
            }
            else
            {
                combatSingle = false;
                single.SelectLvl(combatSingle);
                panelDouble.SetActive(true);
            }
            
            voices.StopPlayer();
            combat = true;
            print("Ha entrado"); 
        }
    }
    public void www()
    {
        if (combat == true) { 

            if (combatSingle)
            {
                panelSingle.SetActive(true);
            }
            else
            {
                panelDouble.SetActive(true);
            }
            voices.StopPlayer();
            print(combat);

        }
        else
        {

        }
    }

    public void LoadLvlPop()
    {
        if (combatSingle)
        {
            panelSingle.SetActive(true);
        }
        else
        {
            panelDouble.SetActive(true);
        }
    }
    public void EnterBattle()
    {
        SceneManager.LoadScene(1);
        SaveSystem.SavePlayer(general, level);
    }
}
