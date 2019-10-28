using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera; // Player's camera
    private Transform lastHit;
    public Camera animatedCamera;

    public bool animating = false;
    private Collider lastTrigger;
    public bool tryingToMoveTowardEnemy = false;
    public bool exitingMaze = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TriggerPoint1")
        {
            animatedCamera.gameObject.SetActive(true);
            playerCamera.GetComponent<Camera>().enabled = false;
            playerCamera.gameObject.SetActive(false);
            animatedCamera.GetComponent<Camera>().enabled = true;
            animating = true;
            lastTrigger = other;
            Invoke("AnimateOff", 16.0f);
        }

        if (other.name == "TriggerPoint2")
        {
            tryingToMoveTowardEnemy = true;
        }

        if (other.name == "TriggerPoint3")
        {
            exitingMaze = true;
        }
    }

    void AnimateOff()
    {
        lastTrigger.isTrigger = false;
        animating = false;
        playerCamera.gameObject.SetActive(true);
        animatedCamera.GetComponent<Camera>().enabled = false;
        animatedCamera.gameObject.SetActive(false);
        playerCamera.GetComponent<Camera>().enabled = true;
    }
}
