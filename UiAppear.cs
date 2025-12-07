using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public GameObject uiElement; // Assign the UI element in the Inspector
    public GameObject item;

    private void Start()
    {
        // Make sure the UI element is initially inactive
        if (uiElement != null)
        {
            uiElement.SetActive(false);

        }
    }

    // When the player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the object is tagged as "Player"
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true); // Show the UI element
            }
        }
    }

    // When the player exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiElement  != null && item == null)
            {
                uiElement.SetActive(false); // Hide the UI element
                            }
        }
    }
}
