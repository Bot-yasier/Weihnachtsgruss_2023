using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ScoreReceiver : MonoBehaviour
{
    // Testing
    private const string backendUrl = "http://localhost:9001/score/HandleGetRequest";

    public TMP_Text textBox1;
    public TMP_Text textBox2;
    public TMP_Text textBox3;

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

            // Display "error" in TMP text components
            textBox1.text = "Error fetching data";
            textBox2.text = "";
            textBox3.text = "";
        }
        else
        {
            // Parse the received JSON data
            string responseText = www.downloadHandler.text;

            // Split the response by semicolon characters
            string[] lines = responseText.Split(';');

            // Clear the existing text in the TMP text components
            textBox1.text = "";
            textBox2.text = "";
            textBox3.text = "";

            // Iterate through the lines and display them in the corresponding text boxes
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    // Split each line by the comma (",") character
                    string[] parts = trimmedLine.Split(',');

                    // Ensure there are at least three parts (comma-separated values) in the line
                    if (parts.Length >= 3)
                    {
                        // Assign the parts to the text boxes
                        textBox1.text += parts[0] + "\n";
                        textBox2.text += parts[1] + "\n";
                        textBox3.text += parts[2] + "\n";
                    }
                }
            }
        }
    }
}
