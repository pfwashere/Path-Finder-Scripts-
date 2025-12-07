using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool isRunning = true;
    public TMP_Text timetext;

    void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);

            }
        }
    }
    void DisplayTime (float timetoDisplay)
    {
        timetoDisplay += 1;
        float Seconds = Mathf.FloorToInt(timetoDisplay % 60);
        timetext.text = string.Format("{1.00}" ,Seconds);
        
    }
}
