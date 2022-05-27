using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapons/Axe")]
public class Scripteable_Axe : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int damage;
    public float range;
    public int durability;
    public int maxDurability;
    public GameObject weapon;
    public Sprite weaponImage;
}
