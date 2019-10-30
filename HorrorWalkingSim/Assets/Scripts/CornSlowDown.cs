using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornSlowDown : MonoBehaviour
{
    public bool slowingDown = false;
    private Collider lastCornTrigger;
    private float normalWalk;
    private float normalRun;
    private float slowWalk;
    private float slowRun;

    private void Start()
    {
        normalWalk = GetComponent<FirstPersonDrifter>().walkSpeed;
        normalRun = GetComponent<FirstPersonDrifter>().runSpeed;
        slowWalk = GetComponent<FirstPersonDrifter>().walkSpeed / 3.5f;
        slowRun = GetComponent<FirstPersonDrifter>().runSpeed / 3.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        lastCornTrigger = other;

        if (other.tag == "Corn")
        {
            GetComponent<FirstPersonDrifter>().walkSpeed = slowWalk;
            GetComponent<FirstPersonDrifter>().runSpeed = slowRun;
            slowingDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lastCornTrigger == other)
        {
            GetComponent<FirstPersonDrifter>().walkSpeed = normalWalk;
            GetComponent<FirstPersonDrifter>().runSpeed = normalRun;
            slowingDown = false;
        }
    }
}
