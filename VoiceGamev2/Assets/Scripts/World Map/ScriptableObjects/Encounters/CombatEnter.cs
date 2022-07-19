using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class CombatEnter : MonoBehaviour
{
    public bool combat;
    [SerializeField] public bool combatSingle;
    VoiceDestinations voices;
    [SerializeField] private SingleCombatChanger single;
    [SerializeField] private MusicChanger musicChanger;
    [SerializeField] private GameObject panelSingle;
    [SerializeField] private GameObject panelDouble;
    [SerializeField] private Transform player;

    [SerializeField] private ScriptableObject actualCombat;

    private GeneralStats general;
    private LevelSystem level;

    public int sceneIndex;

    private BattleWindow battleWindow;

    private void Start()
    {
        general = player.gameObject.GetComponent<GeneralStats>();
        level = player.gameObject.GetComponent<LevelSystem>();
        battleWindow = GameObject.Find("VoiceSystems").GetComponent<BattleWindow>();
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            musicChanger.ChangeCombatMusic();
            single.scriptableObjects.Add(actualCombat);
            battleWindow.GetCombatEnter(this.gameObject);
            voices = other.gameObject.GetComponent<VoiceDestinations>();
            voices.entered = true;
            battleWindow.combatPanel.Start();
            print("Se ha activado combat panel");
            voices.combatEnter = GetComponent<CombatEnter>();
            if (combatSingle)
            {
                battleWindow.combatPanel.Start();
                combatSingle = true;
                single.SelectLvl(combatSingle);
                panelSingle.SetActive(true);
            }
            else
            {
                battleWindow.combatPanel.Stop();
                combatSingle = false;
                single.SelectLvl(combatSingle);
                panelDouble.SetActive(true);
            }
            
            voices.StopPlayer();
            combat = true;
            print("Ha entrado"); 
        }
    }
   /* private void GetIndex(SingleEncounterStructure singleEsctructure)
    {
        sceneIndex = singleEsctructure.sceneIndex;
    }*/
    public void CheckLvlPanel()
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

}
