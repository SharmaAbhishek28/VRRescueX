using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        // Get the ParticleSystem component attached to the GameObject
        particleSystem = GetComponent<ParticleSystem>();
    }

  

    // Function to start the particle effect
    public void StartParticleEffect()
    {
        // Check if the particle system is not playing to avoid restarting it while already playing
        if (!particleSystem.isPlaying)
        {
            // Play the particle system
            particleSystem.Play();
            Debug.Log("Particle Effect Started 🚀");
        }
    }

    // Function to stop the particle effect
    public void StopParticleEffect()
    {
        // Check if the particle system is playing before trying to stop it
        if (particleSystem.isPlaying)
        {
            // Stop the particle system
            particleSystem.Stop();
            Debug.Log("Particle Effect Stopped ⛔");
        }
    }
}
