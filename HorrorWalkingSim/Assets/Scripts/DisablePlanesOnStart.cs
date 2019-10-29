using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlanesOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<BoxCollider>().enabled = true;
            child.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
