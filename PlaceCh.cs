using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCh : MonoBehaviour
{
    public static bool hasplacech = false;
    public GameObject chplaced;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;
    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        chplaced.SetActive(false);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            chplaced.SetActive(true);
            hasplacech = true;
            Destroy(ui);
            PickChicken.hasChicken = false;
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
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && PickChicken.hasChicken == true)
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
