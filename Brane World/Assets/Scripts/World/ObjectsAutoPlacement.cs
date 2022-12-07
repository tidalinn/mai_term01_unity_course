using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ObjectsAutoPlacement : MonoBehaviour
{
    [SerializeField]
    public GameObject placedPrefab;

    [SerializeField]
    private ARPlaneManager arPlaneManager;
    private bool isCreated;

    void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && isCreated == false)
        {
            ARPlane arPlane = args.added[0];
            GameObject placedPrefabInstance = Instantiate(placedPrefab, arPlane.transform.position, Quaternion.identity);
            isCreated = true;
        }
    }
}
