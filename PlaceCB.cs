using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCB : MonoBehaviour
{
    public static bool hasplacecb = false;
    public GameObject cbplaced;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;
    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        cbplaced.SetActive(false);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            cbplaced.SetActive(true);
            hasplacecb = true;
            Destroy(ui);
            StartCoroutine(ShowUiPop());
            PickCrystallBall.hasyCrystallBall = false;
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
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && PickCrystallBall.hasyCrystallBall == true)
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

