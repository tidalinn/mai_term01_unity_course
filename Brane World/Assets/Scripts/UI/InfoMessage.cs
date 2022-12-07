using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoMessage : MonoBehaviour
{
    [Header("Info")]
    public Image infoMessagePrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void printInfo(string info)
    {
        infoMessagePrefab.gameObject.SetActive(true);
        infoMessagePrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(info);
    }
}