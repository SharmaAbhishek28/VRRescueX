using UnityEngine;
using UnityEngine.XR;

public class AlertSoundManager : MonoBehaviour
{
    public XRControllerHapticFeedback hapticFeedbackScript;
    private AudioSource audioSource;
    public bool loop=true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (loop)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }
        // Component is initially inactive, so we don't play the alert sound here.
    }

    private void OnEnable()
    {
        // Start playing the alert sound when the component becomes active.
        PlayAlertSound();

        // Activate haptic feedback
        if (hapticFeedbackScript != null)
        {
            hapticFeedbackScript.ToggleLeftHapticFeedback(true);
            hapticFeedbackScript.ToggleRightHapticFeedback(true);
        }

        // Invoke a method to pause the sound and deactivate the component after 3 seconds.
        Invoke("PauseSoundAndDeactivate", 3f);
    }

    private void PlayAlertSound()
    {
        if (audioSource != null)
        {
            // Play the alert sound.
            audioSource.Play();
        }
    }

    private void PauseSoundAndDeactivate()
    {
        if (audioSource != null)
        {
            // Pause the sound.
            audioSource.Pause();
        }

        // Deactivate the component.
        gameObject.SetActive(false);

        // Deactivate haptic feedback
        if (hapticFeedbackScript != null)
        {
            hapticFeedbackScript.ToggleLeftHapticFeedback(false);
            hapticFeedbackScript.ToggleRightHapticFeedback(false);
        }
    }
}
