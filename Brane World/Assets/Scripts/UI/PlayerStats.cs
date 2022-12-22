using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("userLevel") == 0)
        {
            PlayerPrefs.SetInt("userAttack", 30);
            PlayerPrefs.SetInt("userDefence", 50);
        }
        
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        CorrectFontSize(PlayerPrefs.GetInt("userAttack"), PlayerPrefs.GetInt("userDefence"));
    }

    public void IncreaseStats(int level)
    {
        int updatedAttack = PlayerPrefs.GetInt("userAttack") + (int)Mathf.Floor((PlayerPrefs.GetInt("userAttack") * 0.1f) * (100 - level) * 0.015f);
        PlayerPrefs.SetInt("userAttack", updatedAttack);
        
        int updatedDefence = PlayerPrefs.GetInt("userDefence") + (int)Mathf.Floor((PlayerPrefs.GetInt("userDefence") * 0.1f) * (100 - level) * 0.01f);
        PlayerPrefs.SetInt("userDefence", updatedDefence);

        SetStats();
    }

    public void AddWeaponStats(int attack, int defence)
    {
        int updatedAttack = PlayerPrefs.GetInt("userAttack") + attack;
        PlayerPrefs.SetInt("userAttack", updatedAttack);

        int updatedDefence = PlayerPrefs.GetInt("userDefence") + defence;
        PlayerPrefs.SetInt("userDefence", updatedDefence);

        SetStats();
    }

    private void SetFontSize(int fontSize)
    {
        attackText.fontSize = fontSize;
        defenceText.fontSize = fontSize;
    }

    private void CorrectFontSize(int attack, int defence)
    {
        if (attack > 99 || defence > 99)
            SetFontSize(32);

        else if (attack > 999 || defence > 999)
            SetFontSize(26);

        else if (attack > 9999 || defence > 9999)
            SetFontSize(22);
    }

    public void SetStats()
    {
        attackText.text = "" + PlayerPrefs.GetInt("userAttack");
        defenceText.text = "" + PlayerPrefs.GetInt("userDefence");
    }
}
