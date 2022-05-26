using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour
{
    GameSave gameSave;
    

    private void Start()
    {
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
    }
    public void EnterBattle()
    {
        gameSave.SaveGame();
    }
}
