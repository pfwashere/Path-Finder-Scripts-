using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMr : MonoBehaviour
{
    public static bool hasplaceylwmushroom = false;
    public GameObject ylwmushroomplaced;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;    
    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        ylwmushroomplaced.SetActive(false);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            ylwmushroomplaced.SetActive(true);
            hasplaceylwmushroom = true;
            Destroy(ui);
            PickMushroomgood.hasylwmushroom = false ;   
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
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && PickMushroomgood.hasylwmushroom == true)
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
