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
    [SerializeField] private Image playerImage;
    [SerializeField] private TextMeshProUGUI player;

    [SerializeField] private RawImage magicSkill1;
    [SerializeField] private RawImage magicSkill2;
    [SerializeField] private RawImage magicSkill3;

    [SerializeField] private RawImage strenghtSkill1;
    [SerializeField] private RawImage strenghtSkill2;
    [SerializeField] private RawImage strenghtSkill3;

    /*[SerializeField] private RawImage agilitySkill1;
    [SerializeField] private RawImage agilitySkill2;
    [SerializeField] private RawImage agilitySkill3;*/

    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI ARMOR;
    [SerializeField] private TextMeshProUGUI STR;
    [SerializeField] private TextMeshProUGUI varialbeText;
    //[SerializeField] private TextMeshProUGUI AGI;
    //[SerializeField] private TextMeshProUGUI INT;
    [SerializeField] private TextMeshProUGUI CRIT;
    private FTUE_Progresion fTUE_Progresion;
    private int nextLvl;
    [SerializeField] public TextMeshProUGUI amountofLvl;

    [SerializeField]private ScriptableObject[] actualWeaps;
    [HideInInspector]public bool isMagic;
    [HideInInspector]public GeneralStats general;
    private GameSave gameSave;
    [SerializeField] private GameObject physicalSkills;
    [SerializeField] private GameObject magicalSkills;
    private bool restrictions;
    //[SerializeField] private Inventory inventory;

    private void Awake()
    {
        gameSave = GameObject.Find("GameSaver").GetComponent<GameSave>();
        fTUE_Progresion = GameObject.Find("Canvas").GetComponent<FTUE_Progresion>();
    }
    public void DisplayCharacterInf(GameObject actualCharacter, bool restriction)
    {
        restrictions = restriction;
        GeneralStats stats = GameObject.Find(actualCharacter.transform.name).GetComponent<GeneralStats>();
        GameObject.Find(actualCharacter.transform.name).GetComponent<LevelSystem>().DeactivateButtons();
        
        general = stats;
        gameSave.SaveGame();
        fTUE_Progresion.actualPlayer = actualCharacter.transform.name;
        CallThings(actualCharacter);
        Values(stats);
        CheckWeapon(stats);
        
        print("Continua despues del for");
        /*else
        {
            print("Tiene arma" + actualCharacter.name+  "de un total de "+ inventory.actualWeapons.Count);
            for(int i = 0; i < inventory.actualWeapons.Count; i++)
            {
                Scripteable_Weapon weap = (Scripteable_Weapon)actualWeaps[i];
                if (weap.weaponName == stats.weaponequiped)
                {
                    print("Se ha equipado" + inventory.actualWeapons[i]);
                    weaponImagePosition.sprite = weap.artwork;
                    inventory.actualEquipedWeapon = weap;
                    weap.equiped = true;
                }
            }
            print("TIene la arma: " + stats.weaponequiped);
        }*/
        HP.text = stats.lifePoints.ToString();
        
        //AGI.text = stats.agilityPoints.ToString();
        
        if(actualCharacter.transform.name == "Magnus")
        {
            varialbeText.text = "Fuerza:";
            STR.text = stats.strengthPoints.ToString();
            isMagic = false;
            physicalSkills.SetActive(true);
            magicalSkills.SetActive(false);
            physicalBar = GameObject.Find("physical_bar").GetComponent<Slider>();
            physicalBar.value = stats.strengthPoints;
        }
        else
        {
            varialbeText.text = "Intelecto:";
            STR.text = stats.intellectPoints.ToString();
            isMagic = true;
            magicalSkills.SetActive(true);
            physicalSkills.SetActive(false);
            magicBar = GameObject.Find("magic_bar").GetComponent<Slider>();
            magicBar.value = stats.intellectPoints;
        }

        CRIT.text = stats.critStrikePoints.ToString();
        ARMOR.text = stats.armorPoints.ToString();

        playerImage.sprite = general.charImage;
        
        //agilityBar.value = stats.agilityPoints;
        player.text = actualCharacter.name;
        amountofLvl.text = "Puntos restantes: " + actualCharacter.GetComponent<LevelSystem>().amountOfLvl.ToString();
        //CheckAgility();
        CheckIntellect();
        CheckStrenght();

    }
    private void CheckWeapon(GeneralStats stats)
    {
        if (stats.weaponequiped != null)
        {
            for (int i = 0; i < actualWeaps.Length; i++)
            {
                print(actualWeaps[i].name + stats.weaponequiped);
                if (stats.weaponequiped == actualWeaps[i].name)
                {
                    Scripteable_Weapon weap = (Scripteable_Weapon)actualWeaps[i];
                    weaponImagePosition.sprite = weap.artwork;
                    return;
                }
            }
            print("No tiene arma");
            weaponImagePosition.sprite = null;
        }
    }
    public void Values(GeneralStats stats)
    {
        LevelSystem level = general.gameObject.GetComponent<LevelSystem>();
        int lifeValue = 25;
        for (int i = 2; i <= stats.lifePoints; i++)
        {
            lifeValue += (int)(i * 1.5f);
        }
        TextMeshProUGUI life = HP.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        life.text = lifeValue.ToString();
        
        int strengthValue = 5;
        
        if (isMagic)
        {
            int intellectValue = 5;
            for (int i = 2; i <= stats.intellectPoints; i++)
            {
                intellectValue += (int)i / 3;
                if (i == stats.intellectPoints)
                {
                    
                    nextLvl = (int)(i / 3);
                }
            }
            TextMeshProUGUI intel = STR.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            intel.text = intellectValue.ToString();
            if (level.amountOfLvl > 0)
            {
                TextMeshProUGUI intelnext = GameObject.Find("str_button").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                intelnext.text = "+" + nextLvl.ToString();
            }
        }
        else
        {
            for (int i = 2; i <= stats.strengthPoints; i++)
            {
                strengthValue += (int)i / 3;
                if (i == stats.strengthPoints)
                {
                    i++;
                    nextLvl = (int)(i / 3f);
                }
            }
            if (level.amountOfLvl >0)
            {
                TextMeshProUGUI strnext = GameObject.Find("str_button").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                strnext.text = "+" + nextLvl.ToString();
            }
            TextMeshProUGUI str = STR.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            
            str.text = strengthValue.ToString();
            
        }
        int shieldValue = 15;
        for (int i = 2; i <= stats.armorPoints; i++)
        {
            shieldValue += (int)(i * 0.5f);

                nextLvl = (int)(i * 0.5f);
                
        }
        if (level.amountOfLvl > 0)
        {
            print("Restrictions: " + restrictions);
            if (!restrictions)
            {
                TextMeshProUGUI shieldnext = GameObject.Find("agi_button").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                shieldnext.text = "+" + nextLvl.ToString();
            }
            
        }
        TextMeshProUGUI shield = ARMOR.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        shield.text = shieldValue.ToString();
        


        float criticProb = stats.critStrikePoints * 10f;
        TextMeshProUGUI crit = CRIT.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        crit.text = criticProb.ToString();
        
        if (level.amountOfLvl > 0)
        {
            if (!restrictions)
            {
                TextMeshProUGUI critnext = GameObject.Find("crit_button").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                critnext.text = "+10%";
            }
            
        }
        
        

        
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
    /*public void CheckAgility()
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
    }*/
    private void CallThings(GameObject actualcharacter)
    {
        
        //agilityBar = GameObject.Find("agility_bar").GetComponent<Slider>();
        //physicalBar = GameObject.Find("physical_bar").GetComponent<Scrollbar>();
        //image = GameObject.Find("magic_image").GetComponent<Image>();
        HP = GameObject.Find("hp_stat").GetComponent<TextMeshProUGUI>();
        ARMOR = GameObject.Find("armor_stat").GetComponent<TextMeshProUGUI>();
        STR = GameObject.Find("str_stat").GetComponent<TextMeshProUGUI>();
        //AGI = GameObject.Find("agi_stat").GetComponent<TextMeshProUGUI>();
        //INT = GameObject.Find("int_stat").GetComponent<TextMeshProUGUI>();
        CRIT = GameObject.Find("crit_stat").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateVAR(GeneralStats general)
    {
        if (isMagic)
        {
            STR.text = general.intellectPoints.ToString();
            magicBar.value = general.intellectPoints;
            CheckIntellect();
        }
        else
        {
            STR.text = general.strengthPoints.ToString();
            physicalBar.value = general.strengthPoints;
            CheckStrenght();
        }
    }
    public void UpdateHP(GeneralStats general)
    {
        HP.text = general.lifePoints.ToString();
    }
    public void UpdateARMOR(GeneralStats general)
    {
        ARMOR.text = general.armorPoints.ToString();
    }
    /*public void UpdateAGI(GeneralStats general)
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
    }*/
    public void UpdateCRIT(GeneralStats general)
    {
        CRIT.text = general.critStrikePoints.ToString();
    }
}
