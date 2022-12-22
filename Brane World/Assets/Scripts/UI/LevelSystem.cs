using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXpBar;
    public TextMeshProUGUI levelText;

    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionalMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    // Start is called before the first frame update
    void Start()
    {   
        if (PlayerPrefs.GetInt("userLevel") == 0)
        {
            PlayerPrefs.SetInt("userLevel", 1);
            PlayerPrefs.SetFloat("userXp", 0f);
            PlayerPrefs.SetFloat("requiredXp", 130f);
            PlayerPrefs.SetFloat("frontXpBar", 0f);
        }
        
        frontXpBar.fillAmount = PlayerPrefs.GetFloat("frontXpBar"); 
        PlayerPrefs.SetFloat("requiredXp", CalculateRequiredXp());  
        levelText.text = AddZeroToLevel(PlayerPrefs.GetInt("userLevel"));
    }

    // Update is called once per frame
    void Update()
    {   
        UpdateXpUI();

        if (PlayerPrefs.GetFloat("userXpGained") > 0)
            GainExperienceScalable(PlayerPrefs.GetFloat("userXpGained"), PlayerPrefs.GetInt("userLevel"));

        if (PlayerPrefs.GetFloat("userXp") > PlayerPrefs.GetFloat("requiredXp"))
            LevelUp();
            CorrectFontSize(PlayerPrefs.GetInt("userLevel"));

        levelText.text = AddZeroToLevel(PlayerPrefs.GetInt("userLevel"));
    }

    public void UpdateXpUI()
    {
        float xpFraction = PlayerPrefs.GetFloat("userXp") / PlayerPrefs.GetFloat("requiredXp");
        float frontXp = frontXpBar.fillAmount;

        if (frontXp < xpFraction)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer < 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                
                frontXpBar.fillAmount = Mathf.Lerp(frontXp, xpFraction, percentComplete);
                PlayerPrefs.SetFloat("frontXpBar", frontXpBar.fillAmount);
            }
        }
    }

    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        float updatedUserXp;

        if (passedLevel < PlayerPrefs.GetInt("userLevel"))
        { 
            float multiplier = 1 + (PlayerPrefs.GetInt("userLevel") - passedLevel) * 0.1f;
            updatedUserXp = PlayerPrefs.GetFloat("userXp") + xpGained * multiplier;
        }
        else 
        {
            updatedUserXp = PlayerPrefs.GetFloat("userXp") + xpGained;
        }

        GetComponent<InfoMessage>().DisplayInfo("info", "Вы одолели " + PlayerPrefs.GetString("enemyName") + "\nXP +" + Mathf.RoundToInt(xpGained));

        PlayerPrefs.SetFloat("userXp", updatedUserXp);
        PlayerPrefs.SetFloat("userXpGained", 0);

        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        int newLevel = PlayerPrefs.GetInt("userLevel") + 1;
        PlayerPrefs.SetInt("userLevel", newLevel);

        PlayerPrefs.SetFloat("frontXpBar", 0f);
        frontXpBar.fillAmount = PlayerPrefs.GetFloat("frontXpBar");
        
        float updatedUserXp = PlayerPrefs.GetFloat("userXp") - PlayerPrefs.GetFloat("requiredXp");
        PlayerPrefs.SetFloat("userXp", updatedUserXp);
        PlayerPrefs.SetFloat("requiredXp", CalculateRequiredXp());

        GetComponent<PlayerHealth>().IncreaseHealth(PlayerPrefs.GetInt("userLevel"));
        GetComponent<PlayerStats>().IncreaseStats(PlayerPrefs.GetInt("userLevel"));

        levelText.text = AddZeroToLevel(PlayerPrefs.GetInt("userLevel"));

        string infoText = "Атака ↑ " + (int)PlayerPrefs.GetInt("userAttack") + "\nЗащита ↑ " + (int)PlayerPrefs.GetInt("userDefence") + "\nXP ↑ " + (int)PlayerPrefs.GetFloat("maxHp") + "\nHP ↑ " + (int)PlayerPrefs.GetFloat("userHp");
        GetComponent<InfoMessage>().DisplayInfo("info", infoText);

        GetComponent<PlayerStats>().AddWeaponStats(PlayerPrefs.GetInt("weaponAttack"), PlayerPrefs.GetInt("weaponDefence"));
        GetComponent<PlayerHealth>().AddWeaponStats(PlayerPrefs.GetFloat("weaponXp"), PlayerPrefs.GetFloat("weaponHp"));
        GetComponent<PlayerStats>().SetStats();
    }

    private int CalculateRequiredXp()
    {
        int solverForRequiredXp = 0;

        for (int levelCycle = 1; levelCycle <= PlayerPrefs.GetInt("userLevel"); levelCycle++)
        {
            solverForRequiredXp += (int)Mathf.Floor(levelCycle + additionalMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solverForRequiredXp / 4;
    }

    private string AddZeroToLevel(int level)
    {
        if (level < 10)
            return "0" + level;
        else
            return "" + level;
    }

    private void SetFontSize(int fontSize)
    {
        levelText.fontSize = fontSize;
        levelText.characterSpacing = -8;
    }

    private void CorrectFontSize(int level)
    {
        if (level > 99)
        {
            SetFontSize(40);
        }
    }
}
