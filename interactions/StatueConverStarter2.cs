using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueConverStarter2 : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public static bool hasTalkSt2 = false;
    private bool inRange = false;
    public GameObject uiTask;
    public GameObject ui;
    public GameObject RainFx;
    public GameObject SmokeToSky;
    public GameObject DustExplosion;
    public GameObject MistEffect;
    public GameObject FireFlies;
    private bool smokePlayed = false;
    private bool dustPlayed = false;

    private bool effectsPlayed = false;

    public Light sceneLight; // Reference to the scene light
    public float darkenDuration = 3f; // Time it takes to darken the light
    private float originalLightIntensity; // Store the original intensity of the light


    void Start()
    {
        uiTask.SetActive(false);
        if (MistEffect != null) MistEffect.SetActive(true); // Hide mist initially
        
        if (sceneLight != null)
        {
            originalLightIntensity = sceneLight.intensity;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange == true)
        {
            Destroy(ui);
            hasTalkSt2 = true;
            ConversationManager.Instance.StartConversation(myConversation);
            StartCoroutine(PlayEffectsSequence());
            StartCoroutine(uiTaskDisappear());
        }
    }

    private IEnumerator uiTaskDisappear()
    {
        yield return new WaitForSeconds(1);
        uiTask.SetActive(false);
    }

    private IEnumerator PlayEffectsSequence()
    {
        if (!effectsPlayed)
        {
            effectsPlayed = true;

            if (!smokePlayed && SmokeToSky != null)
            {
                yield return new WaitForSeconds(4.5f); // Wait for smoke to rise
                SmokeToSky.SetActive(true);
                smokePlayed = true;
                
            }

            // Play DustExplosion once after smoke
            if (!dustPlayed && DustExplosion != null)
            {
                yield return new WaitForSeconds(3); // Wait for dust to explode
                DustExplosion.SetActive(true);
                dustPlayed = true;
                
            }

            // Play RainFX for 2 seconds after dust
            if (RainFx != null)
            {
                yield return new WaitForSeconds(2.5f);
                RainFx.SetActive(true);
            }

            // After RainFx, enable MistEffect and start fade-out
            yield return new WaitForSeconds(4);
            if (MistEffect != null)
            {
                MistEffect.SetActive(true);
                StartCoroutine(FadeOutMist());
            }


            StartCoroutine(DarkenLight());

        }
    }
    private IEnumerator DarkenLight()
    {
        if (sceneLight != null)
        {
            float elapsedTime = 0f;
            float startIntensity = sceneLight.intensity;
            float targetIntensity = 0.2f; // Target intensity for a darker effect

            while (elapsedTime < darkenDuration)
            {
                sceneLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / darkenDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            sceneLight.intensity = targetIntensity; // Ensure the target intensity is set
        }
    }


    private IEnumerator FadeOutMist()
    {
        ParticleSystem mist = MistEffect.GetComponent<ParticleSystem>();
        if (mist != null)
        {
            var main = mist.main;
            float startOpacity = main.startColor.color.a;
            float fadeDuration = 2f; // Time to fade out
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float newAlpha = Mathf.Lerp(startOpacity, 0, elapsedTime / fadeDuration);
                main.startColor = new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, newAlpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            mist.Stop(); // Stop the mist effect after fading out
        }
        StartCoroutine(FrieFliesFadeIn());
    }
    private IEnumerator FrieFliesFadeIn()
    {
        yield return new WaitForSeconds(5);
        MistEffect.SetActive(false); // Disable the mist
        yield return new WaitForSeconds(1); // Wait briefly before starting fade-in

        if (FireFlies != null)
        {
            FireFlies.SetActive(true); // Activate the FireFlies object

            ParticleSystem firefliesSystem = FireFlies.GetComponent<ParticleSystem>();
            if (firefliesSystem != null)
            {
                var main = firefliesSystem.main;
                float startOpacity = 0f;
                float targetOpacity = 1f;
                float fadeDuration = 3f; // Time to fully fade in
                float elapsedTime = 0f;

                // Assuming FireFlies uses a material with startColor to control alpha
                while (elapsedTime < fadeDuration)
                {
                    float newAlpha = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / fadeDuration);
                    main.startColor = new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, newAlpha);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Ensure it's fully visible
                main.startColor = new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, targetOpacity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlaceCB.hasplacecb == true && PlaceG.hasplaceG == true  && hasTalkSt2 == false)
        {
            ui.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Converstion1Starter.hasTalkLm == false && other.CompareTag("Player"))
        {
            ui.SetActive(false);
            inRange = false;
        }
    }
}
