using UnityEngine;
using UnityEngine.XR;

public class XRControllerHapticFeedback : MonoBehaviour
{
    public XRNode leftControllerNode = XRNode.LeftHand;
    public XRNode rightControllerNode = XRNode.RightHand;
    private InputDevice leftInputDevice;
    private InputDevice rightInputDevice;

    public UnityEngine.UI.Slider leftIntensitySlider;
    public UnityEngine.UI.Slider leftDurationSlider;
    public UnityEngine.UI.Slider rightIntensitySlider;
    public UnityEngine.UI.Slider rightDurationSlider;

    private bool isLeftFeedbackActive = false;
    private bool isRightFeedbackActive = false;

    void Start()
    {
        leftInputDevice = InputDevices.GetDeviceAtXRNode(leftControllerNode);
        rightInputDevice = InputDevices.GetDeviceAtXRNode(rightControllerNode);
    }

    void Update()
    {
        // Check for haptic feedback activation for left hand
        if (isLeftFeedbackActive)
        {
            float leftIntensity = leftIntensitySlider.value;
            float leftDuration = leftDurationSlider.value;

            StartHapticFeedback(leftInputDevice, leftIntensity, leftDuration);
        }
        else
        {
            StopHapticFeedback(leftInputDevice);
        }

        // Check for haptic feedback activation for right hand
        if (isRightFeedbackActive)
        {
            float rightIntensity = rightIntensitySlider.value;
            float rightDuration = rightDurationSlider.value;

            StartHapticFeedback(rightInputDevice, rightIntensity, rightDuration);
        }
        else
        {
            StopHapticFeedback(rightInputDevice);
        }
    }

    public void StartHapticFeedback(InputDevice inputDevice, float intensity, float duration)
    {
        if (inputDevice != null)
        {
            HapticCapabilities capabilities;
            if (inputDevice.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    // Normalize intensity to fit between 0 and 1
                    intensity = Mathf.Clamp01(intensity);

                    // Start haptic feedback
                    inputDevice.SendHapticImpulse(0, intensity, duration);
                }
            }
        }
    }

    public void StopHapticFeedback(InputDevice inputDevice)
    {
        // Stop haptic feedback
        if (inputDevice != null && inputDevice.isValid)
        {
            inputDevice.StopHaptics();
        }
    }

    public void ToggleLeftHapticFeedback(bool isOn)
    {
        isLeftFeedbackActive = isOn;
    }

    public void ToggleRightHapticFeedback(bool isOn)
    {
        isRightFeedbackActive = isOn;
    }
}
