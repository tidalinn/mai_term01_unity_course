using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public void Grab()
    {
        this.gameObject.SetActive(false);
    }
}
