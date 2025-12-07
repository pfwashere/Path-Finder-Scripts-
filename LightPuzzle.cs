using UnityEngine;
using System.Collections;

public class LightPuzzle : MonoBehaviour
{
    public GameObject Light1; 
    public GameObject Light2;
    public GameObject Light3; 

    public Transform Doorhinge1;  
    public Transform Doorhinge2;  
    public float openAngleDoor1 = 90f;  
    public float openAngleDoor2 = 90f;  
    public float doorOpenSpeed = 2f;    

    public Transform Lever1; 
    public Transform Lever2;  
    public Transform Lever3; 
    public float leverMoveAngle = 45f;  

    public GameObject uiElement;       
    public GameObject uiElementOn1;   
    public GameObject uiElementOn2;     
    public GameObject uiElementOn3;    

    private bool lever1Activated = false;
    private bool lever2Activated = false;
    private bool lever3Activated = false;

    private bool light1On = false;
    private bool light2On = false;
    private bool light3On = false;
    private bool inRange = false;
    private bool doorsOpen = false;

    private Quaternion closedRotationDoor1;
    private Quaternion openRotationDoor1;
    private Quaternion closedRotationDoor2;
    private Quaternion openRotationDoor2;

    void Start()
    {
        UpdateLights();

        closedRotationDoor1 = Doorhinge1.rotation;
        openRotationDoor1 = Quaternion.Euler(Doorhinge1.eulerAngles.x, Doorhinge1.eulerAngles.y + openAngleDoor1, Doorhinge1.eulerAngles.z);

        closedRotationDoor2 = Doorhinge2.rotation;
        openRotationDoor2 = Quaternion.Euler(Doorhinge2.eulerAngles.x, Doorhinge2.eulerAngles.y + openAngleDoor2, Doorhinge2.eulerAngles.z);

        uiElement.SetActive(false);  
        uiElementOn1.SetActive(false);
        uiElementOn2.SetActive(false);
        uiElementOn3.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Alpha1))  
        {
            PullLever1();
        }
        if (inRange && Input.GetKeyDown(KeyCode.Alpha2))  
        {
            PullLever2();
        }
        if (inRange && Input.GetKeyDown(KeyCode.Alpha3))  
        {
            PullLever3();
        }
    }

    public void PullLever1()
    {
        light1On = !light1On;  
        light2On = !light2On;  
        MoveLever(Lever1, ref lever1Activated);
        UpdateLights();
    }

    public void PullLever2()
    {
        light1On = !light1On; 
        light3On = !light3On;  
        MoveLever(Lever2, ref lever2Activated);
        UpdateLights();
    }

    public void PullLever3()
    {
        light2On = !light2On; 
        MoveLever(Lever3, ref lever3Activated);
        UpdateLights();
    }


    void MoveLever(Transform lever, ref bool leverActivated)
    {
        if (!leverActivated)
        {
            
            lever.Rotate(Vector3.right * leverMoveAngle);
            leverActivated = true;
        }
        else
        {
            
            lever.Rotate(Vector3.right * -leverMoveAngle);
            leverActivated = false;
        }
    }

   
    void UpdateLights()
    {
        Light1.SetActive(light1On);  
        Light2.SetActive(light2On);  
        Light3.SetActive(light3On);  

        uiElementOn1.SetActive(light1On); 
        uiElementOn2.SetActive(light2On);  
        uiElementOn3.SetActive(light3On);  


        if (light1On && light2On && light3On)
        {
            if (!doorsOpen)
            {
                StartCoroutine(OpenDoors()); 
            }
        }
        else
        {
            if (doorsOpen)
            {
                StartCoroutine(CloseDoors());  
            }
        }
    }

    private IEnumerator OpenDoors()
    {
        Debug.Log("Opening doors...");
        doorsOpen = true;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(closedRotationDoor1, openRotationDoor1, elapsedTime * doorOpenSpeed);
            Doorhinge2.rotation = Quaternion.Slerp(closedRotationDoor2, openRotationDoor2, elapsedTime * doorOpenSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Doorhinge1.rotation = openRotationDoor1;
        Doorhinge2.rotation = openRotationDoor2;
    }

    private IEnumerator CloseDoors()
    {
        Debug.Log("Closing doors...");
        doorsOpen = false;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Doorhinge1.rotation = Quaternion.Slerp(openRotationDoor1, closedRotationDoor1, elapsedTime * doorOpenSpeed);
            Doorhinge2.rotation = Quaternion.Slerp(openRotationDoor2, closedRotationDoor2, elapsedTime * doorOpenSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Doorhinge1.rotation = closedRotationDoor1;
        Doorhinge2.rotation = closedRotationDoor2;
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
        if (uiElement != null)
        {
            inRange = false;
            uiElement.SetActive(false); 
        }
    }
}
