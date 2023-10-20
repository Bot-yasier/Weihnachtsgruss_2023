using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreSender : MonoBehaviour
{
    private const string backendUrl = "http://localhost:9001/score/HandlePostRequest"; // Replace with your actual backend URL
    public DatabaseScore databasescore;

    public int score; // You can edit this value in the Unity Inspector
    public string playerName = "John"; // You can edit this value in the Unity Inspector
    public string emailAddress = "example@example.com"; // You can edit this value in the Unity Inspector

    // Update is called once per frame
    void Update()
    {
        score = databasescore.scoreint;
        //score = int.Parse(databasescore.score);
        // Check if the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Trigger the SendScore method when "P" is pressed
            SendScore();
        }
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

        // Create a UnityWebRequest to send the POST request
        UnityWebRequest www = new UnityWebRequest(backendUrl, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return www.SendWebRequest();

        // Check for errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"POST request failed: {www.error}");
        }
        else
        {
            Debug.Log("POST request successful!");
            Debug.Log($"Server response: {www.downloadHandler.text}");
        }
    }
}
