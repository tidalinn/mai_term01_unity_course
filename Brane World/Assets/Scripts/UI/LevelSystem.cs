using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    [Header("Level system")]
    public float currentXp = 0;
    public float requiredXp = 130;

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
        frontXpBar.fillAmount = currentXp / requiredXp;
        requiredXp = CalculateRequiredXp();

        if (PlayerPrefs.GetInt("userLevel") == 0)
            PlayerPrefs.SetInt("userLevel", 1);
        
        levelText.text = AddZeroToLevel(PlayerPrefs.GetInt("userLevel"));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();

        if (Input.GetKeyDown(KeyCode.Equals))
            GainExperienceScalable(20, PlayerPrefs.GetInt("userLevel"));

        if (currentXp > requiredXp)
            LevelUp();
            CorrectFontSize(PlayerPrefs.GetInt("userLevel"));
        
        levelText.text = AddZeroToLevel(PlayerPrefs.GetInt("userLevel"));
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float frontXp = frontXpBar.fillAmount;

        if (frontXp < xpFraction)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer < 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;

                frontXpBar.fillAmount = Mathf.Lerp(frontXp, xpFraction, percentComplete);
            }
        }
    }

    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < PlayerPrefs.GetInt("userLevel"))
        { 
            float multiplier = 1 + (PlayerPrefs.GetInt("userLevel") - passedLevel) * 0.1f;
            currentXp += xpGained * multiplier;
        }
        else 
        {
            currentXp += xpGained;
        }

        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        int newLevel = PlayerPrefs.GetInt("userLevel") + 1;
        PlayerPrefs.SetInt("userLevel", newLevel);

        frontXpBar.fillAmount = 0f;
        
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        GetComponent<PlayerHealth>().IncreaseHealth(PlayerPrefs.GetInt("userLevel"));
        GetComponent<PlayerStats>().IncreaseStats(PlayerPrefs.GetInt("userLevel"));
        requiredXp = CalculateRequiredXp();

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
