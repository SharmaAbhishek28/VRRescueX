// Hello Priyanshu! 🌟 Here's an updated C# script that calculates the inverse square law-based proximity between two game objects and displays it using TextMeshPro.

using UnityEngine;
using TMPro;

public class DosimeterController : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public TextMeshProUGUI proximityText;

    // Constant factor for the inverse square law
    public float constantFactor = 1f;

    void Update()
    {
        // Ensure both objects and TextMeshPro are not null before calculating distance
        if (object1 != null && object2 != null && proximityText != null)
        {
            // Calculate the distance using Vector3.Distance
            float distance = Vector3.Distance(object1.position, object2.position);

            // Calculate the proximity using the inverse square law
            float proximity = constantFactor / (distance * distance);

            // Update the TextMeshPro text to display the proximity
            proximityText.text = $"Proximity: {proximity:F2}";
        }
        else
        {
            // Print a message if either object or TextMeshPro is null
            Debug.LogWarning("One or both game objects or TextMeshPro component are null. Please assign valid transforms and TextMeshPro component.");
        }
    }
}