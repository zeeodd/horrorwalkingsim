using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Speed control
    private float speed;
    private float w_speed = 6.0f;
    private float r_speed = 12.0f;

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
        if (Input.GetKey(KeyCode.I))
        {
            speed = w_speed;

            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", true);
        }
    }
}
