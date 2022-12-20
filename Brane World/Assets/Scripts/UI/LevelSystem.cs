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
        
        if (PlayerPrefs.GetString("enemyDead") == "true")
        {
            float xpGained = PlayerPrefs.GetInt("enemyAttack");
            GainExperienceScalable(xpGained, PlayerPrefs.GetInt("userLevel"));

            string textInfo = "Вы одолели " + PlayerPrefs.GetInt("enemyName") + "\nXP +" + xpGained;
            GetComponent<InfoMessage>().DisplayInfo("text", textInfo);
        }

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

        PlayerPrefs.SetFloat("userXp", updatedUserXp);

        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        int newLevel = PlayerPrefs.GetInt("userLevel") + 1;
        PlayerPrefs.SetInt("userLevel", newLevel);

        PlayerPrefs.SetFloat("frontXpBar", 0f);
        frontXpBar.fillAmount = PlayerPrefs.GetFloat("frontXpBar");
        
        float updatedUserXp = Mathf.RoundToInt(PlayerPrefs.GetFloat("userXp") - PlayerPrefs.GetFloat("requiredXp"));
        PlayerPrefs.SetFloat("userXp", updatedUserXp);

        GetComponent<PlayerHealth>().IncreaseHealth(PlayerPrefs.GetInt("userLevel"));
        GetComponent<PlayerStats>().IncreaseStats(PlayerPrefs.GetInt("userLevel"));
        PlayerPrefs.SetFloat("requiredXp", CalculateRequiredXp());

        levelText.text = AddZeroToLevel(PlayerPrefs.GetInt("userLevel"));
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
