using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_skills : MonoBehaviour
{
    private Scrollbar magicBar;
    private Slider physicalBar;

    private Image image;

    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI STR;
    [SerializeField] private TextMeshProUGUI AGI;
    [SerializeField] private TextMeshProUGUI INT;
    [SerializeField] private TextMeshProUGUI CRIT;

    private void Awake()
    {
    }
    void Start()
    {
        
    }

    public void DisplayCharacterInf(GameObject actualCharacter)
    {
        
        //string player = actualCharacter.transform
        print(actualCharacter.transform.name);
        GeneralStats stats = GameObject.Find(actualCharacter.transform.name).GetComponent<GeneralStats>();
        CallThings();
        float intelligence = (float)stats.intellectPoints / 13;
        print(stats.intellectPoints);
        
        print(intelligence);
        magicBar.size = intelligence;
        HP.text = stats.lifePoints.ToString();
        STR.text = stats.strengthPoints.ToString();
        AGI.text = stats.agilityPoints.ToString();
        INT.text = stats.intellectPoints.ToString();
        CRIT.text = stats.critStrikePoints.ToString();
        image.fillAmount = stats.strengthPoints / 10;


    }
    private void CallThings()
    {
        magicBar = GameObject.Find("magic_bar").GetComponent<Scrollbar>();
        //physicalBar = GameObject.Find("physical_bar").GetComponent<Scrollbar>();
        image = GameObject.Find("magic_image").GetComponent<Image>();
        
        

        HP = GameObject.Find("hp_stat").GetComponent<TextMeshProUGUI>();
        STR = GameObject.Find("str_stat").GetComponent<TextMeshProUGUI>();
        AGI = GameObject.Find("agi_stat").GetComponent<TextMeshProUGUI>();
        INT = GameObject.Find("int_stat").GetComponent<TextMeshProUGUI>();
        CRIT = GameObject.Find("crit_stat").GetComponent<TextMeshProUGUI>();
    }
}
