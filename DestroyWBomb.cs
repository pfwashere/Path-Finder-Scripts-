using System.Collections;
using UnityEngine;

public class DestroyWBomb : MonoBehaviour
{
    private bool inRange = false;
    public GameObject uiElement;
    public GameObject uiElementWarning;
    public GameObject MainObject;

    // Array to manage rocks
    public GameObject[] rocks;

    public GameObject Dynamite; // Dynamite prefab
    public Transform DynamitePosition; // Where the dynamite will appear
    public Transform explosionEffectsPosition;
    private GameObject instantiatedDynamite; // Store the instance of the placed dynamite
    public GameObject explosionEffect;

    public AudioClip explosionSound;  // Explosion sound effect
    private AudioSource audioSource;

    private void Start()
    {
        if (uiElement != null && uiElementWarning != null)
        {
            uiElement.SetActive(false);
            uiElementWarning.SetActive(false);
        }

        // Set up the AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = explosionSound;
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (GetBomb.hasBomb)
            {
                StartCoroutine(HandleExplosion());
            }
            else
            {
                // Start a coroutine to display the warning message
                StartCoroutine(ShowWarning());
            }
        }
    }

    // Coroutine to show warning for 3 seconds
    private IEnumerator ShowWarning()
    {
        if (uiElementWarning != null)
        {
            uiElementWarning.SetActive(true); // Show the warning UI
            yield return new WaitForSeconds(3f); // Wait for 3 seconds
            uiElementWarning.SetActive(false); // Hide the warning UI
        }
    }

    private IEnumerator HandleExplosion()
    {
        // Place the dynamite at the specified position
        if (Dynamite != null && DynamitePosition != null)
        {
            instantiatedDynamite = Instantiate(Dynamite, DynamitePosition.position, DynamitePosition.rotation);
        }

        // Wait for 1.5 seconds (simulating fuse time)
        yield return new WaitForSeconds(1.5f);

        // Destroy UI element and the main object
        if (uiElement != null) Destroy(uiElement);
        if (MainObject != null) Destroy(MainObject);

        // Instantiate the explosion effect
        GameObject explosion = Instantiate(explosionEffect, explosionEffectsPosition.position, explosionEffectsPosition.rotation);

        // Get the Particle System component and stop it after it plays
        ParticleSystem explosionParticles = explosion.GetComponent<ParticleSystem>();

        // Play the explosion sound
        if (audioSource != null && explosionSound != null)
        {
            audioSource.Play();
        }

        // Apply physics to rocks
        foreach (GameObject rock in rocks)
        {
            if (rock != null)
            {
                rock.AddComponent<Rigidbody>();
            }
        }

        if (explosionParticles != null)
        {
            yield return new WaitForSeconds(explosionParticles.main.duration);
            Destroy(explosion);
        }

        if (instantiatedDynamite != null) Destroy(instantiatedDynamite);

        Debug.Log("Boom! Rocks are now affected by physics.");
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
