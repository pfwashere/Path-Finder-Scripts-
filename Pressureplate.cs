using UnityEngine;
using System.Collections;

public class PressurePlateSystem : MonoBehaviour
{
    public Transform PressurePlate; 
    public Transform DoorHinge1; 
    public Transform DoorHinge2; 
    public float openAngleDoor1 = 90f; 
    public float openAngleDoor2 = 90f; 
    public float Speed = 2f; // Speed door opening
    private bool isOpen = false; 
    private bool inRange = false; 
    private Quaternion closedRotationDoor1; 
    private Quaternion closedRotationDoor2; 
    private Quaternion openRotationDoor1; 
    private Quaternion openRotationDoor2; 
    public GameObject uiElementWarning;

    void Start()
    {
        // original rotation of the doors
        closedRotationDoor1 = DoorHinge1.rotation;
        openRotationDoor1 = Quaternion.Euler(DoorHinge1.eulerAngles.x, DoorHinge1.eulerAngles.y + openAngleDoor1, DoorHinge1.eulerAngles.z);
        closedRotationDoor2 = DoorHinge2.rotation;
        openRotationDoor2 = Quaternion.Euler(DoorHinge2.eulerAngles.x, DoorHinge2.eulerAngles.y + openAngleDoor2, DoorHinge2.eulerAngles.z);
        uiElementWarning.SetActive(false);
    }

    void Update()
    {
        if (inRange && PlaceABox.placeBox == true)
        {
            if (!isOpen)
            {
                StartCoroutine(ActivateDoors());
            }
        }
        if (inRange && PlaceABox.placeBox == false)
        {
            StartCoroutine(ShowWarning());
        }   
    }
    private IEnumerator ShowWarning()
    {
        if (uiElementWarning != null)
        {
            uiElementWarning.SetActive(true); // warning 
            yield return new WaitForSeconds(3f); 
            uiElementWarning.SetActive(false); // Hide warning 
        }
    }

    private IEnumerator ActivateDoors()
    {
        Debug.Log("Doors Opening!!");
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            DoorHinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * Speed);
            DoorHinge2.rotation = Quaternion.Slerp(closedRotationDoor2, openRotationDoor2, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        // ms doors reach the final rotation
        DoorHinge1.rotation = openRotationDoor1;
        DoorHinge2.rotation = openRotationDoor2;
        isOpen = true; 

        // delay
        yield return new WaitForSeconds(2f); 

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            DoorHinge1.rotation = Quaternion.Slerp(openRotationDoor1, closedRotationDoor1, elapsedTime * Speed);
            DoorHinge2.rotation = Quaternion.Slerp(openRotationDoor2, closedRotationDoor2, elapsedTime * Speed);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        DoorHinge1.rotation = closedRotationDoor1;
        DoorHinge2.rotation = closedRotationDoor2;
        isOpen = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}

