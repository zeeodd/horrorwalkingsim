using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public bool isBeingInteractedWith = false;
    public Text interactText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
        interactText.gameObject.SetActive(false);
        interactText.fontSize = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingInteractedWith) 
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0.25f);
            interactText.gameObject.SetActive(true);
            interactText.fontSize = 55;
        }
        else 
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
            interactText.gameObject.SetActive(false);
            interactText.fontSize = 1;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            print("INTERACTED");
            Destroy(gameObject);
            interactText.gameObject.SetActive(false);
            interactText.fontSize = 1;
        }

    }
}
