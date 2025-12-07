using UnityEngine;

public class TransformToSmaller : MonoBehaviour
{
    public Transform player;
    public float transitionSpeed = 2.0f;
    private bool isSmall = false;
    public GameObject TransformFX;

    public AudioClip TransformSFX;
    private AudioSource audioSource;
    public float effectOffset = 1.0f; // Distance behind the player

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = TransformSFX;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isSmall = !isSmall;
            TransformPlayer();
        }
    }

    private void TransformPlayer()
    {
        // Calculate position behind the player
        Vector3 effectPosition = player.position - player.forward * effectOffset;

        // Instantiate effect slightly behind the player
        GameObject fx = Instantiate(TransformFX, effectPosition, player.rotation);
        ParticleSystem explosionParticles = fx.GetComponent<ParticleSystem>();
        audioSource.Play();

        if (isSmall)
        {
            player.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            player.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
    }
}
