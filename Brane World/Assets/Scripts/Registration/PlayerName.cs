using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerName : MonoBehaviour
{
    public string savedName;

    [Header("UI")]
    public Text inputText;
    public TextMeshProUGUI userName;

    [Header("Switch Scene")]
    public GameObject sceneSwitcherPrefab;
    public string sceneName;

    // Update is called once per frame
    void Update() {
        userName.text = savedName;

        if (userName.text.Length > 12)
        {
            userName.fontSize = 18;
        }
    }

    public void SetName()
    {
        if (inputText.text.Length >= 1 && inputText.text.Length <= 20)
        {
            savedName = inputText.text;
            sceneSwitcherPrefab.GetComponent<SceneSwitcher>().SwitchScene(sceneName);
        }
    }
}
