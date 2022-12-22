using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyGetAttack : MonoBehaviour
{
    [Header("UI")]
    public GameObject hpText;
    public GameObject levelText;

    private GameObject xrOrigin;
    private Animator animator;
    private float initialEnemyHp;

    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GameObject.Find("XR Origin");
        animator = GetComponent<Animator>();
        initialEnemyHp = PlayerPrefs.GetFloat("enemyHp");

        SetStats();
        hpText.GetComponent<TextMeshPro>().text = "" + (int)PlayerPrefs.GetFloat("enemyHp");

        if (PlayerPrefs.GetInt("weaponLevel") < 1)
            xrOrigin.GetComponent<InfoMessage>().DisplayInfo("info", "Найдите оружие, чтобы вступить в бой");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
            AttackEnemy();
    }

    public void AttackEnemy()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && PlayerPrefs.GetFloat("enemyHp") > 0)
        {            
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (PlayerPrefs.GetFloat("userHp") > 0)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.GetComponent<EnemyStats>())
                    {
                        if (PlayerPrefs.GetInt("weaponLevel") > 0)
                        {
                            TakeDamage(Random.Range(10, PlayerPrefs.GetInt("userAttack")));
                            PlayerPrefs.SetInt("battle", 1);
                        }
                        else
                        {
                            xrOrigin.GetComponent<InfoMessage>().DisplayInfo("text", "У вас нет оружия");
                            PlayerPrefs.SetInt("battle", 0);
                        }
                    }
                }
            }
            else xrOrigin.GetComponent<InfoMessage>().DisplayInfo("text", "Необходимо восстановить уровень HР");                
        }
    }

    public void TakeDamage(float damage)
    {
        float updatedEnemyHp = PlayerPrefs.GetFloat("enemyHp") - damage;
        PlayerPrefs.SetFloat("enemyHp", updatedEnemyHp);

        hpText.GetComponent<TextMeshPro>().text = "↓ " + (int)PlayerPrefs.GetFloat("enemyHp");
        animator.SetTrigger("damage");
        SetStats();

        if (PlayerPrefs.GetFloat("enemyHp") <= 0)
        {
            PlayerPrefs.SetInt("battle", 0);
            PlayerPrefs.SetFloat("userXpGained", initialEnemyHp / 2f);
            animator.SetTrigger("dead");

            StartCoroutine(DeadEnemy(10));
        }
    }

    public void LevelUpEnemy()
    {
        animator.SetTrigger("dead");

        int updatedLevel = PlayerPrefs.GetInt("enemyLevel") + 1;
        PlayerPrefs.SetInt("enemyLevel", updatedLevel);

        float updatedEnemyAttack = PlayerPrefs.GetFloat("enemyAttack") * 1.4f;
        PlayerPrefs.SetFloat("enemyAttack", updatedEnemyAttack);

        PlayerPrefs.SetFloat("enemyHp", initialEnemyHp);
        float updatedEnemyHp = PlayerPrefs.GetFloat("enemyHp") * 1.5f;
        PlayerPrefs.SetFloat("enemyHp", updatedEnemyHp);

        initialEnemyHp = PlayerPrefs.GetFloat("enemyHp");

        SetStats();
        hpText.GetComponent<TextMeshPro>().text = "" + (int)PlayerPrefs.GetFloat("enemyHp");
    }

    public void SetStats()
    {
        levelText.GetComponent<TextMeshPro>().text = "lvl " + PlayerPrefs.GetInt("enemyLevel");
    }

    IEnumerator DeadEnemy(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        LevelUpEnemy();
    }
}
