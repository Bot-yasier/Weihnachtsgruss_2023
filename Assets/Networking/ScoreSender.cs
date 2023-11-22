using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class ScoreSender : MonoBehaviour
{
    private const string backendUrl = "/api/score/HandlePostRequest"; // Replace with your actual backend URL

    //private const string backendUrl = "http://localhost:9001/score/HandlePostRequest";

    //private const string backendUrl = "https://localhost:32768/score/HandlePostRequest";

    public DatabaseScore databasescore;

    public string key;

    public Button SendScoretoD;

    public int score; // You can edit this value in the Unity Inspector
    public string playerName; // You can edit this value in the Unity Inspector
    public string emailAddress; // You can edit this value in the Unity Inspector

    public TMP_InputField Email;

    public TMP_InputField Player;

    // Update is called once per frame
    private void Start()
    {
        SendScoretoD.onClick.AddListener(SendScoretoDatabase);
    }
    void SendScoretoDatabase()
    {
            score = databasescore.scoreint;
            Debug.Log($"Sending Score: {score}");
            Debug.Log($"Player Name: {playerName}");
            Debug.Log($"Email Address: {emailAddress}");
            playerName = Player.text;
            emailAddress = Email.text;
            SendScore();
        

    }
    private void Update()
    {
        playerName = Player.text;
        emailAddress = Email.text;
    }

    public void SendScore()
    {
        StartCoroutine(PostScore());
    }

    private IEnumerator PostScore()
    {
        // Escape double quotes in the playerName and emailAddress values
        string escapedPlayerName = playerName.Replace("\"", "\\\"");
        string escapedEmailAddress = emailAddress.Replace("\"", "\\\"");

        // Create a JSON string with the score, escaped playerName, and escaped emailAddress
        string jsonPayload = $"{{ \"Score\": {score}, \"PlayerName\": \"{escapedPlayerName}\", \"Email\": \"{escapedEmailAddress}\" }}";

        // Encrypt JSON-String
        string jsonEncrypted;
        Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = new byte[16];
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(jsonPayload);
                }
                jsonEncrypted = Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        //Debug.Log("Key: " + key);
        //Debug.Log("Plain JSON: " + jsonPayload);
        Debug.Log("Encrypted JSON: " + jsonEncrypted);

        // Create a UnityWebRequest to send the POST request
        UnityWebRequest www = new UnityWebRequest(backendUrl, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonEncrypted);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Log where the request is being sent
        Debug.Log($"Sending POST request to: {backendUrl}");

        // Send the request
        yield return www.SendWebRequest();

        // Check for errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Log the error if the request fails
            Debug.LogError($"POST request failed: {www.error}");
        }
        else
        {
            // Log the success message and server response if the request is successful
            Debug.Log("POST request successful!");
            Debug.Log($"Server response: {www.downloadHandler.text}");
        }
    }
}
