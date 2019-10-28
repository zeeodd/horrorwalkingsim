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
    public Text text;
    public Text text2;

    // Triggers
    public GameObject trigger1;

    // Beat bools
    private bool metEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        //player.GetComponent<FirstPersonDrifter>().enabled = false;

        foreach (Transform child in maze.transform)
        {
            child.GetComponent<BoxCollider>().enabled = true;
            child.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<CornSlowDown>().slowingDown)
        {
            text.text = "Maybe I should stay on the path... For now...";
            text.enabled = true;
            Invoke("TextFadeOut", 4f);
        }

       if (player.GetComponent<PlayerInteract>().tryingToMoveTowardEnemy)
        {
            text2.text = "Not that way!";
            text2.CrossFadeAlpha(1.0f, 0.25f, true);
            text2.enabled = true;
            Invoke("Text2FadeOut", 4f);
            player.GetComponent<PlayerInteract>().tryingToMoveTowardEnemy = false;
        }
    }

    void TextFadeOut()
    {
        text.CrossFadeAlpha(0.0f, 0.25f, true);
        text.gameObject.SetActive(false);
    }

    void Text2FadeOut()
    {
        text2.CrossFadeAlpha(0.0f, 0.25f, true);
    }
}
