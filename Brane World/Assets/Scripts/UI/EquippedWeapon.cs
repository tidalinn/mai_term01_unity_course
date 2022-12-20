using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquippedWeapon : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponPowerValue;
    public TextMeshProUGUI weaponInfo;

    // Update is called once per frame
    public void SetWeapon(GameObject weapon)
    {
        Item item = weapon.GetComponent<PinInfo>().item;

        weaponName.text = item.itemName;
        weaponPowerValue.text = "" + item.attack;
        weaponInfo.text = "Защита: " + item.defence + "| XP: " + item.xp + "| HP: " + item.hp;
    }
}
