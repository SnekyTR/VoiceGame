using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProgressionData
{
    public int progressionNumber;
    public int ftueProgressionNumber;
    public string actualPlayer;

    //[SerializeField] FTUE_Progresion fTUE_Progresion;

    // Update is called once per frame

    public GameProgressionData(Progression pro, FTUE_Progresion fTUE_Progresion)
    {
        progressionNumber = pro.progression;
        ftueProgressionNumber = fTUE_Progresion.ftueProgression;
        actualPlayer = fTUE_Progresion.actualPlayer;
    }
    public void SummProgression(int incr)
    {
        progressionNumber += incr;
    }
}
