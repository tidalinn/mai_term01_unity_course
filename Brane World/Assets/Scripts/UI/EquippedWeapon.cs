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
    [Space] 
    public GameObject equippedItem;
    public string itemLayerName;
    public GameObject noWeapon;

    // Start is called before the first frame update
    void Start()
    {
        noWeapon.SetActive(true);

        if (PlayerPrefs.GetInt("weaponLevel") > 0 && equippedItem.transform.childCount < 1)
        {
            SetStats();
            GetComponent<InfoMessage>().DisplayInfo("text", "Оружие будет загружено вместе со сценой");
        }
    }

    // Update is called once per frame
    void Update()
    {            
        if (PlayerPrefs.GetInt("weaponLevel") > 0 && equippedItem.transform.childCount < 1)
        {
            GameObject weapon = GameObject.Find(PlayerPrefs.GetString("weaponPrefabName"));
            Equip(weapon, equippedItem, itemLayerName);
            
            GetComponent<PlayerStats>().SetStats();
            SetStats();

            noWeapon.SetActive(false);
        }
    }

    public void SetWeapon(GameObject weapon)
    {
        Equip(weapon, equippedItem, itemLayerName);

        Item item = weapon.GetComponent<PinInfo>().item;

        PlayerPrefs.SetString("weaponName", item.itemName);
        PlayerPrefs.SetString("weaponPrefabName", weapon.transform.name);
        PlayerPrefs.SetInt("weaponLevel", item.level);
        PlayerPrefs.SetInt("weaponAttack", item.attack);
        PlayerPrefs.SetInt("weaponDefence", item.defence);
        PlayerPrefs.SetInt("weaponXp", item.xp);
        PlayerPrefs.SetInt("weaponHp", item.hp);

        SetStats();
        noWeapon.SetActive(false);
    }

    public void Equip(GameObject weapon, GameObject parentItem, string layerName)
    {
        GameObject userWeapon = Instantiate(weapon.GetComponent<PinInfo>().item.itemPrefab, parentItem.transform);
        userWeapon.layer = LayerMask.NameToLayer(layerName);
        userWeapon.transform.position = parentItem.transform.position;
        userWeapon.transform.Rotate(0, -90f, 0);
        userWeapon.transform.localScale = userWeapon.transform.localScale * 2.2f;
    }

    public void SetStats()
    {
        weaponName.text = PlayerPrefs.GetString("weaponName");
        weaponPowerValue.text = "" + PlayerPrefs.GetInt("weaponAttack");
        weaponInfo.text = "Уровень: " + PlayerPrefs.GetInt("weaponLevel") + "   Защита: " + PlayerPrefs.GetInt("weaponDefence") + "\nXP: " + PlayerPrefs.GetInt("weaponXp") + "   HP: " + PlayerPrefs.GetInt("weaponHp");
    }
}
