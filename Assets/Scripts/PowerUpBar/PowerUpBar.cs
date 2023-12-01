using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpBar : MonoBehaviour
{
    public Image powerUpBar;
    public TextMeshProUGUI levelText; // Referenz zum TextMeshProUGUI-Textkomponenten f�r den Level

    public float initialFillAmountIncrease = 0.5f;
    public float maxFillAmount = 1f;
    public float fillAmountIncrease;
    public float fillAmountIncreaseMultiplier = 0.8f; // Multiplikationsfaktor f�r die Steigerung
    public int level = 1;
    public int levelUpThreshold = 5; // Anzahl der Level-Ups, bevor die Steigerung sinkt
    public bool hasLevelUpOccurred = false; // �ffentliche bool-Variable f�r Level-Up
    public int levelUpCount = 0; // Z�hler f�r Level-Ups

    private bool isFilling; // Flag, um zu �berpr�fen, ob die Power-Up-Leiste gerade gef�llt wird

    // Diese Methode wird beim Start des Skripts aufgerufen
    private void Start()
    {
        powerUpBar.fillAmount = 0f;
        fillAmountIncrease = initialFillAmountIncrease;
        UpdateLevelText(); // Aktualisiere den Text mit dem Anfangswert des Levels
    }

    // Diese Methode erh�ht den F�llbetrag der Power-Up-Leiste
    public void IncreaseFillAmount()
    {
        if (isFilling) return; // �berpr�fen, ob die Leiste bereits gef�llt wird

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

    // Coroutine f�r den Lerp-Effekt beim Level-Up
    private System.Collections.IEnumerator FillAndLevelUp(float targetFillAmount, float duration)
    {
        isFilling = true; // Setze das Flag auf true, um anzuzeigen, dass die Leiste gef�llt wird

        float startTime = Time.time;
        float startFillAmount = powerUpBar.fillAmount;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            powerUpBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }

        powerUpBar.fillAmount = targetFillAmount; // Abschlie�end den Ziel-F�llbetrag setzen

        LevelUp(); // Level-Up durchf�hren

        // Power-Up-Leiste zur�cksetzen
        powerUpBar.fillAmount = 0f;

        isFilling = false; // Setze das Flag auf false, um anzuzeigen, dass die Leiste nicht mehr gef�llt wird
    }

    // Coroutine f�r den Lerp-Effekt beim normalen F�llen der Power-Up-Leiste
    private System.Collections.IEnumerator FillPowerUpBar(float targetFillAmount, float duration)
    {
        isFilling = true; // Setze das Flag auf true, um anzuzeigen, dass die Leiste gef�llt wird

        float startTime = Time.time;
        float startFillAmount = powerUpBar.fillAmount;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            powerUpBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }

        powerUpBar.fillAmount = targetFillAmount; // Abschlie�end den Ziel-F�llbetrag setzen

        isFilling = false; // Setze das Flag auf false, um anzuzeigen, dass die Leiste nicht mehr gef�llt wird
    }

    // Diese Methode wird aufgerufen, wenn ein Level-Up erreicht wird
    private void LevelUp()
    {
        level++;
        if (level % levelUpThreshold == 0)
        {
            hasLevelUpOccurred = true; // Setze die bool-Variable auf true
            DecreaseFillAmountIncrease(); // Steigerung des Fill Amount Increases verringern
            levelUpCount++; // Z�hler f�r Level-Ups erh�hen
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
