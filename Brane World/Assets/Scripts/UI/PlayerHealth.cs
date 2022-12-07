using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    private float health;
    private float lerpTimer;

    [Header("Damage")]
    public GameObject damageTextPrefab;
    public GameObject parentObject;

    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

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
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;

        if (fillBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = new Color(1f, 0f, 0f, 0.185f);
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }

        if (fillFront < healthFraction)
        {
            backHealthBar.color = new Color(0.5f, 0.5f, 0.5f, 0.185f);
            backHealthBar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;

            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            lerpTimer = 0f;

            GameObject DamageTextInstance = Instantiate(damageTextPrefab, parentObject.transform);
            DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Вас атаковали -" + damage);
        }
        else
        {
            GetComponent<InfoMessage>().printInfo("Вы были убиты");
        }
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    public void IncreaseHealth(int level)
    {
        maxHealth += ((health * 0.01f) * (100 - level) * 0.1f);
        health = maxHealth;
    }
}
