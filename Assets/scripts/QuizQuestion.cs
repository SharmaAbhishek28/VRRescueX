using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizQuestion : MonoBehaviour
{
    public QuizOption optionPrefab; // Reference to the QuizOption prefab
    public string[] optionsText;
    public float factor = 0f;
    public TextMeshProUGUI questionText;
    public string question;
    // Start is called before the first frame update
    void Start()
    {
        if (question != null&& questionText!=null)
        {
            // set text of the quiz option button
            questionText.text = question;
        }
    }

    public void SetQuestionData(string[] options, string qText, TextMeshProUGUI qUIText)
    {
        optionsText = options;
        question = qText;
        questionText = qUIText;
        for (int i = 0; i < optionsText.Length; i++)
        {
            SpawnQuizOption(i);
        }
    }

    // Method to spawn a QuizOption from the prefab
    void SpawnQuizOption(int i)
    {
        // Instantiate the QuizOption prefab
        QuizOption newOption = Instantiate(optionPrefab, transform);

        // Optionally, you can set properties of the new option here
        newOption.SetOptionValue(optionsText[i]);

        newOption.transform.position = new Vector3(newOption.transform.position.x,interpole(i*12) , newOption.transform.position.z);
    }

    float interpole(float y)
    {
        return 2f - 0.08f * (y - 1f);
    }
}
