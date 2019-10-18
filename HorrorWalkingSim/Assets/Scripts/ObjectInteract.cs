using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public bool isBeingInteractedWith = false;
    public Text interactText;
    private int maxSize = 55;
    private int minSize = 1;
    private float maxA = 1;
    private float minA = 0;
    static float t = 0.0f;
    public float deltaT;

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
            interactText.fontSize = (int)Mathf.Lerp(minSize, maxSize, t);
            t += deltaT * Time.deltaTime;
        }
        else 
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
            interactText.gameObject.SetActive(false);
            interactText.fontSize = 1;
            t = 0.0f;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            print("INTERACTED");
            Destroy(gameObject);
            interactText.gameObject.SetActive(false);
            interactText.fontSize = 1;
            t = 0.0f;
            //TODO: INVENTORY FUNCTION
        }

    }
}
