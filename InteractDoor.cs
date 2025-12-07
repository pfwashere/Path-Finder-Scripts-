using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    public Transform doorHinge;  // Assign the door's hinge point or rotation center
    public float openAngle = 90f; // Angle to open the door
    public float doorSpeed = 2f;  // Speed of door opening/closing
    private bool isOpen = false;
    private bool inRange = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public GameObject uiElement;
    public GameObject uiElementWarning;

    void Start()
    {
        // Store the original rotation of the door
        closedRotation = doorHinge.rotation;
        openRotation = Quaternion.Euler(doorHinge.eulerAngles.x, doorHinge.eulerAngles.y + openAngle, doorHinge.eulerAngles.z);
        uiElement.SetActive(false);
        uiElementWarning.SetActive(false);        
    }

    void Update()
    {
        {          
            if (inRange && Input.GetKeyDown(KeyCode.E) && GetKey.haskey == true)
            {
                isOpen = !isOpen; // Toggle door state
            }
            else if (inRange && Input.GetKeyDown(KeyCode.E) && GetKey.haskey == false)
            {
                StartCoroutine(ShowWarning());
            }
        }
        // Smoothly rotate the door
        if (isOpen)
        {
            Debug.Log("Opening Door!!");
            doorHinge.rotation = Quaternion.Slerp(doorHinge.rotation, openRotation, Time.deltaTime * doorSpeed);
        }
        else
        {
            doorHinge.rotation = Quaternion.Slerp(doorHinge.rotation, closedRotation, Time.deltaTime * doorSpeed);

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