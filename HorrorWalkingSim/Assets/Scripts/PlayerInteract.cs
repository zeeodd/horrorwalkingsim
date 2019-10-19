using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera; // Player's camera
    private Transform lastHit;

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

            // Check if the object is interactable
            if (objectHit.tag == "Interactive")
            {

                // Then check the distance between the two objects
                float dist = Vector3.Distance(objectHit.position, transform.position);

                // If the player is close enough, allow interaction
                if (dist <= 2.5f)
                {
                    objectHit.GetComponent<ObjectInteract>().isBeingInteractedWith = true;
                    lastHit = objectHit;
                }
                else
                {
                    objectHit.GetComponent<ObjectInteract>().isBeingInteractedWith = false;
                }
            } 
            else
            {
                if (lastHit != null && lastHit.GetComponent<ObjectInteract>() != null)
                {
                    lastHit.GetComponent<ObjectInteract>().isBeingInteractedWith = false;
                }
            }

        }
    }
}
