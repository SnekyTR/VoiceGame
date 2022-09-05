using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsColocation : MonoBehaviour
{
    [SerializeField] private Transform[] skillLocationsMagnus;
    [SerializeField] private Transform[] skillLocationsVagnar;
    [SerializeField] private Transform[] skillLocationsHammund;
    [SerializeField] private GameObject[] fireSkills;
    [SerializeField] private GameObject[] swordSkills;
    [SerializeField] private GameObject[] bowSkills;
    [SerializeField] private GameObject[] spearSkills;
    [SerializeField] private GameObject[] axeSkills;
    [SerializeField] private GameObject[] healingSkills;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform[] GetSkillsMagnus()
    {
        return skillLocationsMagnus;
    }

    public Transform[] GetSkillsVagnar()
    {
        return skillLocationsVagnar;
    }

    public Transform[] GetSkillsHammun()
    {
        return skillLocationsHammund;
    }

    public void AssignMagnusSkills(string actualWeapon, GameObject player)
    {
        PlayerStats playerStats;
        playerStats = player.GetComponent<PlayerStats>();
        int positionArray = 0;
        if(actualWeapon == "fire staff")
        {
            if (playerStats.intellectPoints >= 6)
            {
                GameObject fireskill1 = Instantiate(fireSkills[0], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                fireskill1.transform.parent = skillLocationsMagnus[positionArray];
                fireskill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    GameObject fireskill2 = Instantiate(fireSkills[1], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                    fireskill2.transform.parent = skillLocationsMagnus[positionArray];
                    fireskill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        GameObject fireskill3 = Instantiate(fireSkills[2], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                        fireskill3.transform.parent = skillLocationsMagnus[positionArray];
                        fireskill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if(actualWeapon == "sword")
        {
            if (playerStats.strengthPoints >= 6)
            {
                GameObject sworkSkill1 = Instantiate(swordSkills[0], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                sworkSkill1.transform.parent = skillLocationsMagnus[positionArray];
                sworkSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 8)
                {
                    GameObject sworkSkill2 = Instantiate(swordSkills[1], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                    sworkSkill2.transform.parent = skillLocationsMagnus[positionArray];
                    sworkSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.strengthPoints >= 10)
                    {
                        GameObject sworkSkill3 = Instantiate(swordSkills[2], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                        sworkSkill3.transform.parent = skillLocationsMagnus[positionArray];
                        sworkSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "axe")
        {
            if (playerStats.strengthPoints >= 6)
            {
                GameObject axeSkill1 = Instantiate(axeSkills[0], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                axeSkill1.transform.parent = skillLocationsMagnus[positionArray];
                axeSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 8)
                {
                    GameObject axeSkill2 = Instantiate(axeSkills[1], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                    axeSkill2.transform.parent = skillLocationsMagnus[positionArray];
                    axeSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.strengthPoints >= 10)
                    {
                        GameObject axeSkill3 = Instantiate(axeSkills[2], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                        axeSkill3.transform.parent = skillLocationsMagnus[positionArray];
                        axeSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if(actualWeapon == "bow")
        {
            if (playerStats.agilityPoints >= 6)
            {
                GameObject bowSkill1 = Instantiate(bowSkills[0], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                bowSkill1.transform.parent = skillLocationsMagnus[positionArray];
                bowSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.agilityPoints >= 8)
                {
                    GameObject bowSkill2 = Instantiate(bowSkills[1], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                    bowSkill2.transform.parent = skillLocationsMagnus[positionArray];
                    bowSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.agilityPoints >= 10)
                    {
                        GameObject bowSkill3 = Instantiate(bowSkills[2], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                        bowSkill3.transform.parent = skillLocationsMagnus[positionArray];
                        bowSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "spear")
        {
            if (playerStats.agilityPoints >= 6)
            {
                GameObject spearSkill1 =  Instantiate(spearSkills[0], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                spearSkill1.transform.parent = skillLocationsMagnus[positionArray];
                spearSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.agilityPoints >= 8)
                {
                    GameObject spearSkill2 = Instantiate(spearSkills[1], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                    spearSkill2.transform.parent = skillLocationsMagnus[positionArray];
                    spearSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.agilityPoints >= 10)
                    {
                        GameObject spearSkill3 = Instantiate(spearSkills[2], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                        spearSkill3.transform.parent = skillLocationsMagnus[positionArray];
                        spearSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if(actualWeapon == "sacred staff"){
            if (playerStats.intellectPoints >= 6)
            {
                GameObject healingskill1 = Instantiate(healingSkills[0], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                healingskill1.transform.parent = skillLocationsMagnus[positionArray];
                healingskill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    GameObject healingskill2 = Instantiate(healingSkills[1], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                    healingskill2.transform.parent = skillLocationsMagnus[positionArray];
                    healingskill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        GameObject healingskill3 = Instantiate(healingSkills[2], skillLocationsMagnus[positionArray].position, Quaternion.identity);
                        healingskill3.transform.parent = skillLocationsMagnus[positionArray];
                        healingskill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
    }
    public void AssignVagnarSkills(string actualWeapon, GameObject player)
    {
        PlayerStats playerStats;
        playerStats = player.GetComponent<PlayerStats>();
        int positionArray = 0;
        if (actualWeapon == "fire staff")
        {
            if (playerStats.intellectPoints >= 6)
            {
                GameObject fireskill1 = Instantiate(fireSkills[0], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                fireskill1.transform.parent = skillLocationsVagnar[positionArray];
                fireskill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    GameObject fireskill2 = Instantiate(fireSkills[1], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                    fireskill2.transform.parent = skillLocationsVagnar[positionArray];
                    fireskill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        GameObject fireskill3 = Instantiate(fireSkills[2], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                        fireskill3.transform.parent = skillLocationsVagnar[positionArray];
                        fireskill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "sword")
        {
            if (playerStats.strengthPoints >= 6)
            {
                GameObject sworkSkill1 = Instantiate(swordSkills[0], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                sworkSkill1.transform.parent = skillLocationsVagnar[positionArray];
                sworkSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 8)
                {
                    GameObject sworkSkill2 = Instantiate(swordSkills[1], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                    sworkSkill2.transform.parent = skillLocationsVagnar[positionArray];
                    sworkSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.strengthPoints >= 10)
                    {
                        GameObject sworkSkill3 = Instantiate(swordSkills[2], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                        sworkSkill3.transform.parent = skillLocationsVagnar[positionArray];
                        sworkSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "axe")
        {
            if (playerStats.strengthPoints >= 6)
            {
                GameObject axeSkill1 = Instantiate(axeSkills[0], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                axeSkill1.transform.parent = skillLocationsVagnar[positionArray];
                axeSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 8)
                {
                    GameObject axeSkill2 = Instantiate(axeSkills[1], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                    axeSkill2.transform.parent = skillLocationsVagnar[positionArray];
                    axeSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.strengthPoints >= 10)
                    {
                        GameObject axeSkill3 = Instantiate(axeSkills[2], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                        axeSkill3.transform.parent = skillLocationsVagnar[positionArray];
                        axeSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "bow")
        {
            if (playerStats.agilityPoints >= 6)
            {
                GameObject bowSkill1 = Instantiate(bowSkills[0], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                bowSkill1.transform.parent = skillLocationsVagnar[positionArray];
                bowSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.agilityPoints >= 8)
                {
                    GameObject bowSkill2 = Instantiate(bowSkills[1], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                    bowSkill2.transform.parent = skillLocationsVagnar[positionArray];
                    bowSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.agilityPoints >= 10)
                    {
                        GameObject bowSkill3 = Instantiate(bowSkills[2], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                        bowSkill3.transform.parent = skillLocationsVagnar[positionArray];
                        bowSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "spear")
        {
            if (playerStats.agilityPoints >= 6)
            {
                GameObject spearSkill1 = Instantiate(spearSkills[0], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                spearSkill1.transform.parent = skillLocationsVagnar[positionArray];
                spearSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.agilityPoints >= 8)
                {
                    GameObject spearSkill2 = Instantiate(spearSkills[1], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                    spearSkill2.transform.parent = skillLocationsVagnar[positionArray];
                    spearSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.agilityPoints >= 10)
                    {
                        GameObject spearSkill3 = Instantiate(spearSkills[2], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                        spearSkill3.transform.parent = skillLocationsVagnar[positionArray];
                        spearSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "sacred staff")
        {
            if (playerStats.intellectPoints >= 6)
            {
                GameObject healingskill1 = Instantiate(healingSkills[0], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                healingskill1.transform.parent = skillLocationsVagnar[positionArray];
                healingskill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    GameObject healingskill2 = Instantiate(healingSkills[1], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                    healingskill2.transform.parent = skillLocationsVagnar[positionArray];
                    healingskill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        GameObject healingskill3 = Instantiate(healingSkills[2], skillLocationsVagnar[positionArray].position, Quaternion.identity);
                        healingskill3.transform.parent = skillLocationsVagnar[positionArray];
                        healingskill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
    }
    public void AssignHammundSkills(string actualWeapon, GameObject player)
    {
        PlayerStats playerStats;
        playerStats = player.GetComponent<PlayerStats>();
        int positionArray = 0;
        if (actualWeapon == "fire staff")
        {
            if (playerStats.intellectPoints >= 6)
            {
                GameObject fireskill1 = Instantiate(fireSkills[0], skillLocationsHammund[positionArray].position, Quaternion.identity);
                fireskill1.transform.parent = skillLocationsHammund[positionArray];
                fireskill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    GameObject fireskill2 = Instantiate(fireSkills[1], skillLocationsHammund[positionArray].position, Quaternion.identity);
                    fireskill2.transform.parent = skillLocationsHammund[positionArray];
                    fireskill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        GameObject fireskill3 = Instantiate(fireSkills[2], skillLocationsHammund[positionArray].position, Quaternion.identity);
                        fireskill3.transform.parent = skillLocationsHammund[positionArray];
                        fireskill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "sword")
        {
            if (playerStats.strengthPoints >= 6)
            {
                GameObject sworkSkill1 = Instantiate(swordSkills[0], skillLocationsHammund[positionArray].position, Quaternion.identity);
                sworkSkill1.transform.parent = skillLocationsHammund[positionArray];
                sworkSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 8)
                {
                    GameObject sworkSkill2 = Instantiate(swordSkills[1], skillLocationsHammund[positionArray].position, Quaternion.identity);
                    sworkSkill2.transform.parent = skillLocationsHammund[positionArray];
                    sworkSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.strengthPoints >= 10)
                    {
                        GameObject sworkSkill3 = Instantiate(swordSkills[2], skillLocationsHammund[positionArray].position, Quaternion.identity);
                        sworkSkill3.transform.parent = skillLocationsHammund[positionArray];
                        sworkSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "axe")
        {
            if (playerStats.strengthPoints >= 6)
            {
                GameObject axeSkill1 = Instantiate(axeSkills[0], skillLocationsHammund[positionArray].position, Quaternion.identity);
                axeSkill1.transform.parent = skillLocationsHammund[positionArray];
                axeSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 8)
                {
                    GameObject axeSkill2 = Instantiate(axeSkills[1], skillLocationsHammund[positionArray].position, Quaternion.identity);
                    axeSkill2.transform.parent = skillLocationsHammund[positionArray];
                    axeSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.strengthPoints >= 10)
                    {
                        GameObject axeSkill3 = Instantiate(axeSkills[2], skillLocationsHammund[positionArray].position, Quaternion.identity);
                        axeSkill3.transform.parent = skillLocationsHammund[positionArray];
                        axeSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "bow")
        {
            if (playerStats.agilityPoints >= 6)
            {
                GameObject bowSkill1 = Instantiate(bowSkills[0], skillLocationsHammund[positionArray].position, Quaternion.identity);
                bowSkill1.transform.parent = skillLocationsHammund[positionArray];
                bowSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.agilityPoints >= 8)
                {
                    GameObject bowSkill2 = Instantiate(bowSkills[1], skillLocationsHammund[positionArray].position, Quaternion.identity);
                    bowSkill2.transform.parent = skillLocationsHammund[positionArray];
                    bowSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.agilityPoints >= 10)
                    {
                        GameObject bowSkill3 = Instantiate(bowSkills[2], skillLocationsHammund[positionArray].position, Quaternion.identity);
                        bowSkill3.transform.parent = skillLocationsHammund[positionArray];
                        bowSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "spear")
        {
            if (playerStats.agilityPoints >= 6)
            {
                GameObject spearSkill1 = Instantiate(spearSkills[0], skillLocationsHammund[positionArray].position, Quaternion.identity);
                spearSkill1.transform.parent = skillLocationsHammund[positionArray];
                spearSkill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                positionArray++;
                if (playerStats.agilityPoints >= 8)
                {
                    GameObject spearSkill2 = Instantiate(spearSkills[1], skillLocationsHammund[positionArray].position, Quaternion.identity);
                    spearSkill2.transform.parent = skillLocationsHammund[positionArray];
                    spearSkill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    positionArray++;
                    if (playerStats.agilityPoints >= 10)
                    {
                        GameObject spearSkill3 = Instantiate(spearSkills[2], skillLocationsHammund[positionArray].position, Quaternion.identity);
                        spearSkill3.transform.parent = skillLocationsHammund[positionArray];
                        spearSkill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        if (actualWeapon == "sacred staff")
        {
            if (playerStats.intellectPoints >= 6)
            {
                GameObject healingskill1 = Instantiate(healingSkills[0], skillLocationsHammund[positionArray].position, Quaternion.identity);
                healingskill1.transform.parent = skillLocationsHammund[positionArray];
                healingskill1.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    GameObject healingskill2 = Instantiate(healingSkills[1], skillLocationsHammund[positionArray].position, Quaternion.identity);
                    healingskill2.transform.parent = skillLocationsHammund[positionArray];
                    healingskill2.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        GameObject healingskill3 = Instantiate(healingSkills[2], skillLocationsHammund[positionArray].position, Quaternion.identity);
                        healingskill3.transform.parent = skillLocationsHammund[positionArray];
                        healingskill3.transform.localScale = new Vector3(0.7f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
    }
    /*public void GetThePlayer(Transform magnus)
    {        
        //playerStats = magnus.GetComponent<PlayerStats>();
        //playerStats = GameObject.Find("Magnus").GetComponent<PlayerStats>();
        playerStats = magnus.gameObject.GetComponent<PlayerStats>();

        
        print(playerStats.intellectPoints);
        if (magnus.weaponName == "Magnus")
        {
            
        }
        else
        {
     
        }
    }

    public void DestroySkillIcons()
    {

        Object.Destroy(magicskill1);
        Object.Destroy(magicskill2);
        Object.Destroy(magicskill3);

        Object.Destroy(healingskill1);
        Object.Destroy(healingskill2);
        Object.Destroy(healingskill3);

        Object.Destroy(physicalskill1);
        Object.Destroy(physicalskill2);
        Object.Destroy(physicalskill3);

        Object.Destroy(rangerskill1);
        Object.Destroy(rangerskill2);
        Object.Destroy(rangerskill3);
    }*/
}
