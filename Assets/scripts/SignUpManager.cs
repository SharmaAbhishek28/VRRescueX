using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignupManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField emailInput;
    public TMP_InputField ageInput;
    public TMP_Dropdown genderDropdown;
    public TextMeshProUGUI statusText;

    private bool isLogin;
    private string userIdKey = "userId";
    private string authTokenKey = "isLogin";

    private string signupEndpoint = "https://cbrn.onrender.com/api/signup";

    void Start()
    {
        CheckIfUserLoggedIn();
    }

    void CheckIfUserLoggedIn()
    {
        if (PlayerPrefs.HasKey(authTokenKey) && PlayerPrefs.HasKey(userIdKey))
        {
            isLogin = PlayerPrefs.GetString(authTokenKey) == "true";
            if (isLogin)
            {
                SceneManager.LoadScene("Dashboard");
            }
        }
        else
        {
            isLogin = false;
        }
    }
    public enum Gender
    {
        Male,
        Female,
    }

    public void OnSignupButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        string email = emailInput.text;
        int age = int.Parse(ageInput.text);
        string gender = GetSelectedGender();


        StartCoroutine(Signup(username, password, email, age, gender));
    }

    string GetSelectedGender()
    {
        // Get the selected gender from the dropdown
        int index = genderDropdown.value;
        return ((Gender)index).ToString();
    }

    IEnumerator Signup(string username, string password, string email, int age, string gender)
    {
        // Prepare the signup data
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", password);
        form.AddField("email", email);
        form.AddField("age", age.ToString());
        form.AddField("gender", gender);

        string jsonBody = "{\"name\":\"" + username + "\",\"password\":\"" + password + "\",\"email\":\"" + email + "\",\"age\":" + age + ",\"gender\":\"" + gender + "\"}";

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);



        using (UnityWebRequest www = UnityWebRequest.Put(signupEndpoint, bodyRaw))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            SignupResponse response = JsonUtility.FromJson<SignupResponse>(www.downloadHandler.text);

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
                    statusText.text = "Signup successful! " + response.username;
                    isLogin = true;

                    PlayerPrefs.SetString(authTokenKey, isLogin.ToString());
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Dashboard");
                }
                else
                {
                    statusText.text = "Signup failed. " + response.message;
                    isLogin = false;
                }
            }
        }
    }



    [System.Serializable]
    public class SignupResponse
    {
        public string username;
        public string id;
        public string token;
        public string message;
    }
}
