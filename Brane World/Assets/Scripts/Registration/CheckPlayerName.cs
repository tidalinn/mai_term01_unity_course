using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerName : MonoBehaviour
{
    private string sceneName;
    private GameObject sceneSwitcherPrefab;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = GetComponent<PlayerName>().sceneName;
        sceneSwitcherPrefab = GetComponent<PlayerName>().sceneSwitcherPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("userName").Length > 0 && sceneSwitcherPrefab != null)
        {
            sceneSwitcherPrefab.GetComponent<SceneSwitcher>().SwitchScene(sceneName);
        }
    }
}
