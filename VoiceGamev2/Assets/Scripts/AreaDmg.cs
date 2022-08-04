using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDmg : MonoBehaviour
{
    private CameraFollow gameM;
    private PlayerMove plyM;
    private PlayerStats plyStats;
    int dmg;
    int crit;
    bool magic = false;

    public GameObject blood;
    public GameObject dust;
    private List<GameObject> bloods = new List<GameObject>();

    void Start()
    {
        gameM = GameObject.Find("GameManager").GetComponent<CameraFollow>();
        plyM = gameM.GetComponent<PlayerMove>();
        plyStats = gameM.playerParent.GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        if(GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
        else if(GetComponent<SphereCollider>())
        {
            GetComponent<SphereCollider>().isTrigger = true;
            magic = true;
        }
    }

    private void LateUpdate()
    {
        if (magic)
        {
            transform.position = Vector3.MoveTowards(transform.position, plyM.target.transform.position, 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyStats>())
        {
            if (plyM.GetAtkState() == "Partir" && plyStats.actualWeapon == "sword")
            {
                dmg = Random.Range((int)(plyStats.GetStrenght() * 1.2f), (int)(plyStats.GetStrenght() * 1.6f));
            }
            else if (plyM.GetAtkState() == "Partir" && plyStats.actualWeapon == "axe")
            {
                dmg = Random.Range((int)(plyStats.GetStrenght() * 1.5f), (int)(plyStats.GetStrenght() * 2f));
            }
            else if (plyM.GetAtkState() == "Demacia" && plyStats.actualWeapon == "sword")
            {
                dmg = Random.Range((int)(plyStats.GetStrenght() * 0.5f), (int)(plyStats.GetStrenght() * 0.8f));
            }
            else if (plyM.GetAtkState() == "Demacia" && plyStats.actualWeapon == "axe")
            {
                dmg = Random.Range((int)(plyStats.GetStrenght() * 0.5f), (int)(plyStats.GetStrenght() * 0.8f));
            }
            else if (plyM.GetAtkState() == "atk" && plyStats.actualWeapon == "fire staff")
            {
                dmg = Random.Range((int)(plyStats.GetIntellect() * 1f), (int)(plyStats.GetIntellect() * 2f));
            }
            else if (plyM.GetAtkState() == "atk" && plyStats.actualWeapon == "sacred staff")
            {
                dmg = Random.Range((int)(plyStats.GetIntellect() * 0.6f), (int)(plyStats.GetIntellect() * 0.9f));
            }
            else if (plyM.GetAtkState() == "Juicio final" && plyStats.actualWeapon == "sacred staff")
            {
                Destroy(Instantiate(dust, plyM.target.transform.position, transform.rotation), 2);

                dmg = Random.Range((int)(plyStats.GetIntellect() * 1.5f), (int)(plyStats.GetIntellect() * 2.5f));

                crit = Random.Range(0, 100);

                if (crit <= plyStats.criticProb)
                {
                    dmg = (int)(dmg * 1.5f);
                }

                for(int i = 0; i < gameM.enemys.Count; i++)
                {
                    if (Vector3.Distance(gameM.enemys[i].transform.position, plyM.target.transform.position) <= 5)
                    {
                        gameM.enemys[i].GetComponent<EnemyStats>().SetLife(-dmg);

                        Destroy(Instantiate(blood, gameM.enemys[i].transform.position, transform.rotation), 2);
                    }
                }

                return;
            }

            crit = Random.Range(0, 100);

            if (crit <= plyStats.criticProb)
            {
                dmg = (int)(dmg * 1.5f);
            }

            other.gameObject.GetComponent<EnemyStats>().SetLife(-dmg);
            other.gameObject.GetComponent<EnemyStats>().StunEnemy(true);
            bloods.Add(Instantiate(blood, other.transform.position, transform.rotation));

            if (plyM.GetAtkState() == "atk" && plyStats.actualWeapon == "fire staff") Destroy(this.gameObject);
            else if (plyM.GetAtkState() == "atk" && plyStats.actualWeapon == "sacred staff") Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = false;
        else if (GetComponent<SphereCollider>()) GetComponent<SphereCollider>().isTrigger = false;

        for (int i = 0; i< bloods.Count; i++)
        {
            Destroy(bloods[i]);
        }

        bloods = new List<GameObject>();
    }
}
