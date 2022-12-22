using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private GameObject xrOrigin;
    private Item item;

    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GameObject.Find("XR Origin");
        item = GetComponent<PinInfo>().item;
    }

    public int GetLevel()
    {
        return GetComponent<PinInfo>().item.level;
    }

    public void AddBonuses()
    {
        xrOrigin.GetComponent<PlayerStats>().AddWeaponStats(item.attack, item.defence);
        xrOrigin.GetComponent<PlayerHealth>().AddWeaponStats(item.xp, item.hp);

        DisplayStats("+");
    }

    public void RemoveBonuses()
    {
        xrOrigin.GetComponent<PlayerStats>().AddWeaponStats(-item.attack, -item.defence);
        xrOrigin.GetComponent<PlayerHealth>().AddWeaponStats(-item.xp, -item.hp);

        PlayerPrefs.SetString("weaponName", null);
        PlayerPrefs.SetString("weaponPrefabName", null);
        PlayerPrefs.SetInt("weaponLevel", 0);
        PlayerPrefs.SetInt("weaponAttack", 0);
        PlayerPrefs.SetInt("weaponDefence", 0);
        PlayerPrefs.SetInt("weaponXp", 0);
        PlayerPrefs.SetInt("weaponHp", 0);
    }

    public void DisplayStats(string operation)
    {
        if (PlayerPrefs.GetInt("weaponLevel") == 0)
        {
            string infoText = "Атака " + operation + item.attack + "\nЗащита " + operation + item.defence + "\nXP " + operation + item.xp + "\nHP " + operation + item.hp;
            xrOrigin.GetComponent<InfoMessage>().DisplayInfo("info", infoText);
        }
    }
}
