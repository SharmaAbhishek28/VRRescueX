using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Start()
    {
        if (particleSystem == null)
        {
            Debug.LogError("Particle System is not assigned!");
        }
    }


    void StartParticleEffect()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
            Debug.Log("Particle Effect Started!");
        }
    }

    void StopParticleEffect()
    {
        if (particleSystem != null)
        {
            particleSystem.Stop();
            Debug.Log("Particle Effect Stopped!");
        }
    }


}
