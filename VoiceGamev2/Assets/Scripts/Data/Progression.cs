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

    [SerializeField]private GameObject victoryResults;
    [SerializeField] private FTUE_Progresion fTUE_Progresion;
    GameSave gameSave;
    private void Start()
    {
        //combat1 = GameObject.Find("First Combat");
        combat2 = GameObject.Find("Second Combat");
        combat3 = GameObject.Find("Third Combat");
        combat4 = GameObject.Find("Fourth Combat");
        combat5 = GameObject.Find("Fifth Combat");
        combat6 = GameObject.Find("Combat6");
        combat7 = GameObject.Find("Combat7");
        combat8 = GameObject.Find("Combat8");
        gameSave = gameObject.GetComponent<GameSave>();
        CheckProgression();
    }
    /*private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }*/
    public void CheckProgression()
    {
        if (progression >= 1)
        {
            //Destroy(combat1);
            //combat2.SetActive(false);
            fTUE_Progresion.FTUEProgression();
            print("Se ha pasado el primer nivel");
            if(progression >= 2)
            {
                combat2.SetActive(false);
                if (progression >= 3)
                {
                    combat3.SetActive(false);
                    if (progression >= 4)
                    {
                        combat4.SetActive(false);
                        if (progression >= 5)
                        {
                            combat5.SetActive(false);
                            if (progression >= 6)
                            {
                                combat6.SetActive(false);
                                if (progression >= 7)
                                {
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
