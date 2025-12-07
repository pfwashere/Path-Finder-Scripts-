using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class obbybutton : MonoBehaviour
{
    public Transform Button;
    public float buttonPressDistance = -0.03f; // How far the button moves when pressed
    public float speed = 2f;
    public static bool isPushed1 = false;
    private bool inRange = false;
    private Vector3 originalButtonPosition;
    private Vector3 pressedButtonPosition;
    public GameObject uiElement;

    public GameObject ObbyPlatFormsAll;
    public GameObject ObbyPlatForms1;
    public GameObject ObbyPlatForms2;

    void Start()
    {
        originalButtonPosition = Button.position;
        pressedButtonPosition = Button.position + Button.forward * buttonPressDistance;

        uiElement.SetActive(false);
        ObbyPlatFormsAll.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            isPushed1 = true;
            if (uiElement != null)
            {
                Destroy(uiElement);
            }

            StartCoroutine(ActivateButtonAndObbys());
        }
    }

    private IEnumerator ActivateButtonAndObbys()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Button.position = Vector3.Lerp(originalButtonPosition, pressedButtonPosition, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }
        Button.position = pressedButtonPosition;

        // Start the Obby platform activation after button press
        StartCoroutine(ActivateObbys());
    }

    private IEnumerator ActivateObbys()
    {
        ObbyPlatFormsAll.SetActive(true);
        while (true)
        {
            ObbyPlatForms2.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            ObbyPlatForms1.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            ObbyPlatForms2.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            ObbyPlatForms1.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            ObbyPlatForms2.SetActive(false);
            yield return new WaitForSeconds(1.5f);
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
