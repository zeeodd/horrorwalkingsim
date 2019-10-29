using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera; // Player's camera
    private Transform lastHit;
    public Camera animatedCamera;
    public Image keyIcon;

    public bool animating = false;
    private Collider lastTrigger;
    public bool tryingToMoveTowardEnemy = false;
    public bool exitingMaze = false;

    private bool lookingAtNote;
    private Vector3 notePos;
    private Quaternion noteRot;

    public bool hasKey = false;
    public bool hasLastKey = false;

    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    public GameObject triggerWall;

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
                if (dist <= 3.5f)
                {
                    objectHit.GetComponent<ObjectInteract>().isBeingInteractedWith = true;
                    lastHit = objectHit;

                    if (hasKey && Input.GetKeyDown(KeyCode.Mouse0) && keyIcon.gameObject.activeSelf)
                    {
                        Destroy(objectHit.gameObject);
                        keyIcon.gameObject.SetActive(false);
                        hasKey = false;
                    }

                    if (hasLastKey && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        //TODO: End
                        SceneManager.LoadScene("End");
                    }
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

            if (objectHit.tag == "Note")
            {

                // Then check the distance between the two objects
                float dist = Vector3.Distance(objectHit.position, transform.position);

                // If the player is close enough, allow interaction
                if (dist <= 2.5)
                {
                    if (!lookingAtNote)
                    {
                        objectHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 0.5f);
                    }

                    lastHit = objectHit;

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        print("CLOSE AND LOOKING");
                        if (!lookingAtNote)
                        {
                            notePos = objectHit.position;
                            noteRot = objectHit.rotation;
                            objectHit.rotation = transform.rotation;
                            Vector3 playerPos = transform.position;
                            objectHit.position = new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z + 1f);
                            lookingAtNote = true;
                            objectHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
                        }
                        else
                        {
                            objectHit.position = notePos;
                            objectHit.rotation = noteRot;
                            lookingAtNote = false;
                        }
                    }

                }
                else
                {
                    if (lastHit != null && lastHit.GetComponent<Renderer>() != null)
                    {
                        objectHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
                    }
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
            GetComponent<FirstPersonDrifter>().enabled = false;
            animatedCamera.GetComponent<Camera>().enabled = true;
            animating = true;
            lastTrigger = other;
            Invoke("AnimateOff", 16.0f);
        }

        if (other.name == "TriggerPoint2")
        {
            tryingToMoveTowardEnemy = true;
            lastTrigger = other;
        }

        if (other.name == "TriggerPoint3")
        {
            exitingMaze = true;
            Destroy(lastTrigger);
        }

        if (exitingMaze)
        {
            if (trigger1 != null)
            {
                Destroy(trigger1);
            }

            if (trigger2 != null)
            {
                Destroy(trigger2);
            }

            if (trigger3 != null)
            {
                Destroy(trigger3);
            }

            if (triggerWall != null)
            {
                Destroy(triggerWall);
            }
        }
    }

    void AnimateOff()
    {
        Destroy(lastTrigger);
        //lastTrigger.isTrigger = false;
        animating = false;
        playerCamera.gameObject.SetActive(true);
        animatedCamera.GetComponent<Camera>().enabled = false;
        animatedCamera.gameObject.SetActive(false);
        playerCamera.GetComponent<Camera>().enabled = true;
        GetComponent<FirstPersonDrifter>().enabled = true;
    }

}
