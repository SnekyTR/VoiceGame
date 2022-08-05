using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_skills : MonoBehaviour
{
    private Slider magicBar;
    private Slider physicalBar;
    private Slider agilityBar;
    [SerializeField] private Image weaponImagePosition;
    [SerializeField] private TextMeshProUGUI player;

    [SerializeField] private RawImage magicSkill1;
    [SerializeField] private RawImage magicSkill2;
    [SerializeField] private RawImage magicSkill3;

    [SerializeField] private RawImage strenghtSkill1;
    [SerializeField] private RawImage strenghtSkill2;
    [SerializeField] private RawImage strenghtSkill3;

    [SerializeField] private RawImage agilitySkill1;
    [SerializeField] private RawImage agilitySkill2;
    [SerializeField] private RawImage agilitySkill3;

    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI STR;
    [SerializeField] private TextMeshProUGUI AGI;
    [SerializeField] private TextMeshProUGUI INT;
    [SerializeField] private TextMeshProUGUI CRIT;

    [SerializeField] public TextMeshProUGUI amountofLvl;

    private GeneralStats general;
    [SerializeField] private Inventory inventory;

    private void Awake()
    {
    }
    void Start()
    {
        
    }
    private void Update()
    {
    }

    public void DisplayCharacterInf(GameObject actualCharacter)
    {
        GeneralStats stats = GameObject.Find(actualCharacter.transform.name).GetComponent<GeneralStats>();
        GameObject.Find(actualCharacter.transform.name).GetComponent<LevelSystem>().DeactivateButtons();
        general = stats;
        CallThings(actualCharacter);
        if(stats.weaponequiped == null){
            print("No tiene arma");
            weaponImagePosition.sprite = null;
        }
        else
        {
            print("Tiene arma" + actualCharacter.name+  "de un total de "+ inventory.actualWeapons.Count);
            for(int i = 0; i < inventory.actualWeapons.Count; i++)
            {
                Scripteable_Weapon weap = (Scripteable_Weapon)inventory.actualWeapons[i];
                if (weap.weaponName == stats.weaponequiped)
                {
                    print("Se ha equipado" + inventory.actualWeapons[i]);
                    weaponImagePosition.sprite = weap.artwork;
                    inventory.actualEquipedWeapon = weap;
                    weap.equiped = true;
                }
            }
            print("TIene la arma: " + stats.weaponequiped);
        }
        HP.text = stats.lifePoints.ToString();
        STR.text = stats.strengthPoints.ToString();
        AGI.text = stats.agilityPoints.ToString();
        INT.text = stats.intellectPoints.ToString();
        CRIT.text = stats.critStrikePoints.ToString();
        magicBar.value = stats.intellectPoints;
        physicalBar.value = stats.strengthPoints;
        agilityBar.value = stats.agilityPoints;
        player.text = actualCharacter.name;
        amountofLvl.text = "Puntos restantes: " + actualCharacter.GetComponent<LevelSystem>().amountOfLvl.ToString();
        CheckAgility();
        CheckIntellect();
        CheckStrenght();
    }
    public void CheckStrenght()
    {
        if (general.strengthPoints >= 6)
        {
            strenghtSkill1.color = new Color(255, 255, 255, 255);
            if (general.strengthPoints >= 8)
            {
                strenghtSkill2.color = new Color(255, 255, 255, 255);
                if (general.strengthPoints >= 10)
                {
                    strenghtSkill3.color = new Color(255, 255, 255, 255);
                }
                else
                {
                    strenghtSkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
                }
            }
            else
            {
                strenghtSkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
                strenghtSkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
            }
        }
        else
        {
            strenghtSkill1.color = new Color(0.15f, 0.15f, 0.15f, 255);
            strenghtSkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
            strenghtSkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
        }
    }
    public void CheckIntellect()
    {
        if (general.intellectPoints >= 6)
        {
            magicSkill1.color = new Color(255, 255, 255, 255);
            if (general.intellectPoints >= 8)
            {
                magicSkill2.color = new Color(255, 255, 255, 255);
                if (general.intellectPoints >= 10)
                {
                    magicSkill3.color = new Color(255, 255, 255, 255);
                }
                else
                {
                   magicSkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
                }
            }
            else
            {
                magicSkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
                magicSkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
            }
        }
        else
        {
            magicSkill1.color = new Color(0.15f, 0.15f, 0.15f, 255);
            magicSkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
            magicSkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
        }
    }
    public void CheckAgility()
    {
        if (general.agilityPoints >= 6)
        {
            agilitySkill1.color = new Color(255, 255, 255, 255);
            if (general.agilityPoints >= 8)
            {
                agilitySkill2.color = new Color(255, 255, 255, 255);
                if (general.agilityPoints >= 10)
                {
                    agilitySkill3.color = new Color(255, 255, 255, 255);
                }
                else
                {
                    agilitySkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
                }
            }
            else
            {
                agilitySkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
                agilitySkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
            }
        }
        else
        {
            agilitySkill1.color = new Color(0.15f, 0.15f, 0.15f, 255);
            agilitySkill2.color = new Color(0.15f, 0.15f, 0.15f, 255);
            agilitySkill3.color = new Color(0.15f, 0.15f, 0.15f, 255);
        }
    }
    private void CallThings(GameObject actualcharacter)
    {
        magicBar = GameObject.Find("magic_bar").GetComponent<Slider>();
        physicalBar = GameObject.Find("physical_bar").GetComponent<Slider>();
        agilityBar = GameObject.Find("agility_bar").GetComponent<Slider>();
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
        CheckStrenght();
    }
    public void UpdateHP(GeneralStats general)
    {
        HP.text = general.lifePoints.ToString();
    }
    public void UpdateAGI(GeneralStats general)
    {
        AGI.text = general.agilityPoints.ToString();
        agilityBar.value = general.agilityPoints;
        CheckAgility();
    }
    public void UpdateINT(GeneralStats general)
    {
        INT.text = general.intellectPoints.ToString();
        magicBar.value = general.intellectPoints;
        CheckIntellect();
    }
    public void UpdateCRIT(GeneralStats general)
    {
        CRIT.text = general.critStrikePoints.ToString();
    }
}
