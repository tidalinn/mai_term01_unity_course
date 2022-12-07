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

    // Update is called once per frame
    void Update() {
        userName.text = savedName;
    }

    public void SetName()
    {
        savedName = inputText.text;
    }
}
