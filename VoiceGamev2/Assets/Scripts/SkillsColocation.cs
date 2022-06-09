using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsColocation : MonoBehaviour
{
    [SerializeField] private Transform[] skillLocations;
    [SerializeField] private Transform[] magicSkills;
    [SerializeField] private Transform[] physicalSkills;
    [SerializeField] private Transform[] rangedSkills;
    [SerializeField] private Transform[] healingSkills;

    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetThePlayer()
    {
        int positionArray = 0;
        //playerStats = player.GetComponent<PlayerStats>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        for(int i = positionArray; i< skillLocations.Length; i++)
        {
            if(playerStats.intellectPoints == 6)
            {
                magicSkills[i].position = skillLocations[0].position;
                i++;
                if(playerStats.intellectPoints == 8)
                {
                    magicSkills[i].position = skillLocations[1].position;
                    i++;
                    if (playerStats.intellectPoints == 10)
                    {
                        magicSkills[i].position = skillLocations[2].position;
                        positionArray = i;
                    }
                    else
                    {
                        positionArray = i;
                        break;
                    }
                }
                else
                {
                    positionArray = i;
                    break;
                }
            }
        }

    }
}
