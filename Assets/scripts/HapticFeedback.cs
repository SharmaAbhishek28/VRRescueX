using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class HapticFeedback : MonoBehaviour
{
    private InputDevice leftController;
    private bool isVibrating = false;

    void Start()
    {
        // Assuming this script is attached to a controller object
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    // Function to start the haptic feedback
    public void StartHaptics(float strength, float duration)
    {
        if (!isVibrating)
        {
            StartCoroutine(TriggerHaptics(strength, duration));
        }
    }

    // Function to stop the haptic feedback
    public void StopHaptics()
    {
        StopAllCoroutines();
        leftController.SendHapticImpulse(0, 0, 0); // Stop any ongoing haptics
        isVibrating = false;
    }

    // Coroutine to simulate drilling machine vibration
    private IEnumerator TriggerHaptics(float strength, float duration)
    {
        isVibrating = true;
        while (isVibrating)
        {
            leftController.SendHapticImpulse(0, strength, duration);
            yield return new WaitForSeconds(duration);
        }
    }
}
