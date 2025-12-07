using System.Collections;
using UnityEngine;

public class PlaceABox : MonoBehaviour
{
    private bool inRange = false;
    public GameObject uiElement;
    public GameObject uiElementWarning;
    public GameObject Box; 
    public Transform BoxPosition; 
    private GameObject instantiatedBox;
    public static bool placeBox = false;

    private void Start()
    {
        if (uiElement != null && uiElementWarning != null)
        {
            uiElement.SetActive(false);
            uiElementWarning.SetActive(false);
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (GetBox.hasBox == true)
            {
                StartCoroutine(PlaceBox());
            }
            else
            {
                StartCoroutine(ShowWarning());
            }
        }
    }


    private IEnumerator ShowWarning()
    {
        if (uiElementWarning != null)
        {
            uiElementWarning.SetActive(true); //warning 
            yield return new WaitForSeconds(3f);
            uiElementWarning.SetActive(false); //hide warning
        }
    }

    private IEnumerator PlaceBox()
    {
        instantiatedBox = Instantiate(Box, BoxPosition.position, BoxPosition.rotation);
        placeBox = true;
        yield return null;
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
