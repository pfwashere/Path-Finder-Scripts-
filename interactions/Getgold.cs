using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getgold : MonoBehaviour
{
    public static bool hasGold = false;
    public GameObject gold;
    public GameObject ui;
    public GameObject uipop;
    public GameObject uiTask1;
    public GameObject uiTask1Done;
    private bool inRange = false;

    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);       
    }

    void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            gold.SetActive(false);
            hasGold = true;
            Destroy(ui);
            StartCoroutine(ShowUiPop());
        }
        //else if (inRange == true && Input.GetKeyDown(KeyCode.E) && StatueConverStarter.hasTalkSt == true && hasGold == false)
        //{
        //    gold.SetActive(false);
        //    hasGold = true;
        //    uiTask1.SetActive(false);
        //    uiTask1Done.SetActive(true);
        //}
    }

    private IEnumerator ShowUiPop()
    {
        uipop.SetActive(true);
        yield return new WaitForSeconds(4);
        uipop.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Converstion1Starter.hasTalkLm == true && hasGold == false)
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
