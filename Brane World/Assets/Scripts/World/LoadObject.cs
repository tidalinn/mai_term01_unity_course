using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadObject : MonoBehaviour
{
    [Header("Show Prefab on Scene")]
    public string prefabName;
    public string sceneName;

    private GameObject prefabScene;
    
    // Start is called before the first frame update
    void Start()
    {
        prefabScene = (GameObject)Resources.Load(prefabName);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            GetComponent<PlaceOnPlane>().scenePrefab = prefabScene;
        }
    }
}