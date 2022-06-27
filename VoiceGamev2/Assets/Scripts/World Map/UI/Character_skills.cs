using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_skills : MonoBehaviour
{
    private Slider magicBar;
    private Slider physicalBar;

    private Slider image;

    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI STR;
    [SerializeField] private TextMeshProUGUI AGI;
    [SerializeField] private TextMeshProUGUI INT;
    [SerializeField] private TextMeshProUGUI CRIT;

    private GeneralStats general;

    private void Awake()
    {
    }
    void Start()
    {
        
    }

    public void DisplayCharacterInf(GameObject actualCharacter)
    {
        GeneralStats stats = GameObject.Find(actualCharacter.transform.name).GetComponent<GeneralStats>();
        general = stats;
        CallThings();
        //magicBar.fillAmount = (float)stats.intellectPoints / 10;
        HP.text = stats.lifePoints.ToString();
        STR.text = stats.strengthPoints.ToString();
        AGI.text = stats.agilityPoints.ToString();
        INT.text = stats.intellectPoints.ToString();
        CRIT.text = stats.critStrikePoints.ToString();
        magicBar.value = stats.intellectPoints;


    }
    private void CallThings()
    {
        magicBar = GameObject.Find("magic_bar").GetComponent<Slider>();
        physicalBar = GameObject.Find("physical_bar").GetComponent<Slider>();
        //physicalBar = GameObject.Find("physical_bar").GetComponent<Scrollbar>();
        //image = GameObject.Find("magic_image").GetComponent<Image>();
        HP = GameObject.Find("hp_stat").GetComponent<TextMeshProUGUI>();
        STR = GameObject.Find("str_stat").GetComponent<TextMeshProUGUI>();
        AGI = GameObject.Find("agi_stat").GetComponent<TextMeshProUGUI>();
        INT = GameObject.Find("int_stat").GetComponent<TextMeshProUGUI>();
        CRIT = GameObject.Find("crit_stat").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateSRT(GeneralStats general)
    {
        STR.text = general.strengthPoints.ToString();
        physicalBar.value = general.strengthPoints;
    }
    public void UpdateHP(GeneralStats general)
    {
        HP.text = general.lifePoints.ToString();
    }
    public void UpdateAGI(GeneralStats general)
    {
        AGI.text = general.agilityPoints.ToString();
    }
    public void UpdateINT(GeneralStats general)
    {
        INT.text = general.intellectPoints.ToString();
        magicBar.value = general.intellectPoints;
    }
    public void UpdateCRIT(GeneralStats general)
    {
        CRIT.text = general.critStrikePoints.ToString();
    }
}
