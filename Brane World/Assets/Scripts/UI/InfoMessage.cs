using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoMessage : MonoBehaviour
{
    [Header("Display Info")]
    public GameObject infoMessagePrefab;
    public GameObject textMessagePrefab;

    public void DisplayInfo(string type, string info, int delay = 2)
    {
        switch (type)
        {
            case "info":
                infoMessagePrefab.SetActive(true);
                infoMessagePrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(info);
                StartCoroutine(HideAfterDelay(infoMessagePrefab, delay + 1));
                break;

            case "text":
                textMessagePrefab.SetActive(true);
                textMessagePrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(info);
                StartCoroutine(HideAfterDelay(textMessagePrefab, delay));
                break;
                
            default:
                break;
        }
    }

    IEnumerator HideAfterDelay(GameObject message, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        message.SetActive(false);
    }
}