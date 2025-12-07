using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class StatueConverStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public static bool hasTalkSt = false;
    private bool inRange = false;
    public GameObject uiTask;
    public GameObject ui;

    void Start()
    {
        uiTask.SetActive(false);       
    }
    void Update()
    {         
       if (Input.GetKeyDown(KeyCode.E) && Converstion1Starter.hasTalkLm == true && inRange == true && hasTalkSt == false)
       {          
           Destroy(ui);
           hasTalkSt = true;
           ConversationManager.Instance.StartConversation(myConversation);
       }
    }
    
    private void OnTriggerEnter(Collider other)
        
    {
        if (Converstion1Starter.hasTalkLm == true && other.CompareTag("Player"))
        {
            ui.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Converstion1Starter.hasTalkLm == false && other.CompareTag("Player"))
        {
            ui.SetActive(false);
            inRange = false;
        }
    }

}
