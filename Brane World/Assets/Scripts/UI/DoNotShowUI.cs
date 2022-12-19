using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotShowUI : MonoBehaviour
{
    public GameObject userUI;

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "Registration")
        {
            userUI.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name != "Registration")
        {
            userUI.SetActive(true);
        }
    }
}
