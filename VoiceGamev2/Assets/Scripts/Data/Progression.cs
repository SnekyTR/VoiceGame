using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    public GameObject combat1;
    public GameObject combat2;
    public GameObject combat3;
    public GameObject combat4;
    public GameObject combat5;
    public GameObject combat6;
    public GameObject combat7;
    public GameObject combat8;
    public int progression;

    [SerializeField] public Animator restAnimator;
    [SerializeField] private Animator bastionAnimator;
    [SerializeField] private Animator forestAnimator;
    [SerializeField] private GameObject p2Interface;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p3Interface;
    [SerializeField] private GameObject p3;

    [SerializeField]private GameObject victoryResults;
    [SerializeField] private FTUE_Progresion fTUE_Progresion;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIMovement uIMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject weapReward;
    [SerializeField] private GameObject singlePanel;
    [SerializeField] private GameObject vagnar;
    [SerializeField] private TextMeshProUGUI expText1;
    [SerializeField] private TextMeshProUGUI expText2;
    [SerializeField] private TextMeshProUGUI expText3;
    bool isTheSame = false;
    bool newWeap;
    GameSave gameSave;
    [SerializeField]VoiceDestinations voices;
    MoveDataToMain moveDataToMain;

    LevelSystem level;
    public int totalEXP;
    public bool fromVictory;


    GameObject[] players;
    private void Awake()
    {
        moveDataToMain = GameObject.Find("SceneConector").GetComponent<MoveDataToMain>();
        if (moveDataToMain.loadVictory)
        {
            fromVictory = true;
            StartCoroutine(IncrementTheProgression());
        }
        
        
        if (System.IO.File.Exists(Application.persistentDataPath + "/progression.data"))
        {
            print("Se ha cargado progresion");
            LoadProgresion();
        }
    }
    private void Start()
    {
        
        gameSave = gameObject.GetComponent<GameSave>();
    }
    private void LoadProgresion()
    {
        if (!fromVictory)
        {
            uIMovement.canOpenGroup = true;
        }
        GameProgressionData data = SaveSystem.LoadProgression();
        progression = data.progressionNumber;

        CheckProgression();
    }
    public void CheckProgression()
    {
        if (progression >= 1)
        {
            fTUE_Progresion.LoadFTUEProgresion();
            print("Ha entrado en check Progresion");
            //Destroy(combat1);
            //combat2.SetActive(false);
            if(fTUE_Progresion.ftueProgression == 0)
            {
                
                fTUE_Progresion.FTUEProgression();
            }else if(fTUE_Progresion.ftueProgression > 0)
            {
                print("Es mayor a 0");
                if (fTUE_Progresion.ftueProgression < 7)
                {
                    fTUE_Progresion.ReloadFTUE(); print("Es menor a 7");
                    return;
                }
                else
                {
                    voices.mapDestinations.Start();
                }
            }
            if(progression >= 1)
            {
                
                for(int i = 0; i < inventory.actualWeapons.Count; i++)
                {
                    if (inventory.totalWeapons[1] == inventory.actualWeapons[i])
                    {
                        isTheSame = true;
                    }
                    
                }
                if (!isTheSame)
                {
                    inventory.actualWeapons.Add(inventory.totalWeapons[1]);
                    Scripteable_Weapon weap = (Scripteable_Weapon)inventory.totalWeapons[1];
                    weapReward.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = weap.name;
                    weapReward.transform.GetChild(2).GetComponent<Image>().sprite = weap.artwork;
                    newWeap = true;
                }
                
                
                player.GetComponent<VoiceDestinations>().entered = false;
                singlePanel.SetActive(false);
                restAnimator.SetFloat("anim", 1);
                print("Rested Anim");
                if (progression >= 2)
                {
                    isTheSame = false;
                    
                    print("Se activa vastion anum");
                    combat2.SetActive(false);
                    restAnimator.SetFloat("anim", 0);
                    bastionAnimator.SetFloat("anim", 1);
                    //vagnar.SetActive(true);
                    if (progression >= 3)
                    {
                        for (int i = 0; i < inventory.actualWeapons.Count; i++)
                        {
                            if (inventory.totalWeapons[2] == inventory.actualWeapons[i])
                            {
                                isTheSame = true;
                            }

                        }
                        if (!isTheSame)
                        {
                            Scripteable_Weapon weap = (Scripteable_Weapon)inventory.totalWeapons[2];
                            weapReward.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = weap.name;
                            weapReward.transform.GetChild(2).GetComponent<Image>().sprite = weap.artwork;
                            newWeap = true;
                        }
                        bastionAnimator.SetFloat("anim", 0);
                        forestAnimator.SetFloat("anim", 1);
                        //p2Interface.SetActive(true);
                        //p2.SetActive(true);
                        vagnar.GetComponent<GeneralStats>().PlayerActivation();
                        combat3.SetActive(false);
                        if (progression >= 4)
                        {
                            forestAnimator.SetFloat("anim", 0);
                            combat4.SetActive(false);
                            if (progression >= 5)
                            {
                                combat5.SetActive(false);
                                if (progression >= 6)
                                {
                                    p3Interface.SetActive(true);
                                    p3.SetActive(true);
                                    combat6.SetActive(false);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void Victory(float xp)
    {
        uIMovement.canOpenGroup = false;
        victoryResults.SetActive(true);
        if (newWeap)
        {
            weapReward.SetActive(true);
        }
        expText1.text = "+" + xp + " XP";
        expText2.text = "+" + xp + " XP";
        expText3.text = "+" + xp + " XP";
        if(fTUE_Progresion.ftueProgression == 0)
        {
            fTUE_Progresion.ftueProgression = 1;
            gameSave.SaveGame();
        }
    }
    IEnumerator IncrementTheProgression()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == "Magnus")
            {
                voices = players[i].GetComponent<VoiceDestinations>();
                voices.enabled = false;
            }
        }
        yield return new WaitForSeconds(0.1f);
        progression += 1;
        gameSave.SaveGame();
        //gameSave.LoadGame();
        CheckProgression();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            level = GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<LevelSystem>();
            level.GainExperience(moveDataToMain.totalEXP);
            level.hasLvlUP = true;
            //gameSave.LoadGame(GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<GeneralStats>(), GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<LevelSystem>()) ;
        }
        gameSave.SaveGame();
        voices.enabled = true;
        Victory(moveDataToMain.totalEXP);
        Progression pro = gameObject.GetComponent<Progression>();
    }
}
