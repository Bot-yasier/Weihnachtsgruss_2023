using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Package : MonoBehaviour
{
    public bool isCommon = false;
    public bool isEpic = false;
    public bool isLegend = false;

    public int commonPoints = 50;
    public int epicPoints = 100;
    public int legendPoints = 200;


    private void Start()
    {
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int score = 0;

            if (isCommon)
            {
                score = commonPoints;
            }
            else if (isEpic)
            {
                score = epicPoints;
            }
            else if (isLegend)
            {
                score = legendPoints;
            }

            // Finde das TextMesh mit dem Tag "Zahl"
            TextMeshProUGUI scoreTextMesh = GameObject.FindGameObjectWithTag("Zahl").GetComponent<TextMeshProUGUI>();

            int currentScore = 0;
            if (!string.IsNullOrEmpty(scoreTextMesh.text))
            {
                currentScore = int.Parse(scoreTextMesh.text);
            }

            currentScore += score;
            scoreTextMesh.text = currentScore.ToString();

            Destroy(gameObject);
        }
    }
}
