using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 1.0f;  // Speed of the floating motion
    public float floatAmplitude = 0.5f;  // How far it moves up and down

    private Vector3 startPosition;

    void Start()
    {
        // Save the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Update the position of the object
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
