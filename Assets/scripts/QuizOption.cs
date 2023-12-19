using UnityEngine;
using TMPro;

public class QuizOption : MonoBehaviour
{
    // Create a private variable for getting the text field of the quiz option button, using TextMeshPro
    public TextMeshProUGUI optionText;
    private string optionValue;

    // Method to set option value
    public void SetOptionValue(string value)
    {
        optionValue = value;

        // Update the option text
        if (optionText != null)
        {
            optionText.text = optionValue;
        }
    }

    void Start()
    {
        // Log the option value (optional)
    }
}
