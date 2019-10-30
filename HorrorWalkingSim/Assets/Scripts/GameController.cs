using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // Important Game Pieces
    public GameObject player;
    public GameObject enemy;
    public GameObject key;
    public GameObject shovel;
    public GameObject maze;
    public GameObject dirt;
    public GameObject gate;
    public GameObject door1;
    public GameObject door2;
    public Text text;
    public Text text2;

    // Triggers
    public GameObject trigger1;

    // Beat bools
    private bool lost = false;

    // Start is called before the first frame update
    void Start()
    {
        //player.GetComponent<FirstPersonDrifter>().enabled = false;

        foreach (Transform child in maze.transform)
        {
            child.GetComponent<BoxCollider>().enabled = true;
            child.GetComponent<BoxCollider>().isTrigger = false;
        }

        door1.GetComponent<ObjectInteract>().enabled = false;
        shovel.GetComponent<ObjectInteract>().enabled = false;
        dirt.GetComponent<ObjectInteract>().enabled = false;
        gate.GetComponent<ObjectInteract>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<CornSlowDown>().slowingDown)
        {
            text.text = "Maybe I should stay on the path... For now...";
            text.enabled = true;
            text.CrossFadeAlpha(0f, 3f, true);
        }

       if (player.GetComponent<PlayerInteract>().tryingToMoveTowardEnemy)
        {
            text2.text = "Not that way!";
            text2.enabled = true;
            Invoke("Text2FadeOut", 3f);
            player.GetComponent<PlayerInteract>().tryingToMoveTowardEnemy = false;
        }

        // As the player picks up the KEY
        if (key == null && door1 != null && door2 != null)
        {
            player.GetComponent<PlayerInteract>().hasKey = true;
            door1.GetComponent<ObjectInteract>().enabled = true;
        }

        // As the player opens the DOOR
        if (door1 == null && shovel != null)
        {
            Destroy(door2);
            shovel.GetComponent<ObjectInteract>().enabled = true;
        } else if (door2 == null && shovel != null)
        {
            Destroy(door1);
            shovel.GetComponent<ObjectInteract>().enabled = true;
        }

        // As the player picks up the SHOVEL
        if (shovel == null && dirt != null)
        {
            dirt.GetComponent<ObjectInteract>().enabled = true;
        }

        // As the player digs up the key
        if (dirt == null && gate != null)
        {
            player.GetComponent<PlayerInteract>().hasLastKey = true;
            gate.GetComponent<ObjectInteract>().enabled = true;
        }
    }

    void TextFadeOut()
    {
        text.CrossFadeAlpha(0.0f, 3f, true);
        text.gameObject.SetActive(false);
    }

    void Text2FadeOut()
    {
        text2.CrossFadeAlpha(0.0f, 0.2f, true);
    }
}
