using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCombatChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] singleCombats;
    [SerializeField] private ScriptableObject[] doubleCombats;

    private LoadSingle loadSingle;
    private LoadDouble loadDouble;
    private void Awake()
    {
        
    }
    public void SelectLvl(bool combatSingle)
    {
        if (combatSingle)
        {
            loadSingle = gameObject.GetComponent<LoadSingle>();
            loadSingle.DisplayLevelInf((SingleEncounterStructure)singleCombats[0]);
        }
        else
        {
            loadDouble = gameObject.GetComponent<LoadDouble>();
            loadDouble.DisplayLevelInf((DoubleEncounterStructure)doubleCombats[0]);
        }
        
    }

}
