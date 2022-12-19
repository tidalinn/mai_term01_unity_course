using UnityEngine;

public enum EquipmentType
{
    None,
    Sword,
    Blade,
    Axe,
    Halberd,
    Mace
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public GameObject itemPrefab;
    public int level;
    public string info;

    [Header("Bonuses")]
    public int attack;
    public int defence;
    public int xp;
    public int hp;

    [Header("Type")]
    public EquipmentType EquipmentType;
}
