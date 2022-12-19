using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RenamePlayer : MonoBehaviour
{
    private GameObject setPlayerNameForm;
    private bool isRenamed;

    // Start is called before the first frame update
    void Start()
    {
        setPlayerNameForm = GetComponent<PlayerName>().setPlayerNameForm;
        isRenamed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene" && !isRenamed)
        {
            setPlayerNameForm.SetActive(false);
        }
    }

    public void Rename()
    {
        setPlayerNameForm.SetActive(true);
        isRenamed = true;
    }
}
