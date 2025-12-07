using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarningUi : MonoBehaviour
{
    private bool inRange = false;
    public GameObject uiElementWarning;

    void Start()
    {
        uiElementWarning.SetActive(false);
    }
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (uiElementWarning != null)
            {
                uiElementWarning.SetActive(false); // Hide UI popup
            }
            StartCoroutine(ShowWarningAndActivateGas());
        }
    }
    private IEnumerator ShowWarningAndActivateGas()
    {
        new WaitForSeconds(1f);
        uiElementWarning.SetActive(true);
        yield return new WaitForSeconds(3f);
        uiElementWarning.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           inRange = false;
        }
    }
}
