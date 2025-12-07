using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 1.0f;  // Speed
    public float floatAmplitude = 0.5f;  // distance

    private Vector3 startPosition;

    void Start()
    {
        // start pos
        startPosition = transform.position;
    }

    void Update()
    {
        // cal new Y position 
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Update the position of the object
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

