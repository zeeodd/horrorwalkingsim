using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Get the hit object's transform
            Transform objectHit = hit.transform;

            // Get the distance between the two objects
            float dist = Vector3.Distance(objectHit.position, transform.position);

            if (objectHit.tag == "Interactive" && dist <= 2.5f) 
            {
                objectHit.GetComponent<ObjectInteract>().isBeingInteractedWith = true;
            }
            else
            {
                if (objectHit.GetComponent<ObjectInteract>() != null)
                {
                    objectHit.GetComponent<ObjectInteract>().isBeingInteractedWith = false;
                }
            }

        }
    }
}
