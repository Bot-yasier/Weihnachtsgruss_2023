using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpBar : MonoBehaviour
{
    public Image powerUpBar;
    public TextMeshProUGUI levelText; // Referenz zum TextMeshProUGUI-Textkomponenten für den Level

    public float initialFillAmountIncrease = 0.5f;
    public float maxFillAmount = 1f;
    public float fillAmountIncrease;
    public float fillAmountIncreaseMultiplier = 0.8f; // Multiplikationsfaktor für die Steigerung
    public int level = 1;
    public int levelUpThreshold = 5; // Anzahl der Level-Ups, bevor die Steigerung sinkt
    public bool hasLevelUpOccurred = false; // Öffentliche bool-Variable für Level-Up
    public int levelUpCount = 0; // Zähler für Level-Ups

    private bool isFilling; // Flag, um zu überprüfen, ob die Power-Up-Leiste gerade gefüllt wird

    // Diese Methode wird beim Start des Skripts aufgerufen
    private void Start()
    {
        powerUpBar.fillAmount = 0f;
        fillAmountIncrease = initialFillAmountIncrease;
        UpdateLevelText(); // Aktualisiere den Text mit dem Anfangswert des Levels
    }

    // Diese Methode erhöht den Füllbetrag der Power-Up-Leiste
    public void IncreaseFillAmount()
    {
        if (isFilling) return; // Überprüfen, ob die Leiste bereits gefüllt wird

        float targetFillAmount = powerUpBar.fillAmount + fillAmountIncrease;
        targetFillAmount = Mathf.Clamp01(targetFillAmount); // Wert zwischen 0 und 1 begrenzen

        if (targetFillAmount >= maxFillAmount)
        {
            // Lerp-Effekt anwenden, um eine sanfte Animation zu erzeugen
            StartCoroutine(FillAndLevelUp(maxFillAmount, 0.5f));
        }
        else
        {
            // Lerp-Effekt anwenden, um eine sanfte Animation zu erzeugen
            StartCoroutine(FillPowerUpBar(targetFillAmount, 0.5f));
        }
    }

    // Coroutine für den Lerp-Effekt beim Level-Up
    private System.Collections.IEnumerator FillAndLevelUp(float targetFillAmount, float duration)
    {
        isFilling = true; // Setze das Flag auf true, um anzuzeigen, dass die Leiste gefüllt wird

        float startTime = Time.time;
        float startFillAmount = powerUpBar.fillAmount;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            powerUpBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }

        powerUpBar.fillAmount = targetFillAmount; // Abschließend den Ziel-Füllbetrag setzen

        LevelUp(); // Level-Up durchführen

        // Power-Up-Leiste zurücksetzen
        powerUpBar.fillAmount = 0f;

        isFilling = false; // Setze das Flag auf false, um anzuzeigen, dass die Leiste nicht mehr gefüllt wird
    }

    // Coroutine für den Lerp-Effekt beim normalen Füllen der Power-Up-Leiste
    private System.Collections.IEnumerator FillPowerUpBar(float targetFillAmount, float duration)
    {
        isFilling = true; // Setze das Flag auf true, um anzuzeigen, dass die Leiste gefüllt wird

        float startTime = Time.time;
        float startFillAmount = powerUpBar.fillAmount;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            powerUpBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }

        powerUpBar.fillAmount = targetFillAmount; // Abschließend den Ziel-Füllbetrag setzen

        isFilling = false; // Setze das Flag auf false, um anzuzeigen, dass die Leiste nicht mehr gefüllt wird
    }

    // Diese Methode wird aufgerufen, wenn ein Level-Up erreicht wird
    private void LevelUp()
    {
        level++;
        if (level % levelUpThreshold == 0)
        {
            hasLevelUpOccurred = true; // Setze die bool-Variable auf true
            DecreaseFillAmountIncrease(); // Steigerung des Fill Amount Increases verringern
            levelUpCount++; // Zähler für Level-Ups erhöhen
        }
        else
        {
            hasLevelUpOccurred = false; // Setze die bool-Variable auf false
        }

        UpdateLevelText(); // Aktualisiere den Text mit dem neuen Level-Wert
    }

    // Diese Methode verringert die Steigerung des Fill Amount Increases
    private void DecreaseFillAmountIncrease()
    {
        fillAmountIncrease *= fillAmountIncreaseMultiplier;
    }

    // Diese Methode aktualisiert den Text mit dem aktuellen Level-Wert
    private void UpdateLevelText()
    {
        levelText.text = level.ToString();
    }
}
