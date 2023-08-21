using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string sceneName; // Der Name der Ziel-Szene oder "Exit", um das Spiel zu schlie�en.

    private void Start()
    {
        Button button = GetComponent<Button>(); // Hole die Button-Komponente vom selben GameObject.

        // F�ge einen Listener hinzu, der auf den Button-Klick reagiert.
        button.onClick.AddListener(ChangeScene);
    }

    // Methode, die aufgerufen wird, wenn der Button geklickt wird.
    private void ChangeScene()
    {
        if (sceneName == "Exit")
        {
            // Schlie�e das Spiel.
            Application.Quit();
        }
        else
        {
            // Lade die angegebene Szene.
            SceneManager.LoadScene(sceneName);
        }
    }
}
