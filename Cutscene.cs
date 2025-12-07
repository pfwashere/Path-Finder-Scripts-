using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.Play();
            Destroy(gameObject);  
        }
    }
}
