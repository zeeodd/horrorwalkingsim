using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public Camera animatedCamera;

    void Update()
    {
        if(player.GetComponent<PlayerInteract>().animating)
        {
            transform.forward = animatedCamera.transform.forward;
        }
        else
        {
            transform.forward = player.transform.forward;
        }
    }

}
