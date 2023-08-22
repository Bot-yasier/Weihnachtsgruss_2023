using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string sceneName; // Der Name der Ziel-Szene oder "Exit", um das Spiel zu schließen.
    public bool Play = false;
    public static bool T = false;
    public Variables variables;

    public Image playbutton;


    void Start()
    {
        Button button = GetComponent<Button>(); // Hole die Button-Komponente vom selben GameObject.

        // Füge einen Listener hinzu, der auf den Button-Klick reagiert.
        button.onClick.AddListener(ChangeScene);

        if (T == true)
        {
            variables.playbutton = true;

        }

    }
    private void Update()
    {
    }

    // Methode, die aufgerufen wird, wenn der Button geklickt wird.
    private void ChangeScene()
    {
        if (Play == true)
        {
            T = true;
            variables.playbutton = true;
        }

        if (variables.playbutton == true)
        {
            if (sceneName == "Exit")
            {
                // Schließe das Spiel.
                Application.Quit();

            }
            else
            {
                Color col = playbutton.color;
                col.a = 100;
                playbutton.color = col;

                // Lade die angegebene Szene.
                SceneManager.LoadScene(sceneName);
            }


        }
     
    }
}
