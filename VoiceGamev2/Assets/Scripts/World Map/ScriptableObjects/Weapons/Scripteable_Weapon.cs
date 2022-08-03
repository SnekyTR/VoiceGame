using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapons")]
public class Scripteable_Weapon : ScriptableObject
{
    public int id;
    public string name;
    public string weaponType;
    public string description;

    public Sprite artwork;
    public bool equiped;
    public GameObject weapon;
}