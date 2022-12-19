using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    public GameObject scenePrefab;

    private GameObject prefabInstance;
    private ARRaycastManager arRaycastManager;
    private List <ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        isEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;

                if (!isEnabled)
                {
                    prefabInstance = Instantiate(scenePrefab, hitpose.position, hitpose.rotation);
                    isEnabled = true;
                }
                else
                {
                    prefabInstance.transform.position = hitpose.position;
                }
            }
        }
    }
}