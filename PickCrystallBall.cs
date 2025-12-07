using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCrystallBall : MonoBehaviour
{
    public static bool hasyCrystallBall = false;
    public GameObject CrystallBall;
    public GameObject ui;
    public GameObject uipop;
    private bool inRange = false;
    public GameObject uiTask3Done;
    public GameObject uiTask3;
    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        uiTask3Done.SetActive(false);
        uiTask3.SetActive(true);
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && StatueConverStarter.hasTalkSt == true)
        {
            CrystallBall.SetActive(false);
            hasyCrystallBall = true;
            uiTask3.SetActive(false);
            uiTask3Done.SetActive(true);
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
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && hasyCrystallBall == false)
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
