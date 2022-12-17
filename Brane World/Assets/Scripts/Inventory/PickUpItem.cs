using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            float pickUpDistance = .5f;

            if (Physics.Raycast(Input.GetTouch(0).position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out GrabbableObject grabbableObject))
                {
                    grabbableObject.Grab();
                }
            }   
        } 
    }
}
