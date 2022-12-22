using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [Header("UI")]
    public string enemyName;

    [Header("Combat")]
    public float attackCD = 2f;
    public float attackRange = 3.5f;

    private GameObject xrOrigin;
    private Animator animator;
    private float timePassed;
    private float initialEnemyHp;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("enemyLevel") == 0)
        {
            PlayerPrefs.SetInt("enemyLevel", 1);
            PlayerPrefs.SetInt("enemyAttack", 10);
            PlayerPrefs.SetFloat("enemyHp", 50);
        }

        PlayerPrefs.SetString("enemyName", enemyName);
        PlayerPrefs.SetFloat("userXpGained", 0);

        xrOrigin = GameObject.Find("XR Origin");
        animator = GetComponent<Animator>();

        initialEnemyHp = PlayerPrefs.GetFloat("enemyHp");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (timePassed >= attackCD && PlayerPrefs.GetFloat("enemyHp") > 0)
            {
                if (Vector3.Distance(Camera.main.transform.position, transform.position) <= attackRange)
                {
                    AttackUser();
                    PlayerPrefs.SetInt("battle", 1);
                }
            }

            timePassed += Time.deltaTime;
            transform.LookAt(Camera.main.transform);
        }
    }

    public void AttackUser()
    {
        if (PlayerPrefs.GetFloat("userHp") > 0)
        {
            animator.SetTrigger("attack");
            xrOrigin.GetComponent<PlayerHealth>().TakeDamage(Random.Range(10, PlayerPrefs.GetInt("enemyAttack")));
            
            timePassed = 0;
            PlayerPrefs.SetInt("battle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("battle", 0);
            xrOrigin.GetComponent<InfoMessage>().DisplayInfo("info", "Вас убил " + PlayerPrefs.GetString("enemyName"));
        }
    }
}
