using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HepticIntraction : MonoBehaviour
{
    
        public HapticFeedback hapticController;
        public float strength=1.0f;
        public float duration=0.1f;

        // Call this function when you want to start the haptic feedback
        public void StartDrillingVibration()
        {
            hapticController.StartHaptics(strength, duration); // Adjust strength and duration as needed
        }

        // Call this function when you want to stop the haptic feedback
        public void StopDrillingVibration()
        {
            hapticController.StopHaptics();
        }
    }

