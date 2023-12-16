using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRObject : MonoBehaviour
{


    private XRDirectInteractor interactor = null;
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    private void Awake()
    {
        interactor = GetComponent<XRDirectInteractor>();

    }

    private void OnEnable()
    {
        interactor.onSelectEntered.AddListener(StartParticleEffect);
    }

    private void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(StopParticleEffect);
    }



    void StartParticleEffect(XRBaseInteractable interactable)
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
            Debug.Log("Particle Effect Started!");
        }
    }

    void StopParticleEffect(XRBaseInteractable interactable)
    {
        if (particleSystem != null)
        {
            particleSystem.Stop();
            Debug.Log("Particle Effect Stopped!");
        }
    }



}
