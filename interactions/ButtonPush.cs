using UnityEngine;
using System.Collections;
using TMPro;

public class ButtonSystem : MonoBehaviour
{
    public Transform Button;
    public Transform Doorhinge1;
    public Transform Doorhinge2;
    public float buttonPressDistance = 0.2f; // How far the button moves when pressed
    public float openAngleDoor1 = 90f;
    public float openAngleDoor2 = 90f;
    public float speed = 2f;
    public float doorOpenTime = 10f; // Time the doors remain open
    private bool isPushed = false;
    private bool inRange = false;
    private Vector3 originalButtonPosition;
    private Vector3 pressedButtonPosition;
    private Quaternion closedRotationDoor1;
    private Quaternion closedRotationDoor2;
    private Quaternion openRotationDoor1;
    private Quaternion openRotationDoor2;
    public GameObject uiElement;
    public GameObject uiTimer; 
    public TMP_Text timerText; 
    public float countdownTime = 3f; 

    private float timeRemaining;
    private bool timerIsRunning = false;

    void Start()
    {
        originalButtonPosition = Button.position;
        pressedButtonPosition = Button.position + Button.forward * buttonPressDistance;

        closedRotationDoor1 = Doorhinge1.rotation;
        closedRotationDoor2 = Doorhinge2.rotation;
        openRotationDoor1 = Quaternion.Euler(Doorhinge1.eulerAngles.x, Doorhinge1.eulerAngles.y + openAngleDoor1, Doorhinge1.eulerAngles.z);
        openRotationDoor2 = Quaternion.Euler(Doorhinge2.eulerAngles.x, Doorhinge2.eulerAngles.y + openAngleDoor2, Doorhinge2.eulerAngles.z);

        uiElement.SetActive(false);
        uiTimer.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !isPushed)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false); 
            }

            StartCoroutine(ActivateButtonAndDoors());
        }
    }
    public void StartTimer()
    {
        timeRemaining = countdownTime;
        timerIsRunning = true;
        if (uiTimer != null)
        {
            uiTimer.SetActive(true);
        }
        StartCoroutine(TimerCountdown());
    }

    private IEnumerator TimerCountdown()
    {
        while (timerIsRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; 
            UpdateTimerUI(timeRemaining); 
            yield return null;
        }

        timerIsRunning = false;
        if (uiTimer != null)
        {
            uiTimer.SetActive(false); 
        }
    }

    private void UpdateTimerUI(float time)
    {
        timerText.text = Mathf.Clamp(time, 0, countdownTime).ToString("F2"); // e.g., 9.99
    }

    private IEnumerator ActivateButtonAndDoors()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Button.position = Vector3.Lerp(originalButtonPosition, pressedButtonPosition, elapsedTime * speed);
            elapsedTime += Time.deltaTime;          
            yield return null;
        }
        Button.position = pressedButtonPosition;
        
        StartTimer();

        isPushed = true;
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * speed);
            Doorhinge2.rotation = Quaternion.Slerp(closedRotationDoor2, openRotationDoor2, elapsedTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Doorhinge1.rotation = openRotationDoor1;
        Doorhinge2.rotation = openRotationDoor2;
      
        yield return new WaitForSeconds(doorOpenTime);

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(openRotationDoor1, closedRotationDoor1, elapsedTime * speed);
            Doorhinge2.rotation = Quaternion.Slerp(openRotationDoor2, closedRotationDoor2, elapsedTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Doorhinge1.rotation = closedRotationDoor1;
        Doorhinge2.rotation = closedRotationDoor2;

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Button.position = Vector3.Lerp(pressedButtonPosition, originalButtonPosition, elapsedTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Button.position = originalButtonPosition;

        isPushed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiElement != null)
            {
                inRange = true;
                uiElement.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiElement != null)
            {
                inRange = false;
                uiElement.SetActive(false);
            }
        }
    }
}
