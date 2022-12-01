using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void OpenRegistration()
    {
        SceneManager.LoadScene(0); // Registration
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene(1); // Main Scene
    }
}
