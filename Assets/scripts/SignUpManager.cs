using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;
using System;
using System.Collections;
using UnityEngine.Networking;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SignUpManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField emailInputField;
    public TMP_InputField ageInputField;
    public TMP_Dropdown genderDropdown;
    public TMP_InputField passwordInputField;
    public TMP_InputField confirmPasswordInputField;
    public LoginManager loginManager; // Reference to your LoginManager script
    public string sendEmailApiUrl = "https://cbrn.onrender.com/sendemail";

    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SignUpButtonClicked()
    {
        string name = nameInputField.text;
        string email = emailInputField.text;
        string age = ageInputField.text;
        string gender = genderDropdown.options[genderDropdown.value].text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;

        // Ensure the fields are not empty
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(age) &&
            !string.IsNullOrEmpty(gender) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword))
        {
            // Check if passwords match
            if (password == confirmPassword)
            {
                // Call the SignUp method
                SignUp(name, email, age, gender, password);
            }
            else
            {
                Debug.LogError("Passwords do not match!");
            }
        }
        else
        {
            Debug.LogError("One or more fields are empty!");
        }
    }

    private async void SignUp(string name, string email, string age, string gender, string password)
    {
        try
        {
            // Create user with email and password
            var task = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            await task;

            // Check for any exceptions during sign-up
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sign-up failed: " + task.Exception);
            }
            else
            {
                // Sign-up successful, now save additional user data to the database
                FirebaseUser user = task.Result.User;
                SaveUserData(user.UserId, name, email, age, gender,GenerateUsername(email));

                StartCoroutine(SendUserInfoToApi(user.UserId,name, email, age, gender, GetSendEmailApiUrl()));

                // Automatically sign in the newly created user
                loginManager.SignInWithUsername(email, password);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception during sign-up: " + ex.Message);
        }
    }

    private void SaveUserData(string userId, string name, string email, string age, string gender,string username="")
    {
        // Save additional user data to the database
        UserInfo userData = new UserInfo(name, email, age, gender,username);
        string json = JsonUtility.ToJson(userData);

        databaseReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

    private string GetSendEmailApiUrl()
    {
        return sendEmailApiUrl;
    }

    private IEnumerator SendUserInfoToApi(string userId, string name, string email, string age, string gender, string sendEmailApiUrl)
    {
        // Create a JSON object with user information
        UserInfo userInfo = new UserInfo(name, email, age, gender,GenerateUsername(email));
        string json = JsonUtility.ToJson(userInfo);

        // Set up the UnityWebRequest to send data to the API
        UnityWebRequest request = UnityWebRequest.PostWwwForm(sendEmailApiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending user info to API: " + request.error);
        }
        else
        {
            Debug.Log("User info sent to API successfully!");
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(request.downloadHandler.text);

            if (response != null)
            {
                string username = response.username;
                Debug.Log("User info sent to API successfully! Username: " + username);

                SaveUserData(userId, name, email, age, gender, username);

                // Now you can use the 'username' variable as needed.
            }
            else
            {
                Debug.LogError("Invalid API response: " + request.downloadHandler.text);
            }
        }
    }

    private string GenerateUsername(string email)
    {
        string[] emailParts = email.Split('@');
        string username = emailParts[0];
        return "VRX-" + username;
    }
}


[System.Serializable]
public class UserInfo
{
    public string name;
    public string email;
    public string age;
    public string gender;
    public string username;

    public UserInfo(string name, string email, string age, string gender, string username)
    {
        this.name = name;
        this.email = email;
        this.age = age;
        this.gender = gender;
        this.username = username;
    }
}


[System.Serializable]
public class ApiResponse
{
    public string message;
    public string username;
}