using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsColocation : MonoBehaviour
{
    [SerializeField] private Transform[] skillLocations;
    [SerializeField] private GameObject[] magicSkills;
    [SerializeField] private GameObject[] physicalSkills;
    [SerializeField] private GameObject[] rangedSkills;
    [SerializeField] private GameObject[] healingSkills;

    [SerializeField]private PlayerStats playerStats;

    private GameObject magicskill1;
    private GameObject magicskill2;
    private GameObject magicskill3;

    private GameObject healingskill1;
    private GameObject healingskill2;
    private GameObject healingskill3;

    private GameObject physicalskill1;
    private GameObject physicalskill3;
    private GameObject physicalskill2;

    private GameObject rangerskill1;
    private GameObject rangerskill2;
    private GameObject rangerskill3;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetThePlayer(Transform player)
    {
        DestroySkillIcons();
        int positionArray = 0;
        //playerStats = player.GetComponent<PlayerStats>();
        //playerStats = GameObject.Find("Magnus").GetComponent<PlayerStats>();
        playerStats = player.gameObject.GetComponent<PlayerStats>();

        int i = positionArray;
        print(playerStats.intellectPoints);
        if (player.name == "Magnus")
        {
            if (playerStats.intellectPoints >= 6)
            {
                //magicSkills[i].position = skillLocations[0].position;
                magicskill1 = Instantiate(magicSkills[0], skillLocations[positionArray].position, Quaternion.identity);
                magicskill1.transform.parent = skillLocations[positionArray];
                magicskill1.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                //magicSkills[i].gameObject.SetActive(true);
                print(positionArray);
                positionArray++;
                if (playerStats.intellectPoints >= 8)
                {
                    //magicSkills[i].transform.position = skillLocations[1].position;
                    magicskill2 = Instantiate(magicSkills[1], skillLocations[positionArray].position, Quaternion.identity);
                    magicskill2.transform.parent = skillLocations[positionArray];
                    magicskill2.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 10)
                    {
                        print("Entra 10" + positionArray);

                        //magicSkills[i].transform.position = skillLocations[2].position;
                        magicskill3 = Instantiate(magicSkills[2], skillLocations[positionArray].position, Quaternion.identity);
                        magicskill3.transform.parent = skillLocations[positionArray];
                        magicskill3.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                        positionArray++;
                    }
                }
            }
        }
        else
        {
            /*if(player.name == "Valfar")
            {
                if (playerStats.intellectPoints >= 6)
                {
                    //magicSkills[i].position = skillLocations[0].position;
                    healingskill1 = Instantiate(healingSkills[0], skillLocations[positionArray].position, Quaternion.identity);
                    healingskill1.transform.parent = skillLocations[positionArray];
                    healingskill1.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                    //magicSkills[i].gameObject.SetActive(true);
                    print(positionArray);
                    positionArray++;
                    if (playerStats.intellectPoints >= 8)
                    {
                        //magicSkills[i].transform.position = skillLocations[1].position;
                        healingskill2 = Instantiate(healingSkills[1], skillLocations[positionArray].position, Quaternion.identity);
                        healingskill2.transform.parent = skillLocations[positionArray];
                        healingskill2.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                        print(positionArray);
                        positionArray++;
                        if (playerStats.intellectPoints >= 10)
                        {
                            print("Entra 10" + positionArray);

                            //magicSkills[i].transform.position = skillLocations[2].position;
                            healingskill3 = Instantiate(healingSkills[2], skillLocations[positionArray].position, Quaternion.identity);
                            healingskill3.transform.parent = skillLocations[positionArray];
                            healingskill3.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                            positionArray++;
                        }
                    }
                }
            }*/
        }

        /*if (playerStats.strengthPoints >= 6)
        {
            //magicSkills[i].position = skillLocations[0].position;
            physicalskill1 = Instantiate(physicalSkills[0], skillLocations[positionArray].position, Quaternion.identity);
            physicalskill1.transform.parent = skillLocations[positionArray];
            physicalskill1.transform.localScale = new Vector3(0.65f, 0.65f, 1);
            //magicSkills[i].gameObject.SetActive(true);
            print("Se ha generado");
            positionArray++;
            if (playerStats.strengthPoints >= 8)
            {
            //magicSkills[i].transform.position = skillLocations[1].position;
                physicalskill2 = Instantiate(physicalSkills[1], skillLocations[positionArray].position, Quaternion.identity);
                physicalskill2.transform.parent = skillLocations[positionArray];
                physicalskill2.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                positionArray++;
                if (playerStats.strengthPoints >= 10)
                {
                    //magicSkills[i].transform.position = skillLocations[2].position;
                    physicalskill3 = Instantiate(physicalSkills[2], skillLocations[positionArray].position, Quaternion.identity);
                    physicalskill3.transform.parent = skillLocations[positionArray];
                    physicalskill3.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                    positionArray++;
                }
            }
        }
    if (playerStats.agilityPoints >= 6)
    {
        //magicSkills[i].position = skillLocations[0].position;
        rangerskill1 = Instantiate(rangedSkills[0], skillLocations[positionArray].position, Quaternion.identity);
        rangerskill1.transform.parent = skillLocations[positionArray];
        rangerskill1.transform.localScale = new Vector3(0.65f, 0.65f, 1);
        //magicSkills[i].gameObject.SetActive(true);
        print("Se ha generado");
        positionArray++;
        if (playerStats.agilityPoints >= 8)
        {
            //magicSkills[i].transform.position = skillLocations[1].position;
            rangerskill2 = Instantiate(rangedSkills[1], skillLocations[positionArray].position, Quaternion.identity);
            rangerskill2.transform.parent = skillLocations[positionArray];
            rangerskill2.transform.localScale = new Vector3(0.65f, 0.65f, 1);
            positionArray++;
            if (playerStats.agilityPoints >= 10)
            {
                //magicSkills[i].transform.position = skillLocations[2].position;
                rangerskill3 = Instantiate(physicalSkills[2], skillLocations[positionArray].position, Quaternion.identity);
                rangerskill3.transform.parent = skillLocations[positionArray];
                rangerskill3.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                positionArray++;
            }
        }
    }*/
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
    }
}
