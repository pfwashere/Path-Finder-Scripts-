using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeTorch : MonoBehaviour
{
    public GameObject TorchObject;
    private bool inRange = false;
    
    void Update ()
    {
        if (inRange == true)
        {
            Destroy(TorchObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           inRange = true;
        }
    }
}
