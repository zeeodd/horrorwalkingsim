﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // == Various enemy control vars ==
    // Speed vars
    private float speed;
    public float w_speed = 4.0f;
    public float r_speed = 10.0f;
    // These vars control the scanning of the enemy
    public int visionDist = 25;
    public float visionRadius = 60.0f;
    private float inVisionTime;
    private float inVisionTimeInit = 5.0f;
    // These control the idle/patrol vars
    private float idleTime = 1.0f;
    private float destRadius = 5.0f;
    private bool atDestination = false;
    private bool setDestination = false;
    private Vector3 destination;

    // Grab model components
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;

    // Get Player
    public GameObject player;

    // Make the enemy a NavMesh agent
    NavMeshAgent agent;

    private enum States { Idle, Patrol, Suspicous, Chasing, Testing };
    private States currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col_size = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();

        inVisionTime = inVisionTimeInit;

        // TODO: CHANGE THIS 
        currentState = States.Testing;
    }

    void Update()
    {
        // State Controller 
        switch (currentState)
        {
            case States.Idle: Idle(); break;
            case States.Patrol: Patrol(); break;
            case States.Suspicous: Suspicious(); break;
            case States.Chasing: Chasing(); break;
            case States.Testing: Testing(); break;
            default: break;
        }

        print(currentState);

        // Keep updating speed to controlled speed vars
        GetComponent<NavMeshAgent>().speed = speed;

    }

    void Idle()
    {
        // Change State
        currentState = States.Idle;
        print("IDLE NOW");

        // Control speed and animation
        speed = 0;
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);

        // =============================

        if(inVisionTime < inVisionTimeInit)
        {
            inVisionTime = inVisionTimeInit;
        }

        VisualScan();
        Invoke("ResetDestination", idleTime);
    }

    void Patrol()
    {
        // Change State
        currentState = States.Patrol;
        print("PATROL NOW");

        // Control speed and animation
        speed = w_speed;
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);

        // =============================

        // If the destination is not set, set the destination and flip the bool
        if (!setDestination)
        {
            destination = GetNewDestination();
            setDestination = true;
        }

        // Now there's a destination, move toward it until you're there
        if (!atDestination && setDestination)
        {
            agent.SetDestination(destination);
            VisualScan();
        }

        // Distance calculations
        // If the enemy has walked within the radius of the final location,
        // flip the atDestination bool
        float dist;
        if (float.IsPositiveInfinity(agent.remainingDistance) || agent.pathPending)
        {
            dist = Vector3.Distance(transform.position, destination);
        }
        else
        {
            dist = agent.remainingDistance;
        }

        if (dist <= destRadius && !atDestination)
        {
            atDestination = true;
        }

        // If atDestination, switch state to Idle
        if (atDestination)
        {
            currentState = States.Idle;
        }
    }

    void Suspicious()
    {
        // Change State
        currentState = States.Suspicous;
        print("SUSPICIOUS NOW");

        // Control speed and animation
        speed = w_speed;
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);

        // =============================

        // Keep chasing
        agent.SetDestination(player.transform.position);

        if (inVisionTime < inVisionTimeInit)
        {
            inVisionTime = inVisionTimeInit;
        }

        // If the player is within the vision radius and close enough, raycast!
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(transform.position + Vector3.up, forward);
        Debug.DrawRay(ray.origin, ray.direction * visionDist, Color.yellow);

        if (Physics.Raycast(ray, out hit, visionDist))
        {
            // Get the hit object's transform
            Transform objectHit = hit.transform;

            if (objectHit.tag == "Player")
            {
                currentState = States.Chasing;
            }
            else
            {
                currentState = States.Idle;
            }
        }

    }

    void Chasing()
    {
        // Change State
        currentState = States.Chasing;
        print("CHASING NOW");

        // Control speed and animation
        speed = r_speed;
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);

        // =============================

        inVisionTime -= 1.0f * Time.deltaTime;

        if (inVisionTime > 0f)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            currentState = States.Suspicous;
        }
    }

    void Testing()
    {
        // Change State
        currentState = States.Testing;

        // Control speed and animation
        speed = 0;
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
    }

    Vector3 GetNewDestination()
    {
        var snapshotPos = player.transform.position;

        var currentPlayerX = snapshotPos.x;
        var currentPlayerY = snapshotPos.y;
        var currentPlayerZ = snapshotPos.z;

        var randomRadius = Random.insideUnitCircle * 10;

        var newDestinatonX = currentPlayerX + randomRadius.x;
        var newDesinationY = currentPlayerY + randomRadius.y;

        var newDestination = new Vector3(newDestinatonX, newDesinationY, currentPlayerZ);

        atDestination = false;

        return newDestination;
    }

    void ResetDestination()
    {
        atDestination = false;
        setDestination = false;
        currentState = States.Patrol;
    }

    void VisualScan()
    {
        // Gets a vector that points from the player's position to the enemy's
        var heading = player.transform.position - transform.position;

        // Grab the angle and the distance from the player
        var distance = heading.magnitude;
        float angle = Vector3.Angle(transform.forward, heading);

        if ((angle <= visionRadius) && (distance <= visionDist))
        {
            // If the player is within the vision radius and close enough, raycast!
            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Ray ray = new Ray(transform.position + Vector3.up, forward);
            Debug.DrawRay(ray.origin, ray.direction * visionDist, Color.yellow);

            if (Physics.Raycast(ray, out hit, visionDist))
            {
                // Get the hit object's transform
                Transform objectHit = hit.transform;

                if (objectHit.tag == "Player")
                {
                    print("Hit Player");
                    currentState = States.Chasing;
                }
            }
        }

    }
}