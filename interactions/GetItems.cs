using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Getitems : MonoBehaviour
{
    public static bool haskey = false; // Track if the player has the key
    private bool inRange = false;

    // Reference to the UI element to hide when the key is collected
    public GameObject uiElement;
    public GameObject item;

    private void Start()
    {
        // Make sure the UI element is initially inactive
        if (uiElement != null)
        {
            uiElement.SetActive(false);

        }
    }
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {           
            Destroy(uiElement);
            Destroy(item);
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
