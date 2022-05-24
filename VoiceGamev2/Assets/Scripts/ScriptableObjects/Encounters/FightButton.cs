using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightButton : MonoBehaviour
{
    [SerializeField] private GeneralStats general;
    public void EnterBattle()
    {
        SceneManager.LoadScene(1);
        SaveSystem.SavePlayer(general);
    }
}
