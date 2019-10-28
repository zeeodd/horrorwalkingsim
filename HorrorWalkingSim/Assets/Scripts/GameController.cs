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
