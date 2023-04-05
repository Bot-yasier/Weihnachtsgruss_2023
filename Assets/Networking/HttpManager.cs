using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

public class HttpManager : MonoBehaviour
{
    // Set this to the URL of the score API
    private const string ScoreApiUrl = "https://localhost:44397/api/score";

    // Send a score to the server
    public void SendScore(string playerName, int score)
    {
        // Create the API URL
        string apiUrl = $"{ScoreApiUrl}/PostScore";

        try
        {
            // Send the score to the server
            StartCoroutine(SendScoreAsync(apiUrl, playerName, score));
            Debug.Log("Score sent to server.");
        }
        catch (Exception ex)
        {
            Debug.Log($"Error sending score: {ex.Message}");
        }
    }

    private IEnumerator<object> SendScoreAsync(string apiUrl, string playerName, int score)
    {
        // Create the form data to be sent with the request
        var form = new WWWForm();
        form.AddField("playerName", playerName);
        form.AddField("score", score);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl, form))
        {
            // Send the HTTP request and wait for the response
            yield return webRequest.SendWebRequest();

            // Check if the response is successful
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score sent successfully.");
            }
            else
            {
                Debug.Log("Error sending score: " + webRequest.error);
            }
        }
    }
}
