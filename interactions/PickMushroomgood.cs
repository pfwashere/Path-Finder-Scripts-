using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMushroomgood : MonoBehaviour
{
    public static bool hasylwmushroom = false;
    public GameObject ylwmushroom;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;
    public GameObject uiTask2Done;
    public GameObject uiTask2;
    void Start()
    {   
        ui.SetActive(false);
        uipop.SetActive(false);
        uiTask2Done.SetActive(false);
        uiTask2.SetActive(true);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && StatueConverStarter.hasTalkSt == true)
        {
            ylwmushroom.SetActive(false);
            hasylwmushroom = true;
            uiTask2.SetActive(false);
            uiTask2Done.SetActive(true);
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
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && hasylwmushroom == false)
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
