using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    public bool isBeingInteractedWith = false;
    public Image inventoryIcon; // An icon that will be used for inventory
    public Text interactText; // The text gameobj from the Canvas ("INTERACT")
    public string displayText; // The text to display for this specific obj
    public float deltaT; // Lerp control
    public Image cursorTexture; // The cursor texture

    // Obj lerp control
    private int maxSize = 50;
    private int minSize = 1;
    static float t = 0.0f;

    // Cursor lerp control
    private int curMaxSize = 30;
    private int curMinSize = 15;
    private int curCurrSize = 15;
    static float tCur = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set emission intensity to 0, interact text inactive and small, cursor to normal
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
        interactText.gameObject.SetActive(false);
        interactText.text = displayText;
        cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMinSize, curMinSize);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if object is being interacted with
        if (isBeingInteractedWith) 
        {

            // Modify the emission and interaction display text
            // Also modify the cursor size
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 0.5f);
            interactText.fontSize = maxSize;
            interactText.gameObject.SetActive(true);
            cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMaxSize, curMaxSize);

            print("INTERACTING!!!");

            // Check if player wants to interact with the object
            if (Input.GetKey(KeyCode.Mouse0))
            {
                print("INTERACTED");
                Destroy(gameObject);

                // Reset everything
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
                interactText.gameObject.SetActive(false);
                cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMinSize, curMinSize);

                if (inventoryIcon != null)
                {
                    inventoryIcon.gameObject.SetActive(true);
                }

                // Check if we're interacting with the door, and if so, disable the key icon
                if (interactText.text == "OPEN")
                {
                    inventoryIcon.gameObject.SetActive(false);
                }
            }

        }
        else 
        {
            // Reset everything
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 0f);
            interactText.gameObject.SetActive(false);
            cursorTexture.GetComponent<RectTransform>().sizeDelta = new Vector2(curMinSize, curMinSize);
        }
    }
}
