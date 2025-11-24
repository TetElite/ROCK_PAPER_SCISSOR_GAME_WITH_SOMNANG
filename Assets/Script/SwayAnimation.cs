using UnityEngine;
using System.Collections;

public class SwayAnimation : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAngle = 5f; // How much to rotate
    public float swaySpeed = 1f; // How fast to sway
    public float startDelay = 0f; // Stagger the animations
    public bool useScaleSway = false; // Optional: Add scale animation too
    public float scaleSwayAmount = 0.1f; // How much to scale

    private Vector3 originalRotation;
    private Vector3 originalScale;

    void Start()
    {
        originalRotation = transform.eulerAngles;
        originalScale = transform.localScale;
        
        // Start the sway coroutine
        StartCoroutine(SwayRoutine());
    }

    IEnumerator SwayRoutine()
    {
        // Wait for initial delay to stagger animations
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // Sway to the right
            yield return StartCoroutine(SwayTo(swayAngle, 1f / swaySpeed));
            
            // Sway to the left
            yield return StartCoroutine(SwayTo(-swayAngle, 1f / swaySpeed));
            
            // Return to center
            yield return StartCoroutine(SwayTo(0f, 1f / swaySpeed));

            // Brief pause at center
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator SwayTo(float targetAngle, float duration)
    {
        float timer = 0f;
        Vector3 startRotation = transform.eulerAngles;
        Vector3 targetRotation = originalRotation + new Vector3(0, 0, targetAngle);
        
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = useScaleSway ? 
            originalScale * (1f + Mathf.Abs(targetAngle) * scaleSwayAmount / swayAngle) : 
            originalScale;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            
            // Smooth interpolation
            float smoothProgress = Mathf.SmoothStep(0f, 1f, progress);
            
            // Rotate
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, smoothProgress);
            
            // Scale (optional)
            if (useScaleSway)
            {
                transform.localScale = Vector3.Lerp(startScale, targetScale, smoothProgress);
            }
            
            yield return null;
        }

        // Ensure we reach the exact target values
        transform.eulerAngles = targetRotation;
        if (useScaleSway)
        {
            transform.localScale = targetScale;
        }
    }

    // Optional: Call this if you want to reset the animation
    public void ResetAnimation()
    {
        StopAllCoroutines();
        transform.eulerAngles = originalRotation;
        transform.localScale = originalScale;
        StartCoroutine(SwayRoutine());
    }
}