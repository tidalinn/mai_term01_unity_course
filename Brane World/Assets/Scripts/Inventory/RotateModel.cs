using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    [Header("Params")]
    public float speed = 40;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -speed) * Time.deltaTime);
    }
}
