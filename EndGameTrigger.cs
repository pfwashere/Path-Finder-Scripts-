using UnityEngine;
using UnityEngine.UI;  

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endGameUI; 

    private void Start()
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(false);  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);  
            Time.timeScale = 0;         
        }
    }

   
    public void RetryGame()
    {
        endGameUI.SetActive(false);
        Time.timeScale = 1; 
     
    }
}

