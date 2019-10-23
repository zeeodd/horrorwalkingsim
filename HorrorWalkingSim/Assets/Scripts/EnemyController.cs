using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Speed control
    private float speed;
    private float w_speed = 4.0f;
    private float r_speed = 10.0f;

    // Grab model components
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

    // Get Player
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();

        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
    }

    void Update()
    {
        // TODO: Remove, keep just for testing
        if (Input.GetKey(KeyCode.I))
        {
            speed = 0;

            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.O))
        {
            speed = w_speed;

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.P))
        {
            speed = r_speed;

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }

        //TODO: 1. Raycast from the enemy
        //      2. Rotate raycast as enemy walks
        //      3. Have enemy always be walking in direction of player
        //      4. Randomize player location so it's not 100% direct

        // Find player location
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.forward = -player.transform.forward;

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, player.transform.position) <= 10.0f)
        {
            speed = r_speed;

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
            print("IN RANGE");

        }
    }
}
