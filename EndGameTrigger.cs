using UnityEngine;
using UnityEngine.UI;  // Needed for UI interactions

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endGameUI;  // Assign the End Game UI in the Inspector

    private void Start()
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(false);  // Hide the end game UI at the start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the collider is the player
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);  // Show the end game UI
            Time.timeScale = 0;         // Pause the game
        }
    }

    // Add this method if you want to resume or restart the game
    public void RetryGame()
    {
        endGameUI.SetActive(false);
        Time.timeScale = 1;  // Resume the game
        // Add any additional code for resetting the game state
    }
}
