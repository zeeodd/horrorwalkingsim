﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Inside")
        {
            gameObject.GetComponent<AudioSource>().Stop();


        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Inside")
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
