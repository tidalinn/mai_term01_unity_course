using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    private float lerpTimer;

    [Header("Damage")]
    public GameObject damageTextPrefab;
    public GameObject parentObject;

    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("userLevel") == 1)
            PlayerPrefs.SetFloat("maxHp", 100f);

        frontHealthBar.fillAmount = PlayerPrefs.GetFloat("frontHealthBar");
        backHealthBar.fillAmount = PlayerPrefs.GetFloat("backHealthBar");

        PlayerPrefs.SetFloat("userHp", PlayerPrefs.GetFloat("maxHp"));
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("userHp", Mathf.Clamp(PlayerPrefs.GetFloat("userHp"), 0, PlayerPrefs.GetFloat("maxHp")));
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(Random.Range(5, 10));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }

    public void UpdateHealthUI()
    {
        float healthFraction = PlayerPrefs.GetFloat("userHp") / PlayerPrefs.GetFloat("maxHp");
        
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        
        // reduce HP
        if (fillBack > healthFraction)
        {
            if (PlayerPrefs.GetFloat("backHealthBar") < healthFraction)
                frontHealthBar.fillAmount = PlayerPrefs.GetFloat("backHealthBar");
            else
                frontHealthBar.fillAmount = healthFraction;

            backHealthBar.color = new Color(1f, 0f, 0f, 0.185f);
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;

            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }

        // recover HP
        if (fillFront < healthFraction)
        {
            if (PlayerPrefs.GetFloat("frontHealthBar") > healthFraction)
                frontHealthBar.fillAmount = PlayerPrefs.GetFloat("frontHealthBar");
            else
                frontHealthBar.fillAmount = healthFraction;

            backHealthBar.color = new Color(0.5f, 0.5f, 0.5f, 0.185f);
            backHealthBar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;

            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }

        PlayerPrefs.SetFloat("frontHealthBar", frontHealthBar.fillAmount);
        PlayerPrefs.SetFloat("backHealthBar", backHealthBar.fillAmount);
    }

    public void TakeDamage(float damage)
    {
        if (PlayerPrefs.GetFloat("userHp") > 0)
        {
            float updatedHealth = PlayerPrefs.GetFloat("userHp") - damage;
            PlayerPrefs.SetFloat("userHp", updatedHealth);
            lerpTimer = 0f;

            GameObject DamageTextInstance = Instantiate(damageTextPrefab, parentObject.transform);
            DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Вас атаковали -" + damage);
        }
        else
        {
            GetComponent<InfoMessage>().DisplayInfo("info", "Вы были убиты");
        }
    }

    public void RestoreHealth(float healAmount)
    {
        float updatedUserHp = PlayerPrefs.GetFloat("userHp") + healAmount;
        PlayerPrefs.SetFloat("userHp", updatedUserHp);
        lerpTimer = 0f;
    }

    public void IncreaseHealth(int level)
    {
        float updatedMaxHp = PlayerPrefs.GetFloat("maxHp") + ((PlayerPrefs.GetFloat("userHp") * 0.01f) * (100 - level) * 0.1f);
        PlayerPrefs.SetFloat("userHp", updatedMaxHp);
    }
}
