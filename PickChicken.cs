using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickChicken : MonoBehaviour
{
    public static bool hasChicken = false;
    public GameObject Chicken;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;
    public GameObject uiTask4Done;
    public GameObject uiTask4;
    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        uiTask4Done.SetActive(false);
        uiTask4.SetActive(true);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && StatueConverStarter.hasTalkSt == true)
        {
            Chicken.SetActive(false);
            hasChicken = true;
            uiTask4.SetActive(false);
            uiTask4Done.SetActive(true);
            Destroy(ui);
            StartCoroutine(ShowUiPop());
        }
    }

    private IEnumerator ShowUiPop()
    {
        uipop.SetActive(true);
        yield return new WaitForSeconds(4);
        uipop.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && hasChicken == false)
        {
            inRange = true;
            ui.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            ui.SetActive(false);
        }
    }
}

