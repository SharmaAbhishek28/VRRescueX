using UnityEngine;

public class AlertsoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Component is initially inactive, so we don't play the alert sound here.
    }

    private void OnEnable()
    {
        gameObject.SetActive(true);
        // Start playing the alert sound when the component becomes active.
        PlayAlertSound();

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
    }
}
