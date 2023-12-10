using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Net.Mail;
using System;

public class AuthenticationManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public FirebaseUser user;
    public string displayName;
    public Uri photoUrl;
    public string emailAddress;
    

    public InputField emailInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button signupButton;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        });

        loginButton.onClick.AddListener(Login);
        signupButton.onClick.AddListener(SignUp);
    }

    void Login()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                user = task.Result.User;
                Debug.Log("User signed in successfully: " + user.DisplayName);
            });
        }
        else
        {
            Debug.LogWarning("Email and password cannot be empty.");
        }
    }

    void SignUp()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

               user = task.Result.User;
                Debug.Log("User created successfully: " + user.DisplayName);
            });
        }
        else
        {
            Debug.LogWarning("Email and password cannot be empty.");
        }
    }

 

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl;
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}


// Unity and Firebase Authentication with Realtime Database Script

// Import necessary libraries
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;

public class AuthenticationManager : MonoBehaviour
{
    // Firebase variables
    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    // UI elements
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField nameInput;
    public InputField genderInput;
    public InputField emailInput;
    public InputField ageInput;
    public Button loginButton;
    public Button signupButton;

    void Start()
    {
        // Initialize Firebase authentication
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });

        // Attach methods to UI buttons
        loginButton.onClick.AddListener(Login);
        signupButton.onClick.AddListener(SignUp);
    }

    // Method to handle login
    
    // Method to handle signup
    void SignUp()
    {
        // ... (unchanged from the previous script)
    }
}
