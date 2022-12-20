using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpItem : MonoBehaviour
{
    [Header("UI")]
    public GameObject equippedItem;
    public string itemLayerName;
    public GameObject noWeapon;

    private bool isInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        isInstantiated = false;
        noWeapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject touchedObject = hit.transform.gameObject;

                    if (touchedObject.GetComponent<GrabbableObject>())
                    {
                        Grab(touchedObject);
                    }
                }
            } 
        }
    }

    private void Grab(GameObject weapon)
    {
        if (PlayerPrefs.GetInt("userLevel") >= weapon.GetComponent<GrabbableObject>().GetLevel())
        {
            if (equippedItem.transform.childCount > 0) {
                weapon.GetComponent<GrabbableObject>().RemoveBonuses();

                foreach (Transform item in equippedItem.transform) 
                {
                    DestroyImmediate(item.gameObject);
                    isInstantiated = false;

                    noWeapon.SetActive(true);
                }
            }

            if (equippedItem.transform.childCount < 1 && !isInstantiated)
            {
                weapon.GetComponent<GrabbableObject>().AddBonuses();
                InstantiateItem(weapon);
                isInstantiated = true;

                noWeapon.SetActive(false);
                GetComponent<EquippedWeapon>().SetWeapon(weapon);

                PlayerPrefs.SetString("userHasWeapon", "true");
            }
        }
        else
        {
            GetComponent<InfoMessage>().DisplayInfo("text", "Недостаточный уровень");
        }
    }

    private void InstantiateItem(GameObject weapon)
    {
        GameObject userWeapon = Instantiate(weapon.GetComponent<PinInfo>().item.itemPrefab, equippedItem.transform);
        userWeapon.layer = LayerMask.NameToLayer(itemLayerName);
        userWeapon.transform.position = equippedItem.transform.position;
        userWeapon.transform.Rotate(0, 90f, 0);
        userWeapon.transform.localScale = userWeapon.transform.localScale / 2.5f;
    }
}
