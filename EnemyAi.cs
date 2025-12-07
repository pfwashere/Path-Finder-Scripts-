using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZombieAI : MonoBehaviour
{
    public Transform[] waypoints;  // 4 patrol waypoints
    public float detectionRange = 2.3f;
    public float attackRange = 2f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    private int currentWaypointIndex = 0;
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject tryAgainMenu;
    private bool playerIsDead = false;


    void Start()
    {
     
        tryAgainMenu.SetActive(false); 
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.autoBraking = false;
        GoToNextWaypoint();
    }

    void Update()
    {
        if (!playerIsDead && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            PlayerDies();
        }
        // Check distance - player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // atk
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("IsAttacking", true);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // chase
            agent.isStopped = false;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("IsAttacking", false);
        }
        else
        {
            // Patrol when player is not detected
            Patrol();
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }

        animator.SetBool("isWalking", true);
        animator.SetBool("IsAttacking", false);
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected waypoint
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[currentWaypointIndex].position);

        // Choose the next waypoint in the array as the destination
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void PlayerDies()
    {
        playerIsDead = true;
        Debug.Log("Player has been detected and died!");

        // player freeze (died)
        vThirdPersonController playerController = player.GetComponent<vThirdPersonController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Try Again UI
        if (tryAgainMenu != null)
        {
            tryAgainMenu.SetActive(true);
        }

       

        // auto re
        StartCoroutine(RestartGame()); 



    private IEnumerator RestartGame()
    {
        // autorestart
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

