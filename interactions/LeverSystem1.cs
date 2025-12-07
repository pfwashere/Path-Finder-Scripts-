using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class LeverSystem1 : MonoBehaviour
{
    public Transform LeverHandle;
    public Transform Doorhinge1;
    public float openAngleLever = 45f;
    public float openAngleDoor1 = 90f;
    public float Speed = 2f;
    private bool inRange = false;
    private Quaternion closedRotationLever;
    private Quaternion openRotationLever;
    private Quaternion openRotationDoor1;
    private Quaternion closedRotationDoor1;
    public GameObject uiElement;
    public GameObject uiWarning;   // Warning UI that will show for 3 seconds
    public GameObject AcidGasEffects; // Gas effects that will repeat every 5 seconds

    void Start()
    {
        // Store the original rotation of the doors and levers
        closedRotationLever = LeverHandle.rotation;

        // Adjust the axis of rotation to move the lever up and down
        openRotationLever = Quaternion.Euler(LeverHandle.eulerAngles.x + openAngleLever, LeverHandle.eulerAngles.y, LeverHandle.eulerAngles.z);

        closedRotationDoor1 = Doorhinge1.rotation;
        openRotationDoor1 = Quaternion.Euler(Doorhinge1.eulerAngles.x, Doorhinge1.eulerAngles.y + openAngleDoor1, Doorhinge1.eulerAngles.z);

        uiElement.SetActive(false);
        if (uiWarning != null) uiWarning.SetActive(false); // Hide warning initially
        if (AcidGasEffects != null) AcidGasEffects.SetActive(false); // Hide gas effects initially
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {       
            if (uiElement != null)
            {
                uiElement.SetActive(false); // Hide UI popup (range-based)
            }

            // Start the coroutine to activate the lever and doors
            StartCoroutine(ActivateLeverAndDoors());
            StartCoroutine(ShowWarning());
        }
    }

    private IEnumerator ActivateLeverAndDoors()
    {
        // Pulling the lever down
        Debug.Log("Pulling Down!!");
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
        Debug.Log("Door Opening!!");
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        Doorhinge1.rotation = openRotationDoor1;

    }

    private IEnumerator ShowWarning()
    {
        if (uiWarning != null)
        {
            uiWarning.SetActive(true); 
            yield return new WaitForSeconds(3f); 
            uiWarning.SetActive(false);
            yield return new WaitForSeconds(2f);
            StartCoroutine(ActivateGasEffects());

        }
    }

    private IEnumerator ActivateGasEffects()
    {
        while (true) // Repeat infinitely, you can stop it later if needed
        {
            if (AcidGasEffects != null && ButtonPush1.isPushed1 == false)
            {
                AcidGasEffects.SetActive(true); // Show the gas effects

                // Animate the gas moving down
                float elapsedTime = 0f;
                float duration = 3f; // Time it takes to move the gas down (same as how long it shows)
                Vector3 originalPosition = AcidGasEffects.transform.position;
                Vector3 targetPosition = originalPosition + new Vector3(0, 0, 0); // Move 5 units down (adjust as needed)

                // Move the gas down over 'duration' time
                while (elapsedTime < duration)
                {
                    AcidGasEffects.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
                    elapsedTime += Time.deltaTime;
                    yield return null; // Wait until the next frame
                }

                // Ensure the gas reaches the target position
                AcidGasEffects.transform.position = targetPosition;

                // Hide the gas effects after the movement
                AcidGasEffects.SetActive(false);

                // Optionally reset the position for the next loop
                AcidGasEffects.transform.position = originalPosition;
            }
            else
                break;

            yield return new WaitForSeconds(5f); 
        }
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
