using UnityEngine;
using TMPro;

public class DatabaseScore : MonoBehaviour
{
    public PlayerHealthController playerHealth;
    public string score; // Hier wird der Score gespeichert

    private TextMeshProUGUI scoreTextMesh; // Hier wird das TextMeshProUGUI-Objekt gespeichert

    private void Start()
    {
        // Finde das TextMeshProUGUI-Objekt mit dem Tag "Zahl"
        scoreTextMesh = GameObject.FindGameObjectWithTag("Zahl").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // �berpr�fe, ob das GameObject mit dem Tag "Player" nicht mehr existiert
        if (playerHealth.currentHealth == 0)
        {
            Debug.Log("Hallo");

            score = scoreTextMesh.text;

        }
    }
}
