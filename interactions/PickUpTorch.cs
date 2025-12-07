using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetTorch : MonoBehaviour
{
    private bool inRange = false;
    public GameObject uiElement;
    public GameObject itemObject;
    public GameObject itemObjectHold;

    private void Start()
    {
            uiElement.SetActive(false);
            itemObjectHold.SetActive(false);
    }
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            itemObjectHold.SetActive(true);
            Destroy(uiElement);
            Destroy(itemObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                inRange = true;
                uiElement.SetActive(true);
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
