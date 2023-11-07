using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ScoreReceiver : MonoBehaviour
{
    // Docker
    //private const string backendUrl = "/api/score/HandleGetRequest"; // Replace with your actual backend GET URL

    // Testing
    private const string backendUrl = "http://localhost:9001/score/HandleGetRequest";

    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetScoreData());
    }

    private IEnumerator GetScoreData()
    {
        // Create a UnityWebRequest to send the GET request
        UnityWebRequest www = UnityWebRequest.Get(backendUrl);

        // Log where the request is being sent
        Debug.Log($"Sending GET request to: {backendUrl}");

        // Send the request
        yield return www.SendWebRequest();

        // Check for errors
        if (www.result != UnityWebRequest.Result.Success)
        {
            // Log the error if the request fails
            Debug.LogError($"GET request failed: {www.error}");

            // Display "error" in TMP text component
            scoreText.text = "Error fetching data";
        }
        else
        {
            // Parse the received JSON data
            string responseText = www.downloadHandler.text;

            // Split the response by newline characters
            string[] lines = responseText.Split('\n');

            // Clear the existing text in the TMP text component
            scoreText.text = "";

            // Iterate through the lines and display them
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    scoreText.text += trimmedLine + "\n";
                }
            }
        }
    }
}