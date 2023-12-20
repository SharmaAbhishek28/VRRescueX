using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionList : MonoBehaviour
{
    // Start is called before the first frame update
    public QuizQuestion quizQuestionPrefab;
    public string[] optionsText;
    public TextMeshProUGUI questionText;
    public string question;

    void Start()
    {
        if (quizQuestionPrefab != null)
        {
            quizQuestionPrefab.SetQuestionData(optionsText, question, questionText);

            QuizQuestion newQuestion = Instantiate(quizQuestionPrefab, transform);
        }
    }
}
