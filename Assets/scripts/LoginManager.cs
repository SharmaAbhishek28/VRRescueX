using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI statusText;

    private bool isLogin;
    private string authTokenKey = "isLogin";

    private string loginEndpoint = "https://cbrn.onrender.com/api/login";

    void Start()
    {
        CheckIfUserLoggedIn();
    }

    void CheckIfUserLoggedIn()
    {
        if (PlayerPrefs.HasKey(authTokenKey))
        {
            isLogin = true;
            SceneManager.LoadScene("nuclear scene");
        }
        else
        {
            isLogin = false;
        }
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        StartCoroutine(Login(username, password));
    }

    IEnumerator Login(string username, string password)
    {
        // Prepare the login data
        string jsonBody = "{\"name\":\"" + username + "\",\"password\":\"" + password + "\"}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest www = UnityWebRequest.Put(loginEndpoint, bodyRaw))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            LoginResponse response = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                statusText.text = response.message;
                isLogin = false;
            }
            else
            {
                int statusCode = (int)www.responseCode;

                if (statusCode == 200 || statusCode == 201)
                {
                    statusText.text = "Login successful! " + response.username;
                    isLogin = true;

                    PlayerPrefs.SetString(authTokenKey, isLogin.ToString());
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("nuclear scene");
                }
                else
                {
                    statusText.text = "Login failed. " + response.message;
                    isLogin = false;
                }
            }
        }
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string username;
        public string id;
        public string token;
        public string message;
    }
}
