using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerName : MonoBehaviour
{
    [Header("UI")]
    public Text inputText;
    public TextMeshProUGUI userName;
    public GameObject setPlayerNameForm;

    [Header("Switch Scene")]
    public GameObject sceneSwitcherPrefab;
    public string sceneName;

    // Update is called once per frame
    void Update() {
        userName.text = PlayerPrefs.GetString("userName");;

        if (userName.text.Length > 12)
        {
            userName.fontSize = 18;
        }
    }

    public void SetName()
    {
        if (inputText.text.Length >= 1 && inputText.text.Length <= 20)
        {
            PlayerPrefs.DeleteKey("userName");
            PlayerPrefs.SetString("userName", inputText.text);

            if (SceneManager.GetActiveScene().name != sceneName)
                sceneSwitcherPrefab.GetComponent<SceneSwitcher>().SwitchScene(sceneName);
            else
                setPlayerNameForm.SetActive(false);
        }
    }
}
