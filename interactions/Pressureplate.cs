using UnityEngine;
using System.Collections;

public class PressurePlateSystem : MonoBehaviour
{
    public Transform PressurePlate; // The pressure plate itself
    public Transform DoorHinge1; // First door hinge
    public Transform DoorHinge2; // Second door hinge
    public float openAngleDoor1 = 90f; // Open angle for door 1
    public float openAngleDoor2 = 90f; // Open angle for door 2
    public float Speed = 2f; // Speed of the door opening
    private bool isOpen = false; // Track whether doors are open
    private bool inRange = false; // Track if player is on the pressure plate
    private Quaternion closedRotationDoor1; // Closed rotation for door 1
    private Quaternion closedRotationDoor2; // Closed rotation for door 2
    private Quaternion openRotationDoor1; // Open rotation for door 1
    private Quaternion openRotationDoor2; // Open rotation for door 2
    public GameObject uiElementWarning;

    void Start()
    {
        // Store the original rotation of the doors
        closedRotationDoor1 = DoorHinge1.rotation;
        openRotationDoor1 = Quaternion.Euler(DoorHinge1.eulerAngles.x, DoorHinge1.eulerAngles.y + openAngleDoor1, DoorHinge1.eulerAngles.z);
        closedRotationDoor2 = DoorHinge2.rotation;
        openRotationDoor2 = Quaternion.Euler(DoorHinge2.eulerAngles.x, DoorHinge2.eulerAngles.y + openAngleDoor2, DoorHinge2.eulerAngles.z);
        uiElementWarning.SetActive(false);
    }

    void Update()
    {
        // Check if player is on the pressure plate and has a box
        if (inRange && PlaceABox.placeBox == true)
        {
            // Start opening the doors if they're not already open
            if (!isOpen)
            {
                StartCoroutine(ActivateDoors());
            }
        }
        if (inRange && PlaceABox.placeBox == false)
        {
            StartCoroutine(ShowWarning());
        }   
    }
    private IEnumerator ShowWarning()
    {
        if (uiElementWarning != null)
        {
            uiElementWarning.SetActive(true); // Show the warning UI
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            uiElementWarning.SetActive(false); // Hide the warning UI
        }
    }

    private IEnumerator ActivateDoors()
    {
        // Opening the doors
        Debug.Log("Doors Opening!!");
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            DoorHinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * Speed);
            DoorHinge2.rotation = Quaternion.Slerp(closedRotationDoor2, openRotationDoor2, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure doors reach the final rotation
        DoorHinge1.rotation = openRotationDoor1;
        DoorHinge2.rotation = openRotationDoor2;
        isOpen = true; // Mark doors as open

        // Optionally add a delay before closing the doors again
        yield return new WaitForSeconds(2f); // Adjust as necessary

        // Closing the doors after some time
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            DoorHinge1.rotation = Quaternion.Slerp(openRotationDoor1, closedRotationDoor1, elapsedTime * Speed);
            DoorHinge2.rotation = Quaternion.Slerp(openRotationDoor2, closedRotationDoor2, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure doors reach the closed rotation
        DoorHinge1.rotation = closedRotationDoor1;
        DoorHinge2.rotation = closedRotationDoor2;
        isOpen = false; // Mark doors as closed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
