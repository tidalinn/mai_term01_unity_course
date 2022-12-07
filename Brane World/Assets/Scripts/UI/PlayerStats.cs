using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int attack = 50;
    public int defence = 80;

    [Header("UI")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;

    // Start is called before the first frame update
    void Start()
    {
        attackText.text = "" + attack;
        defenceText.text = "" + defence;
    }

    // Update is called once per frame
    void Update()
    {
        CorrectFontSize(attack, defence);
    }

    public void IncreaseStats(int level)
    {
        attack += (int)Mathf.Floor((attack * 0.1f) * (100 - level) * 0.015f);
        defence += (int)Mathf.Floor((defence * 0.1f) * (100 - level) * 0.01f);

        attackText.text = "" + attack;
        defenceText.text = "" + defence;
    }

    private void SetFontSize(int fontSize)
    {
        attackText.fontSize = fontSize;
        defenceText.fontSize = fontSize;
    }

    private void CorrectFontSize(int attack, int defence)
    {
        if (attack > 99 || defence > 99)
        {
            SetFontSize(32);
        }

        else if (attack > 999 || defence > 999)
        {
            SetFontSize(26);
        }

        else if (attack > 9999 || defence > 9999)
        {
            SetFontSize(22);
        }
    }
}
