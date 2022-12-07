using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotShowUI : MonoBehaviour
{
    public GameObject userUI;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Registration")
        {
            userUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name != "Registration")
        {
            userUI.SetActive(true);
        }
    }
}
