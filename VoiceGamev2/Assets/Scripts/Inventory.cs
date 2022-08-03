using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<ScriptableObject> totalWeapons = new List<ScriptableObject>();

    [SerializeField]public List<ScriptableObject> actualWeapons = new List<ScriptableObject>();
    public ScriptableObject actualEquipedWeapon;
    [SerializeField]private Transform[] weaponPositions;
    [SerializeField] private Transform[] armorPositions;
    [SerializeField] private EquipObjects equip;
    private void Start()
    {

        for (int i = 0; i < actualWeapons.Count; i++)
        {
            DisplayWeapons(i, (Scripteable_Weapon)actualWeapons[i]);
        }
        //equip.AddDictionary();
        WeaponPositions();
    }
    public void WeaponPositions()
    {

        for (int i = 0; i < actualWeapons.Count; i++)
        {
            DisplayWeapons(i, (Scripteable_Weapon)actualWeapons[i]);
        }
        equip.AddDictionary();
    }
    public String GetName(Scripteable_Weapon item)
    {
        return item.name;
    }
    private void DisplayWeapons(int i,Scripteable_Weapon item)
    {
        weaponPositions[i].gameObject.SetActive(true);
        Image img = weaponPositions[i].GetChild(0).GetComponent<Image>();
        img.sprite = item.artwork;
        weaponPositions[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = item.name;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int a = 0; a < players.Length; a++)
        {
            print("Se comprueba" + players[a].GetComponent<GeneralStats>().weaponequiped + "De" + item.name);
            if (players[a].GetComponent<GeneralStats>().weaponequiped == item.name)
            {
                print("PUTA" + item.name);
                item.equiped = true;

                weaponPositions[i].GetChild(4).gameObject.SetActive(true);
            }

        }
        
    }
    public void ActivateTheEquipped(Scripteable_Weapon item)
    {
        for(int i = 0; i < weaponPositions.Length; i++)
        {
            print(weaponPositions[i].transform.name);
            String weap = weaponPositions[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
            if(weap == item.name.ToString())
            {
                print(weap);
                weaponPositions[i].GetChild(4).gameObject.SetActive(true);
                break;
            }
        }
    }
    public void DeactivateTheEquipped(String item)
    {
        print("OSHE QUE HA ENTRADO");
        for(int i = 0; i < weaponPositions.Length; i++)
        {
            print("Puta" + weaponPositions[i].GetChild(4).gameObject.name);
            String weap = weaponPositions[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
            if(weap == item)
            {
                weaponPositions[i].GetChild(4).gameObject.SetActive(false);
                break;
            }
        }
    }
}
