using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;             // Speed of the enemy
    public float detectionRange = 5f;    // Range within which the enemy can detect the player
    public float attackRange = 1.5f;     // Distance at which the enemy can attack the player
    public int attackDamage = 10;        // Damage per attack
    public float attackCooldown = 1f;    // Time between attacks
    public Transform pointA;             // Patrol point A
    public Transform pointB;             // Patrol point B
    public Transform player;             // Reference to the player
    public int playerHealth = 100;       // Player's health

    private bool movingToPointB = true;  // Whether the enemy is moving to point B
    private bool playerInRange = false;  // Is the player within detection range?
    private float nextAttackTime = 0f;   // Cooldown timer for next attack

    void Update()
    {
        if (playerInRange)
        {
            ChaseAndAttackPlayer(); // If the player is in range, chase and attack
        }
        else
        {
            Patrol(); // Otherwise, patrol between points
        }
    }

    void Patrol()
    {
        // Move towards point B
        if (movingToPointB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

            // If the enemy reaches point B, switch to point A
            if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                movingToPointB = false;
            }
        }   
        // Move towards point A
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

            // If the enemy reaches point A, switch to point B
            if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                movingToPointB = true;
            }
        }

        DetectPlayer(); // Check if the player is within detection range
    }

    void DetectPlayer()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            playerInRange = true;
        }
    }

    void ChaseAndAttackPlayer()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Check if the enemy is close enough to attack the player
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                AttackPlayer(); // Attack the player
                nextAttackTime = Time.time + attackCooldown; // Set the next attack time
            }
        }

        // If the player is out of detection range, stop chasing
        if (Vector3.Distance(transform.position, player.position) > detectionRange)
        {
            playerInRange = false;
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Enemy attacks the player!");

        // Reduce the player's health
        playerHealth -= attackDamage;

        // Check if the player is dead
        if (playerHealth <= 0)
        {
            Debug.Log("Player has died!");
            Destroy(player.gameObject); // Destroy the player (or trigger a death event)
        }
    }
}

