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
    private GameObject xrOrigin;
    
    // Start is called before the first frame update
    void Start()
    {
        prefabScene = (GameObject)Resources.Load(prefabName);
        xrOrigin = GameObject.Find("XR Origin");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            xrOrigin.GetComponent<PlaceOnPlane>().scenePrefab = prefabScene;
        }
    }
}