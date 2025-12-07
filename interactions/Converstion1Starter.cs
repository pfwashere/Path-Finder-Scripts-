using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Converstion1Starter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public static bool hasTalkLm = false;
    public GameObject uiTalk;
    private bool inRange = false;

    private void Start()
    {
        uiTalk.SetActive(false);
    }

    private void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && hasTalkLm == false)
        {

            Destroy(uiTalk);
            ConversationManager.Instance.StartConversation(myConversation);
            hasTalkLm = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiTalk != null)
            {
                uiTalk.SetActive(true);
                inRange = true;
            }           
        }
    }
    private void OnTriggerExit(Collider other)
    {
    if (other.CompareTag("Player"))
        {
            if (uiTalk != null)
            {
                uiTalk.SetActive(false);
                inRange = false;
            }      
        }
    }
}
