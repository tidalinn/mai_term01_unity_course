using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public int GetLevel()
    {
        return GetComponent<PinInfo>().item.level;
    }

    public void AddBonuses()
    {
        GameObject xrOrigin = GameObject.Find("XR Origin");
        Item item = GetComponent<PinInfo>().item;

        xrOrigin.GetComponent<PlayerStats>().AddWeaponStats(item.attack, item.defence);

        DisplayStats("+");
    }

    public void RemoveBonuses()
    {
        GameObject xrOrigin = GameObject.Find("XR Origin");
        Item item = GetComponent<PinInfo>().item;

        xrOrigin.GetComponent<PlayerStats>().AddWeaponStats(-item.attack, -item.defence);
    }

    public void DisplayStats(string operation)
    {
        Item item = GetComponent<PinInfo>().item;

        string infoText = "Атака " + operation + item.attack + "\nЗащита " + operation + item.defence + "\nXP " + operation + item.xp + "\nHP " + operation + item.hp;

        GameObject.Find("XR Origin").GetComponent<InfoMessage>().DisplayInfo("info", infoText);
    }
}
