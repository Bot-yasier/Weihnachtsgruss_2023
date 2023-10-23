using UnityEngine;
using TMPro;

public class DatabaseScore : MonoBehaviour
{
    public PlayerHealthController playerHealth;
    public string score; // Hier wird der Score gespeichert
    public int scoreint;

    public TextMeshProUGUI finalscore;

    private TextMeshProUGUI scoreTextMesh; // Hier wird das TextMeshProUGUI-Objekt gespeichert

    private void Start()
    {
        // Finde das TextMeshProUGUI-Objekt mit dem Tag "Zahl"
        scoreTextMesh = GameObject.FindGameObjectWithTag("Zahl").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Überprüfe, ob das GameObject mit dem Tag "Player" nicht mehr existiert
        if (playerHealth.currentHealth == 0)
        {
            Debug.Log("Hallo");

            score = scoreTextMesh.text;
            scoreint = int.Parse(score);

            finalscore.text = score;

        }
    }
}
