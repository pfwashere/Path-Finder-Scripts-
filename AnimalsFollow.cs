using System.Collections;
using UnityEngine;
using UnityEngine.AI; 

public class CageAndAnimalSystem : MonoBehaviour
{
    public Transform DoorHinge;         
    public Transform Player;          
    public GameObject Animal;          
    public float OpenAngleDoor = 90f;  
    public float DoorSpeed = 2f;     
    public float FollowStoppingDistance = 1.5f; 
    public GameObject UIElement;      
    private bool inRange = false;      
    private bool isDoorOpen = false;  
    private Quaternion closedRotationDoor; 
    private Quaternion openRotationDoor;  
    private NavMeshAgent animalAgent;  

    void Start()
    {
          
        closedRotationDoor = DoorHinge.rotation;
        openRotationDoor = Quaternion.Euler(DoorHinge.eulerAngles.x, DoorHinge.eulerAngles.y + OpenAngleDoor, DoorHinge.eulerAngles.z);

        if (UIElement != null) UIElement.SetActive(false);

        // Get NavMeshAgent ให้แมว
        animalAgent = Animal.GetComponent<NavMeshAgent>();
        if (animalAgent == null)
        {
            Debug.LogError("Animal GameObject must have a NavMeshAgent component!");
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !isDoorOpen)
        {
            isDoorOpen = true;
            if (UIElement != null) UIElement.SetActive(false);
            StartCoroutine(OpenDoorAndStartFollowing());
        }

        if (isDoorOpen && animalAgent != null)
        {
            float distance = Vector3.Distance(Animal.transform.position, Player.position);
            if (distance > FollowStoppingDistance)
            {
                animalAgent.SetDestination(Player.position); 
            }
            else
            {
                animalAgent.ResetPath();
            }
        }
    }

    private IEnumerator OpenDoorAndStartFollowing()
    {
    
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            DoorHinge.rotation = Quaternion.Slerp(closedRotationDoor, openRotationDoor, elapsedTime * DoorSpeed);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        DoorHinge.rotation = openRotationDoor; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            if (UIElement != null) UIElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (UIElement != null) UIElement.SetActive(false);
        }
    }
}

