using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armors", menuName = "Armor/Heavy")]
public class Scripteable_Heavy : ScriptableObject
{
    public string name;
    public string description;

    public int health;
    public int durability;
    public int maxdurability;
    public GameObject armor;
    public Sprite armorImage;
}
