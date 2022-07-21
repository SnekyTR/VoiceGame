using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject singlePanel;
    [SerializeField] private GameObject vagnar;
    GameSave gameSave;
    private void Awake()
    {
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
        GameProgressionData data = SaveSystem.LoadProgression();
        progression = data.progressionNumber;
        CheckProgression();
    }
    public void CheckProgression()
    {
        if (progression >= 1)
        {
            //Destroy(combat1);
            //combat2.SetActive(false);
            if(fTUE_Progresion.ftueProgression == 0)
            {
                fTUE_Progresion.LoadFTUEProgresion();
                fTUE_Progresion.FTUEProgression();
            }
            print("Se ha pasado el primer nivel");
            if(progression >= 2)
            {
                print("Se ha pasado el segundo nivel");
                combat2.SetActive(false);
                player.GetComponent<VoiceDestinations>().entered = false;
                singlePanel.SetActive(false);


                if (progression >= 3)
                {
                    combat3.SetActive(false);
                    restAnimator.SetFloat("anim", 0);
                    bastionAnimator.SetFloat("anim", 1);
                    //vagnar.SetActive(true);
                    if (progression >= 4)
                    {
                        bastionAnimator.SetFloat("anim", 0);
                        forestAnimator.SetFloat("anim", 1);
                        //p2Interface.SetActive(true);
                        //p2.SetActive(true);
                        vagnar.GetComponent<GeneralStats>().PlayerActivation();
                        combat4.SetActive(false);
                        if (progression >= 5)
                        {
                            forestAnimator.SetFloat("anim", 0);
                            combat5.SetActive(false);
                            if (progression >= 6)
                            {
                                combat6.SetActive(false);
                                if (progression >= 7)
                                {
                                    p3Interface.SetActive(true);
                                    p3.SetActive(true);
                                    combat7.SetActive(false);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void Victory()
    {
        victoryResults.SetActive(true);
    }
}
