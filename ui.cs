using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : MonoBehaviour
{
    public GameObject ui;
    private void Start()
    {
        ui.SetActive(false);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {                        
           ui.SetActive(true);         
        }
    }
    private void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Plater"))
        {
            ui.SetActive(false);
        }
    }
}
