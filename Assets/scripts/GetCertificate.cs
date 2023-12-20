using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetCertificate : MonoBehaviour
{
    // Start is called before the first frame update
    private string userId;
    private string userIdKey = "userId";
    private string apiURL;
    void Start()
    {
        if (PlayerPrefs.HasKey(userIdKey))
        {
            userId = PlayerPrefs.GetString(userIdKey);
            apiURL = "https://cbrn.onrender.com/api/finish/" + userId;
        }
    }


    public void GetCert()
    {
        if (userId != null)
        {
            StartCoroutine(GetCertificateRequest());
        }
    }

    IEnumerator GetCertificateRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(apiURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Certificate Request Sent");
        }
    }
}
