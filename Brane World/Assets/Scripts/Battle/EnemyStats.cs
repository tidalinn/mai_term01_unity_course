using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyStats : MonoBehaviour
{
    [Header("UI")]
    public string enemyName;
    public TextMeshPro levelText;
    public TextMeshPro damageText;

    private Animator animator;

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
        levelText.text = "lvl " + PlayerPrefs.GetString("enemyLevel");

        animator = GetComponent<Animator>();

        animator.runtimeAnimatorController = Resources.Load("1H@CombatIdle") as RuntimeAnimatorController;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject enemy = hit.transform.gameObject;
                    GameObject user = GameObject.Find("XR Origin");

                    if (enemy.GetComponent<EnemyStats>())
                    {
                        if (PlayerPrefs.GetString("userHasWeapon") == "true")
                        {
                            PlayerPrefs.SetFloat("enemyDamage", Random.Range(10, PlayerPrefs.GetInt("userAttack")));
                            TakeDamage(PlayerPrefs.GetFloat("enemyDamage"));
                            
                            AddDelay(Random.Range(0, 2));
                            PlayerPrefs.SetFloat("userDamage", Random.Range(10, PlayerPrefs.GetInt("enemyAttack")));
                            user.GetComponent<PlayerHealth>().TakeDamage(PlayerPrefs.GetFloat("userDamage"));
                        }
                        else
                        {
                            user.GetComponent<InfoMessage>().DisplayInfo("text", "У вас нет оружия");
                        }
                    }
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage < PlayerPrefs.GetFloat("enemyHp"))
        {
            damageText.text = "-" + (int)damage;

            if (damage > 20 && damage < 30)
                animator.runtimeAnimatorController = Resources.Load("1H@RHAttacks") as RuntimeAnimatorController;

            else if (damage < 2)
                animator.runtimeAnimatorController = Resources.Load("1H@ShieldAttacks") as RuntimeAnimatorController;

            float updatedHp = PlayerPrefs.GetFloat("enemyHp") - damage;
            PlayerPrefs.SetFloat("enemyHp", updatedHp);
            Debug.Log(damage + " " + PlayerPrefs.GetFloat("enemyHp"));
        }
        else 
        {
            PlayerPrefs.SetString("enemyDead", "true");
            animator.runtimeAnimatorController = Resources.Load("MW@Death01") as RuntimeAnimatorController;

            PlayerPrefs.SetInt("enemyLevel", 2);
            PlayerPrefs.SetInt("enemyAttack", PlayerPrefs.GetInt("enemyAttack") + 20);
            PlayerPrefs.SetFloat("enemyHp", PlayerPrefs.GetFloat("enemyHp") + 30);

            levelText.text = "" + PlayerPrefs.GetString("enemyName");

            AddDelay(5);
            animator.runtimeAnimatorController = Resources.Load("1H@CombatIdle") as RuntimeAnimatorController;
        }
    }

    IEnumerator AddDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
