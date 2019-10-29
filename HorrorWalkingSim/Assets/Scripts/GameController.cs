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

        shovel.gameObject.SetActive(false);
        dirt.gameObject.SetActive(false);
        gate.GetComponent<ObjectInteract>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<CornSlowDown>().slowingDown)
        {
            Color tempColor = text.color;
            tempColor.a = 1.0f;
            text.color = tempColor;
            text.text = "Maybe I should stay on the path... For now...";
            text.enabled = true;
            text.CrossFadeAlpha(0f, 3f, true);
        }

       if (player.GetComponent<PlayerInteract>().tryingToMoveTowardEnemy)
        {
            Color tempColor2 = text2.color;
            tempColor2.a = 1.0f;
            text2.color = tempColor2;
            text2.text = "Not that way!";
            text2.enabled = true;
            Invoke("Text2FadeOut", 0f);
            player.GetComponent<PlayerInteract>().tryingToMoveTowardEnemy = false;
        }

        // As the payer picks up various inventory items
        if (key == null && shovel != null && !shovel.activeSelf)
        {
            player.GetComponent<PlayerInteract>().hasKey = true;
        }

        if (door1 == null && shovel != null && !shovel.activeSelf)
        {
            Destroy(door2);
            shovel.gameObject.SetActive(true);
        } else if (door2 == null && shovel != null && !shovel.activeSelf)
        {
            Destroy(door1);
            shovel.gameObject.SetActive(true);
        }

        if (shovel == null && dirt != null && !dirt.activeSelf)
        {
            dirt.gameObject.SetActive(true);
        }

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
