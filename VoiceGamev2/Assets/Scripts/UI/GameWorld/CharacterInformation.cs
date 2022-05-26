using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    [SerializeField] private GeneralStats general;

    public TextMeshProUGUI STR;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI AGI;
    public TextMeshProUGUI INT;
    public TextMeshProUGUI CRIT;
    private void Start()
    {
        STR.text = general.strengthPoints.ToString();
        HP.text = general.lifePoints.ToString();
        AGI.text = general.agilityPoints.ToString();
        INT.text = general.intellectPoints.ToString();
        CRIT.text = general.critStrikePoints.ToString();
    }
    public void UpdateSRT()
    {
        STR.text = general.strengthPoints.ToString();
    }
    public void UpdateHP()
    {
        HP.text = general.lifePoints.ToString();
    }
    public void UpdateAGI()
    {
        AGI.text = general.agilityPoints.ToString();
    }
    public void UpdateINT()
    {
        INT.text = general.intellectPoints.ToString();
    }
    public void UpdateCRIT()
    {
        CRIT.text = general.critStrikePoints.ToString();
    }
}
