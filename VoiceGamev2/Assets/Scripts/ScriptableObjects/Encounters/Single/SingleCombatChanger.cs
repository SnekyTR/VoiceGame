using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCombatChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] serializableObjects;

    private LoadSingle loadSingle;
    private void Awake()
    {
        loadSingle = gameObject.GetComponent<LoadSingle>();
    }
    public void SelectLvl()
    {
        loadSingle.DisplayLevelInf((SingleEncounterStructure)serializableObjects[0]);
    }

}
