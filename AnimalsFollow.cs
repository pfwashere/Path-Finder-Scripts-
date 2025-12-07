using System.Collections;
using UnityEngine;
using UnityEngine.AI; // Import NavMeshAgent

public class CageAndAnimalSystem : MonoBehaviour
{
    public Transform DoorHinge;         // The door to open
    public Transform Player;           // The player for the animal to follow
    public GameObject Animal;          // The animal GameObject with NavMeshAgent
    public float OpenAngleDoor = 90f;  // Angle to open the door
    public float DoorSpeed = 2f;       // Speed to open the door
    public float FollowStoppingDistance = 1.5f; // Distance to stop following the player
    public GameObject UIElement;       // UI element to show when in range
    private bool inRange = false;      // Check if player is near the cage
    private bool isDoorOpen = false;   // Check if the door is open
    private Quaternion closedRotationDoor; // Original door rotation
    private Quaternion openRotationDoor;   // Opened door rotation
    private NavMeshAgent animalAgent;  // NavMeshAgent for the animal

    void Start()
    {
        // Store the original and open door rotations
        closedRotationDoor = DoorHinge.rotation;
        openRotationDoor = Quaternion.Euler(DoorHinge.eulerAngles.x, DoorHinge.eulerAngles.y + OpenAngleDoor, DoorHinge.eulerAngles.z);

        if (UIElement != null) UIElement.SetActive(false);

        // Get the NavMeshAgent from the animal
        animalAgent = Animal.GetComponent<NavMeshAgent>();
        if (animalAgent == null)
        {
            Debug.LogError("Animal GameObject must have a NavMeshAgent component!");
        }
    }

    void Update()
    {
        // Check for player input when in range
        if (inRange && Input.GetKeyDown(KeyCode.E) && !isDoorOpen)
        {
            isDoorOpen = true;
            if (UIElement != null) UIElement.SetActive(false);
            StartCoroutine(OpenDoorAndStartFollowing());
        }

        // Make the animal follow the player if the door is open
        if (isDoorOpen && animalAgent != null)
        {
            float distance = Vector3.Distance(Animal.transform.position, Player.position);
            if (distance > FollowStoppingDistance)
            {
                animalAgent.SetDestination(Player.position); // Set player as the target
            }
            else
            {
                animalAgent.ResetPath(); // Stop moving when within the stopping distance
            }
        }
    }

    private IEnumerator OpenDoorAndStartFollowing()
    {
        // Open the door smoothly
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            DoorHinge.rotation = Quaternion.Slerp(closedRotationDoor, openRotationDoor, elapsedTime * DoorSpeed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }
        DoorHinge.rotation = openRotationDoor; // Ensure door is fully open
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            if (UIElement != null) UIElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (UIElement != null) UIElement.SetActive(false);
        }
    }
}
