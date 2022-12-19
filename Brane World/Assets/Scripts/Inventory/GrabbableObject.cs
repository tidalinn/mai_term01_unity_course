using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [Header("UI")]
    public GameObject equippedItem;
    public LayerMask itemLayer;
    
    private bool isInstantiated;
    private List<GameObject> equipped;

    void Start()
    {
        isInstantiated = false;
    }

    void Update() 
    {
        GameObject weapon = GameObject.Find("Heavy Sword");
        {
            if (equippedItem)
            {
                equipped = GetChildren();
                /*
                if (equipped.Count > 1)
                {
                    Destroy(equipped[0]);
                }*/

                if (!isInstantiated)
                {
                    GameObject userWeapon = Instantiate(weapon, equippedItem.transform);
                    /*
                    userWeapon.transform.position = new Vector3(0, 0.55f, 0);
                    userWeapon.transform.rotation = new Quaternion(0, 0.9f, 0, 1);
                    userWeapon.transform.localScale = equippedItem.transform.localScale / 2;
                    userWeapon.layer = itemLayer.value;

                    AddBonuses();
                    */
                }
            }
        }
    }
    public void Grab(GameObject weapon) {}

    /*
    public void Grab(GameObject weapon) 
    {
        if (PlayerPrefs.GetInt("userLevel") >= weapon.GetComponent<PinInfo>().item.level)
        {
            if (equippedItem)
            {
                equipped = GetChildren();

                if (equipped.Count > 1)
                {
                    Destroy(equipped[0]);
                }

                if (!isInstantiated)
                {
                    GameObject userWeapon = Instantiate(weapon, equippedItem.transform);
                    userWeapon.transform.position = new Vector3(0, 0.55f, 0);
                    userWeapon.transform.rotation = new Quaternion(0, 0.9f, 0, 1);
                    userWeapon.transform.localScale = equippedItem.transform.localScale / 2;
                    userWeapon.layer = itemLayer.value;

                    AddBonuses();
                }
            }
        }
        else
        {
            GameObject.Find("XR Origin").GetComponent<InfoMessage>().DisplayInfo("text", "Недостаточный уровень");
        }
    }
    */

    private void AddBonuses()
    {

    }

    private void RemoveBonuses()
    {

    }

    private List<GameObject> GetChildren()
    {
        Transform[] children = equippedItem.GetComponentsInChildren<Transform>();
        equipped = new List<GameObject>();

        foreach (Transform child in children)
        { 
            equipped.Add(child.gameObject);
        }
        equipped.RemoveAt(0);

        return equipped;
    }
}
