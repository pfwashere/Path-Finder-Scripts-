using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetBomb : MonoBehaviour
{
    public static bool hasBomb = false; // Track if the player has the items
    private bool inRange = false;

    // Reference to the UI element to hide when the key is collected
    public GameObject uiElement;
    public GameObject itemObject;

    private void Start()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }
    private void Update()   
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            hasBomb = true;
            Destroy(uiElement);
            Destroy(itemObject);
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
