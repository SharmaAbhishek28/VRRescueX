using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class QuizQuestion : MonoBehaviour
{
    public QuizOption optionPrefab; // Reference to the QuizOption prefab
    public string[] optionsText;
    public float factor = 0f;
    public TextMeshProUGUI questionText;
    public string question;
    public int correctAnswerNumber;
    public int selectedAnswerNumber;
    public Canvas nextQuestion;

    private string apiURL;
    private string userIdKey="userId";
    private string userId;
    public TRAINING_TYPE trainingType=TRAINING_TYPE.NUCLEAR;
    public MODULE_TYPE moduleType=MODULE_TYPE.MODULE_1;

    public int QuesID;
    // Start is called before the first frame update
    void Start()
    {
        if (question != null&& questionText!=null)
        {
            // set text of the quiz option button
            questionText.text = question;
        }
        if(optionsText != null)
        {
            // set text of the quiz option button
            for (int i = 0; i < optionsText.Length; i++)
            {
                SpawnQuizOption(i);
            }
        }

        if (PlayerPrefs.HasKey(userIdKey))
        {
            userId=PlayerPrefs.GetString(userIdKey);
            apiURL = "https://cbrn.onrender.com/api/user/"+userId+"/score";
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

        newOption.GetComponent<Button>().onClick.AddListener(() => OnOptionClicked(i));

        newOption.transform.position = new Vector3(newOption.transform.position.x,interpole(i*2) , newOption.transform.position.z);
    }

    float interpole(float y)
    {
        return 2f - 0.08f * (y - 1f);
    }

    // Method to handle option click event
    void OnOptionClicked(int optionIndex)
    {
        selectedAnswerNumber = optionIndex;

        if (selectedAnswerNumber == correctAnswerNumber)
        {
            Debug.Log("Correct Answer!");
            StartCoroutine(UpdateScore(2));
        }
        else
        {
            Debug.Log("Incorrect Answer!");
            StartCoroutine(UpdateScore(0));
        }

        gameObject.SetActive(false);
        nextQuestion.gameObject.SetActive(true);
    }

    IEnumerator UpdateScore(int score)
    {
        string jsonBody = "{\"score\":" + score + ",\"training\":{\"type\":" + trainingType + ",\"moduleType\":" + moduleType + "}}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest www = UnityWebRequest.Put(apiURL, bodyRaw))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Sending Request to: " + apiURL);

            yield return www.SendWebRequest();

            Debug.Log("Response Code: " + www.responseCode); // Log the response code

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Network/HTTP Error: " + www.error);
                Debug.LogError("Server Response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Score Updated!");
                Debug.Log("Server Response: " + www.downloadHandler.text);
            }
        }
    }
}

public enum TRAINING_TYPE
{
    CHEMICAL,
    RADIOLOGICAL,
    BIOLOGICAL,
    NUCLEAR
}

public enum MODULE_TYPE
{
    MODULE_1,
    MODULE_2,
    MODULE_3,
    MODULE_4,
    MODULE_5,
    MODULE_6
}