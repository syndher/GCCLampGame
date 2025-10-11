using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lights : MonoBehaviour
{

    [SerializeField] private Light2D initialLight;  
    [SerializeField] private Light2D finalLight; 

    [SerializeField] private float fadeSpeed = 1.5f;
    private bool hasActivated = false;
    public static int activatedLights = 0;

    void Start()
    {

        if (initialLight != null) initialLight.enabled = true;
        if (finalLight != null)
        {
            finalLight.enabled = true;
            finalLight.intensity = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;

            activatedLights++;

            StartCoroutine(FadeInFinalLight());
        }
    }

    private System.Collections.IEnumerator FadeInFinalLight()
    {
        float targetIntensity = finalLight.intensity > 0 ? finalLight.intensity : 1.5f;
        finalLight.intensity = 0f;

        while (finalLight.intensity < targetIntensity)
        {
            finalLight.intensity = Mathf.MoveTowards(finalLight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        // fully replace lights
        if (initialLight != null) initialLight.enabled = false;
        finalLight.intensity = targetIntensity;
    }
}
