using UnityEngine;
using System.Collections;

public class LeverSystem : MonoBehaviour
{
    public Transform LeverHandle;
    public Transform Doorhinge1;
    public Transform Doorhinge2;
    public float openAngleLever = 45f;
    public float openAngleDoor1 = 90f;
    public float openAngleDoor2 = 90f;
    public float Speed = 2f;
    private bool isOpen = false;
    private bool inRange = false;
    private Quaternion closedRotationLever;
    private Quaternion openRotationLever;
    private Quaternion openRotationDoor1;
    private Quaternion openRotationDoor2;
    private Quaternion closedRotationDoor1;
    private Quaternion closedRotationDoor2;
    public GameObject uiElement;

    void Start()
    {
        // Store the original rotation of the doors and levers
        closedRotationLever = LeverHandle.rotation;

        // Adjust the axis of rotation to move the lever up and down
        openRotationLever = Quaternion.Euler(LeverHandle.eulerAngles.x + openAngleLever, LeverHandle.eulerAngles.y, LeverHandle.eulerAngles.z);

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
                uiElement.SetActive(false); // UI popup logic
            }
            // Start the coroutine when the lever is activated
            StartCoroutine(ActivateLeverAndDoors());
        }
    }

    private IEnumerator ActivateLeverAndDoors()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            LeverHandle.rotation = Quaternion.Slerp(closedRotationLever, openRotationLever, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure lever reaches the final rotation
        LeverHandle.rotation = openRotationLever;

        // Wait a bit if you want to add a delay before the doors start opening
        yield return new WaitForSeconds(0.5f);

        // Opening the doors
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * Speed);
            Doorhinge2.rotation = Quaternion.Slerp(closedRotationDoor2, openRotationDoor2, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure doors reach the final rotation
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
