using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpItem : MonoBehaviour
{
    private GameObject equippedItem;
    private bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        equippedItem = GetComponent<EquippedWeapon>().equippedItem;
        isGrabbed = false;
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
                        Grab(touchedObject);
                }
            } 
        }
    }

    private void Grab(GameObject weapon)
    {
        if (PlayerPrefs.GetInt("userLevel") >= weapon.GetComponent<GrabbableObject>().GetLevel())
        {
            if (equippedItem.transform.childCount > 0) 
            {
                weapon.GetComponent<GrabbableObject>().RemoveBonuses();

                foreach (Transform item in equippedItem.transform) 
                {
                    DestroyImmediate(item.gameObject);
                    isGrabbed = false;
                }
            }

            if (equippedItem.transform.childCount < 1 && !isGrabbed)
            {
                weapon.GetComponent<GrabbableObject>().AddBonuses();
                GetComponent<EquippedWeapon>().SetWeapon(weapon);
                isGrabbed = true;
            }
        }
        else
        {
            GetComponent<InfoMessage>().DisplayInfo("text", "Недостаточный уровень");
        }
    }
}
