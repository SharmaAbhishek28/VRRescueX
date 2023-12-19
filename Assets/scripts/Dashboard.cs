using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Dashboard : MonoBehaviour
{
    private string authTokenKey = "userId";
    private User user;
    private string userId;

    void Start()
    {
        if (PlayerPrefs.HasKey(authTokenKey))
        {
            userId = PlayerPrefs.GetString(authTokenKey);
            StartCoroutine(GetUser(userId));
        }
    }

    IEnumerator GetUser(string userId)
    {
        string getUserEndpoint = "https://cbrn.onrender.com/api/user/" + userId;
        Debug.Log("Get User Endpoint: " + getUserEndpoint);
        using (UnityWebRequest www = UnityWebRequest.Get(getUserEndpoint))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string userData = www.downloadHandler.text;
                Debug.Log(userData);

                // Deserialize the JSON response
                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(userData);

                // Check if the deserialization was successful and userResponse is not null
                if (userResponse != null && userResponse.user != null)
                {
                    user = userResponse.user;
                    Debug.Log(user);

                    // Now you can access user data
                    Debug.Log("User Name: " + user.name);
                    Debug.Log("User Email: " + user.email);

                    // Check if user.training is not null before accessing it

                }
                else
                {
                    Debug.LogError("User response or user data is null.");
                }
            }
            else
            {
                Debug.LogError("Error fetching user data: " + www.error);
            }
        }
    }

}

[Serializable]
public class UserResponse
{
    public User user;
}

public class Module
{
    public string type { get; set; }
    public int score { get; set; }
}

public class Training
{
    public string type { get; set; }
    public int score { get; set; }
    public List<Module> module { get; set; }
}

public class User
{
    public string _id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string username { get; set; }
}

