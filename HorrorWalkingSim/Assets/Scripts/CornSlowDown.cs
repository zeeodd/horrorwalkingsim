using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornSlowDown : MonoBehaviour
{
    private Collider lastCornTrigger;

    private void OnTriggerEnter(Collider other)
    {
        lastCornTrigger = other;

        if (other.tag == "Corn")
        {
            GetComponent<FirstPersonDrifter>().walkSpeed = 3;
            GetComponent<FirstPersonDrifter>().runSpeed = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lastCornTrigger == other)
        {
            GetComponent<FirstPersonDrifter>().walkSpeed = 6;
            GetComponent<FirstPersonDrifter>().runSpeed = 10;
        }
    }
}
