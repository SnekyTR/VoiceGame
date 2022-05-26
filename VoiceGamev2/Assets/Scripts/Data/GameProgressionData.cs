using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProgressionData
{
    public int progressionNumber;
   
    // Update is called once per frame
    
    public GameProgressionData(int incr)
    {
        progressionNumber += incr; 
    }
}
