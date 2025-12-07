using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCage : MonoBehaviour
{
    public static bool haskey = false;
    private bool inRange = false;

    public GameObject uiElement;
    public GameObject keyObject;

    private void Start()
    {
        uiElement.SetActive(false);
    }
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            haskey = true;
            Destroy(uiElement);
            keyObject.SetActive(false);
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
