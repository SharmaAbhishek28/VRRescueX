using UnityEngine;
using TMPro;

public class LoginUIManager : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public LoginManager loginManager; // Reference to your LoginManager script

    private void Start()
    {
        // Ensure you have a reference to your LoginManager script
        if (loginManager == null)
        {
            Debug.LogError("LoginManager reference is missing!");
        }
    }

    public void SignInButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        // Ensure the email and password fields are not empty
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            // Call the SignInWithUsername method from your LoginManager
            loginManager.SignInWithUsername(email, password);
        }
        else
        {
            Debug.LogError("Email or password is empty!");
        }
    }

    public void SignOutButtonClicked()
    {
        // Call the SignOut method from your LoginManager
        loginManager.SignOut();
    }
}
