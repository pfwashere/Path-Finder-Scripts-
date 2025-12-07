using UnityEngine;
using System.Collections;

public class RopeLeverSystem : MonoBehaviour
{
    public Transform RopeHandle; // Reference to the rope handle
    public Transform Doorhinge1;
    public Transform Doorhinge2;
    public float pullDistance = 0.5f; // Distance the rope is pulled down
    public float openAngleDoor1 = 90f;
    public float openAngleDoor2 = 90f;
    public float Speed = 2f;
    private bool isOpen = false;
    private bool inRange = false;
    private Vector3 originalRopePosition;
    private Vector3 pulledRopePosition;
    private Quaternion openRotationDoor1;
    private Quaternion openRotationDoor2;
    private Quaternion closedRotationDoor1;
    private Quaternion closedRotationDoor2;
    public GameObject uiElement;

    void Start()
    {
        // Store the original position of the rope
        originalRopePosition = RopeHandle.position;

        // Calculate the pulled position of the rope
        pulledRopePosition = originalRopePosition + Vector3.down * pullDistance;

        // Store the original and open rotations of the doors
        closedRotationDoor1 = Doorhinge1.rotation;
        openRotationDoor1 = Quaternion.Euler(Doorhinge1.eulerAngles.x, Doorhinge1.eulerAngles.y + openAngleDoor1, Doorhinge1.eulerAngles.z);
        closedRotationDoor2 = Doorhinge2.rotation;
        openRotationDoor2 = Quaternion.Euler(Doorhinge2.eulerAngles.x, Doorhinge2.eulerAngles.y + openAngleDoor2, Doorhinge2.eulerAngles.z);

        uiElement.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            if (uiElement != null)
            {
                uiElement.SetActive(false); // Hide UI
            }
            // Start the coroutine to pull the rope and open the doors
            StartCoroutine(PullRopeAndOpenDoors());
        }
    }

    private IEnumerator PullRopeAndOpenDoors()
    {
        float elapsedTime = 0f;

        // Pulling the rope down
        while (elapsedTime < 1f)
        {
            RopeHandle.position = Vector3.Lerp(originalRopePosition, pulledRopePosition, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the rope reaches the final pulled position
        RopeHandle.position = pulledRopePosition;

        // Wait a bit before opening the doors
        yield return new WaitForSeconds(1.5f);

        // Opening the doors
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * Speed);
            Doorhinge2.rotation = Quaternion.Slerp(closedRotationDoor2, openRotationDoor2, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure doors reach the final open position
        Doorhinge1.rotation = openRotationDoor1;
        Doorhinge2.rotation = openRotationDoor2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiElement != null)
            {
                inRange = true;
                uiElement.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiElement != null)
            {
                inRange = false;
                uiElement.SetActive(false);
            }
        }
    }
}
