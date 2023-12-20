using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    private string authTokenKey = "isLogin";

    public void LogOut()
    {
        PlayerPrefs.SetString(authTokenKey, null);
        SceneManager.LoadScene("bright_Login");
    }
}
