using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class EquipObjects : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIMovement uIMovement;
    [SerializeField] private GameSave gameSave;
    private Dictionary<string, Action> weaponOptions = new Dictionary<string, Action>();
    public KeywordRecognizer weapons;
    private Dictionary<string, Action> equipWeapon = new Dictionary<string, Action>();
    public KeywordRecognizer equipOrders;
    public String selectedWeapon;
    public String equipedWeapon;
    public bool isInventory;

    [SerializeField]private Image weaponImage;
    //public bool ableToEquip = false;

    private void Start()
    {
        AddEquipOrders();
    }
    public void AddDictionary()
    {
        for (int i = 0; i < inventory.actualWeapons.Count; i++)
        {
            Scripteable_Weapon data = (Scripteable_Weapon)inventory.actualWeapons[i];
            //String weap = inventory((Scripteable_Weapon)inventory.actualWeapons[i].name);
            weaponOptions.Add(data.name, SelectObject);
            print("Se ha añadido la arma" + data.name);
         
            /*weaponOptions.Add("Arco de lagrimas de andreu", SelectObject);
            weaponOptions.Add("espada revienta culo", SelectObject);
            weaponOptions.Add("baculo quemador de pelo", SelectObject);*/
        }
        weapons = new KeywordRecognizer(weaponOptions.Keys.ToArray());
        weapons.OnPhraseRecognized += RecognizedVoice;
        weapons.Start();
    }
    public void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        selectedWeapon = speech.text.ToString();
        Debug.Log(speech.text);
        weaponOptions[speech.text].Invoke();
        
    }
    private void ResetDictionary()
    {
        if (!PlayerPrefs.HasKey("pm")) PlayerPrefs.SetInt("pm", 0);

        int ns = PlayerPrefs.GetInt("pm");
        PlayerPrefs.SetInt("pm", (ns + 1));
        Dictionary<string, Action> zero1 = new Dictionary<string, Action>();
        zero1.Add("asdfasd" + SceneManager.GetActiveScene().name + ns, SelectObject);
        weapons = new KeywordRecognizer(zero1.Keys.ToArray());
    }
    private void SelectObject()
    {
        if (isInventory)
        {
            Scripteable_Weapon actual = (Scripteable_Weapon)inventory.actualEquipedWeapon;
            if (selectedWeapon == actual.name)
            {
                print("Has seleccionado tu propia arma");
                return;
            }
            print("Quieres equiparlo?"+ "Se ha seleccionado " + selectedWeapon +" Actual "+ actual.name);
            equipOrders.Start();
        }
    }
    private void AddEquipOrders()
    {
        equipWeapon.Add("Equipar", EquipItem);
        equipWeapon.Add("Cancelar", CancelEquip);
        equipOrders = new KeywordRecognizer(equipWeapon.Keys.ToArray());
        equipOrders.OnPhraseRecognized += RecognizedEquipVoice;
    }
    public void RecognizedEquipVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        equipWeapon[speech.text].Invoke();
    }
    private void EquipItem()
    {
        equipOrders.Stop();
        Scripteable_Weapon actual = (Scripteable_Weapon)inventory.actualEquipedWeapon;
        for(int i = 0; i < inventory.actualWeapons.Count; i++)
        {

            Scripteable_Weapon items = (Scripteable_Weapon)inventory.actualWeapons[i];
            if (items.name == selectedWeapon)
            {
                if (items.equiped)
                {
                    print("Esta arma esta equipada");
                    return;
                }

                print("ACTUALMENTE TIENES EL ARMA: " + equipedWeapon);
                if (String.IsNullOrWhiteSpace(equipedWeapon))
                {
                    
                    inventory.DeactivateTheEquipped(actual.name);
                    actual.equiped = false;
                    print("Te has equipado: " + items.name + "Al personaje " + uIMovement.charSelected);
                    GeneralStats actualCharacter = GameObject.Find(uIMovement.charSelected).GetComponent<GeneralStats>();
                    actualCharacter.weaponequiped = items.name;
                    actualCharacter.weaponType = items.weaponType;
                    equipedWeapon = items.name;
                    weaponImage.sprite = items.artwork;
                    inventory.actualEquipedWeapon = items;
                    items.equiped = true;

                    inventory.ActivateTheEquipped((Scripteable_Weapon)inventory.actualWeapons[i]);
                }
                else
                {
                    inventory.DeactivateTheEquipped(actual.name);
                    actual.equiped = false;
                    print("Te vas a equipar: " + items.name);
                    GeneralStats actualCharacter = GameObject.Find(uIMovement.charSelected).GetComponent<GeneralStats>();
                    actualCharacter.weaponequiped = items.name;
                    inventory.DeactivateTheEquipped(equipedWeapon);
                    equipedWeapon = items.name;
                    weaponImage.sprite = items.artwork;
                    inventory.actualEquipedWeapon = items;
                    items.equiped = true;
                    inventory.ActivateTheEquipped((Scripteable_Weapon)inventory.actualWeapons[i]);
                }
                gameSave.SaveGame();
            }
        }
    }
    private void CancelEquip()
    {

    }
    
    
    private void DesSelectObject()
    {

    }
}
