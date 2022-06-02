using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCombatChanger : MonoBehaviour
{
    [SerializeField] public ScriptableObject[] singleCombats;
    [SerializeField] public ScriptableObject[] doubleCombats;

    public List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();

    private LoadSingle loadSingle;
    private LoadDouble loadDouble;
    private void Awake()
    {
        //LoadProgression();
        
        /*for (int i = 0; i < singleCombats.Length; i++)
        {
            scriptableObjects.Add(singleCombats[i]);
        }*/
        
        
    }
    public void SelectLvl(bool combatSingle)
    {
        if (combatSingle)
        {
            loadSingle = gameObject.GetComponent<LoadSingle>();
            loadSingle.DisplayLevelInf((SingleEncounterStructure)scriptableObjects[0]);
        }
        else
        {
            loadDouble = gameObject.GetComponent<LoadDouble>();
            loadDouble.DisplayLevelInf((DoubleEncounterStructure)scriptableObjects[0]);
        }
        
    }
    /*public void LoadProgression()
    {
        GameProgressionData data = SaveSystem.LoadProgression();

        singleCombats = data.singleCombats;
        doubleCombats = data.doubleCombats;
    }*/
}
