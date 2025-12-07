using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PickMushroombad : MonoBehaviour
{
    public static bool haspsnmushroom = false;
    public GameObject psnmushroom;
    public GameObject ui;
    public GameObject uipop;
    public GameObject DeadScene; // Assuming this is your "Game Over" screen or equivalent
    public VideoPlayer deathVideo; // Reference to the VideoPlayer component on Raw Image
    private bool inRange = false;

    void Start()
    {
        ui.SetActive(false);
        uipop.SetActive(false);
        DeadScene.SetActive(false); 
        if (deathVideo != null)
        {
            deathVideo.gameObject.SetActive(false); // Ensure video UI is hidden initially
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && StatueConverStarter.hasTalkSt == true)
        {
            psnmushroom.SetActive(false);
            haspsnmushroom = true;           
            Destroy(ui);
            StartCoroutine(ShowUiPopAndPlayVideo());
        }
    }

    private IEnumerator ShowUiPopAndPlayVideo()
    {
        uipop.SetActive(true);
        yield return new WaitForSeconds(4);
        uipop.SetActive(false);

        // Wait another 5 seconds, then play the video
        yield return new WaitForSeconds(5);

        if (deathVideo != null)
        {
            deathVideo.gameObject.SetActive(true); // Show video UI
            deathVideo.Play(); // Start playing the video
        }   

        // Optional: Hide the video UI after it's done playing (if you have a loop, it won't end)
        yield return new WaitForSeconds((float)deathVideo.length);
        deathVideo.gameObject.SetActive(false);

        // Show the DeadScene UI if desired
        if (DeadScene != null)
        {
            DeadScene.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StatueConverStarter.hasTalkSt == true && haspsnmushroom == false)
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
