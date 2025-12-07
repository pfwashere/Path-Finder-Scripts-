using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceG : MonoBehaviour
{
    public static bool hasplaceG = false;
    public GameObject Gplaced;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;
    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        Gplaced.SetActive(false);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            Gplaced.SetActive(true);
            hasplaceG = true;
            Destroy(ui);
            Getgold.hasGold = false;
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
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && Getgold.hasGold == true)
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
