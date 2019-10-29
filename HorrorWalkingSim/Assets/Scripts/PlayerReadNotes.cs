using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReadNotes : MonoBehaviour
{

    public Camera playerCamera;
    private Transform lastHit;
    private bool lookingAtNote;
    private Vector3 notePos;
    private Quaternion noteRot;

    public Image cursorTexture;
    public Text interactText;
    public string displayText;

    private int maxSize = 50;
    private int minSize = 1;

    private int curMaxSize = 30;
    private int curMinSize = 15;

    // Start is called before the first frame update
    void Start()
    {
        interactText.gameObject.SetActive(false);
        interactText.text = displayText;
        cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMinSize, curMinSize);
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

            if (objectHit.tag == "Note")
            {

                // Then check the distance between the two objects
                float dist = Vector3.Distance(objectHit.position, transform.position);

                // If the player is close enough, allow interaction
                if (dist <= 3.5)
                {
                    if (!lookingAtNote)
                    {
                        objectHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 0.5f);
                        interactText.fontSize = maxSize;
                        interactText.gameObject.SetActive(true);
                        cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMaxSize, curMaxSize);
                    }
                    lastHit = objectHit;

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        if (!lookingAtNote)
                        {
                            // Modify note position and bool
                            notePos = objectHit.position;
                            noteRot = objectHit.rotation;
                            objectHit.rotation = transform.rotation;
                            Vector3 playerPos = transform.position;
                            objectHit.position = new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z + 1f);
                            lookingAtNote = true;

                            // Nullify note emission
                            objectHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
                            interactText.gameObject.SetActive(false);
                            cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMinSize, curMinSize);
                        }
                        else
                        {
                            objectHit.position = notePos;
                            objectHit.rotation = noteRot;
                            lookingAtNote = false;
                        }
                    }

                }

            }
            else
            {
                if (lastHit != null && lastHit.tag == "Note" && lastHit.GetComponent<Renderer>() != null)
                {
                        lastHit.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
                        interactText.gameObject.SetActive(false);
                        cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMinSize, curMinSize);
                }

                if (lastHit != null && lastHit.tag == "Note" && lookingAtNote)
                {
                    lastHit.position = notePos;
                    lastHit.rotation = noteRot;
                    lookingAtNote = false;
                }
            }

        }
    }
}
