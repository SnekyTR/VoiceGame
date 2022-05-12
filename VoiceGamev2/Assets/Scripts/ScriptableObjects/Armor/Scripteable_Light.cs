using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armors", menuName = "Armor/Light")]
public class Scripteable_Light : ScriptableObject
{
    public string name;
    public string description;

    public int health;
    public int durability;
    public int maxdurability;
    public GameObject armor;
    public Sprite armorImage;
}
