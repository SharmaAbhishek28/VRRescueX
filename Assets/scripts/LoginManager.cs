using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class LoginManager : MonoBehaviour
{
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

    public void SignInWithUsername(string username, string password)
    {
        // Retrieve email associated with the username from the database
        GetEmailByUsername(username, email =>
        {
            if (!string.IsNullOrEmpty(email))
            {
                // Sign in with the obtained email and provided password
                auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        // Handle sign-in failure
                        Debug.LogError("Sign-in failed: " + task.Exception);
                    }
                    else
                    {
                        // Signed in successfully
                        FirebaseUser user = task.Result.User;
                        Debug.Log("Signed in as: " + user.DisplayName);
                    }
                });
            }
            else
            {
                // Handle the case where no email is found for the given username
                Debug.LogError("No email found for the username: " + username);
            }
        });
    }

    private async void GetEmailByUsername(string username, Action<string> callback)
    {
        DataSnapshot dataSnapshot = await databaseReference.Child("users").Child(username).Child("email").GetValueAsync();

        if (dataSnapshot.Exists)
        {
            string email = dataSnapshot.Value.ToString();
            callback(email);
        }
        else
        {
            callback(null);
        }
    }

    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("Signed out");
    }
}
