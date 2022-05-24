using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCombatChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] serializableObjects;
     private LoadDouble loadDouble;
    private void Awake()
    {
        loadDouble = gameObject.GetComponent<LoadDouble>();  
    }
    public void SelectLvL() {
        loadDouble.DisplayLevelInf((DoubleEncounterStructure)serializableObjects[0]);
    }

}
