using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinInfo : MonoBehaviour
{
    public Item item;
    public GameObject textLevel;

    // Start is called before the first frame update
    void Start()
    {
        textLevel.GetComponent<TextMeshPro>().text = item.level + " lvl";
    }
}
