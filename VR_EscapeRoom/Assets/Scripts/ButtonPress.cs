using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ButtonPress : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private bool isAnimating = false;
    [SerializeField] private float pressDepth = 0.1f;
    [SerializeField] private float pressSpeed = 0.5f;
    [SerializeField] private float holdDuration = 0.0f;
    public Animator doorAnimator;
    public AudioSource doorSound; // Added audio source reference
    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnButtonPress()
    {
        if (isAnimating) return;

        StartCoroutine(ButtonAnimation());

        if (doorAnimator == null) return;

        if (!doorAnimator.GetBool("isOpen"))
        {
            doorSound?.Play();
            doorAnimator.SetBool("isOpen", true);
        }
    }
    private IEnumerator ButtonAnimation()
    {
        isAnimating = true;
        Vector3 pressedPosition = originalPosition + Vector3.down * pressDepth;

        // Press down
        float elapsedTime = 0f;
        while (elapsedTime < pressSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(originalPosition, pressedPosition, t);
            yield return null;
        }

        // Hold
        if (holdDuration > 0)
        {
            yield return new WaitForSeconds(holdDuration);
        }

        // Return to original position
        elapsedTime = 0f;
        while (elapsedTime < pressSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(pressedPosition, originalPosition, t);
            yield return null;
        }

        transform.localPosition = originalPosition;
        isAnimating = false;
    }
}


