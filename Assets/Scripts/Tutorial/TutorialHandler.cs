using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialHandler : MonoBehaviour
{
    public PlayerMovementMouse playerMovementMouse;
    public Rigidbody2D playerrigid;
    public TextMeshProUGUI tutorialText;
    public Button tutorialbutton;
    public GameObject tutorialTextg;
    public GameObject tutorialbuttong;
    public Animator animator;
    public GameObject Enemy;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public UpgradeHandler upgradeHandler;
    public string nextSceneName;

    public GameObject door1;
    public GameObject Herzen;
    public GameObject Powerbar;
    public GameObject Score;

    public GameObject Paket1;
    public GameObject Paket2;
    public GameObject Paket3;
    int counter = 0;
    public bool presentscollected = false;
    int clickcounter = 0;
    bool klicker = false;
    bool enemysd = false;
    bool packaged = false;

    public GameObject Door3;
    public GameObject Door4;

    // Start is called before the first frame update
    void Start()
    {
        tutorialbutton.onClick.AddListener(tutorialbuttonclicked);
        tutorialText.text = "Hallo Hauptling, bereit für ein Abenteuer!";
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        freezplayer();

    }

    // Update is called once per frame
    void Update()
    {
        if (klicker == true)
        {
            if (Input.GetMouseButtonDown(0)) // Überprüfen, ob die linke Maustaste geklickt wurde
            {
                clickcounter++; // Zähler erhöhen

            }
            if (clickcounter >= 5)
            {
                counter++;
                klicker = false;
            }
        }
        if(enemysd == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                counter++;
            }
            enemysd = false;

        }
        if (packaged == true)
        {
            GameObject[] packages = GameObject.FindGameObjectsWithTag("Package");
            if (packages.Length == 0)
            {
                counter++;
            }
            packaged = false;

        }
        if (upgradeHandler.tutorialbool == true)
        {
            counter++;
            upgradeHandler.tutorialbool = false;
        }
      

        if (presentscollected == true)
        {
            counter++;
            presentscollected = false;
        }
        switch (counter)
        {
            case 1:
                eins();
                break;
            case 2:
                zwei();
                break;
            case 3:
                drei();
                break;
            case 4:
                vier();
                break;
            case 5:
                fünf();
                break;
            case 6:
                sechs();
                break;
            case 7:
                sieben();
                break;
            case 8:
                acht();
                break;
            case 9:
                neun();
                break;
            case 10:
                zehn();
                break;
            case 11:
                elf();
                break;
            case 12:
                zwölf();
                break;
            case 13:
                dreizehn();
                break;
            case 14:
                vierzehn();
                break;
            case 15:
                fünfzehn();
                break;
            case 16:
                sechszehn();
                break;
            case 17:
                siebzehn();
                break;
            case 18:
                achtzehn();
                break;
            case 19:
                neunzehn();
                break;
            case 20:
                zwanzig();
                break;
            case 21:
                SceneManager.LoadScene(nextSceneName);
                break;
            default:
               
                break;
        }
    }
    public void tutorialbuttonclicked()
    {
        counter++;
        Debug.Log(counter);
    }

    void unfreezplayer()
    {
        playerMovementMouse.enabled = true;
        playerrigid.constraints = RigidbodyConstraints2D.None;
        playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.enabled = true;
    }
    void freezplayer()
    {
        animator.enabled = false;
        playerMovementMouse.enabled = false;
        playerrigid.constraints = RigidbodyConstraints2D.FreezePosition;
        playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        GameObject[] enemyAmmoObjects = GameObject.FindGameObjectsWithTag("EnemyAmmo");

        foreach (GameObject enemyAmmoObject in enemyAmmoObjects)
        {
            Destroy(enemyAmmoObject);
        }

    }

    void eins()
    { tutorialText.text = "Lassen Sie uns mit der Steuerung beginnen. Der Spieler bewegt sich stets zur Position des Mauszeigers."; }

    void zwei()
    { tutorialText.text = "Jetzt bist du an der Reihe – versuche die Geschenke einzusammeln!"; Paket1.SetActive(true);  }

    void drei()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); 

    }

    void vier()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Super gemacht! Hier ist ein Tipp: Mit einem Klick kannst du eine Rolle ausführen. Das hilft dir, in schwierigen Situationen besser auszuweichen. Probier es doch gleich mal aus!";
        freezplayer();

    }

    void fünf()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); klicker = true;
    }
    void sechs()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Du hast es drauf ;) Gehen nun in den nächsten Raum dort wartet eine Überraschung auf dich!";
        freezplayer();


    }
    void sieben()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); door1.SetActive(false);
    }
    void acht()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Vorsicht, ein Schneemann! Sei stets auf der Hut vor ihnen, denn Schneemänner haben selten Gutes im Sinn.";
        Enemy.SetActive(true);
        freezplayer();

    }
    void neun()
    {
        tutorialText.text = "Du findest nun oben links deine Herz-Anzeige. Jedes Mal, wenn dich ein Schneemann erwischt, verlierst du ein halbes Herz."; Herzen.SetActive(true);
    }
    void zehn()
    {
        tutorialText.text = "Dein Charakter wird automatisch Schneebälle auf den Schneemann werfen. Bist du bereit?";
    }
    void elf()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer();
        enemysd = true;
    }

    void zwölf()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Perfekt! Erlaube mir, deine Powerup-Leiste vorzustellen. Jedes Mal, wenn du einen Schneemann besiegst, steigt dein Level an.";
        Powerbar.SetActive(true);
        freezplayer();
        Door3.SetActive(false);
    }
    void dreizehn()
    { tutorialText.text = "Begib dich nun in den nächsten Raum, wo weitere Schneemänner auf dich warten."; }

    void vierzehn()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); Enemy1.SetActive(true); Enemy2.SetActive(true);
    }
    void fünfzehn()
    {
      
        tutorialTextg.SetActive(true);
        tutorialText.text = "Großartig, du hast ein Level-Up erreicht! Wähle eine von drei Fähigkeiten aus, um deinen Charakter zu verbessern.";
        freezplayer();
        Door4.SetActive(false);
    }
    void sechszehn()
    {
        freezplayer();
        tutorialbuttong.SetActive(true);
        tutorialText.text = "Mache dich nun auf den Weg in den nächsten Raum, wo Geschenke auf dich warten!";
    }
    void siebzehn()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer();
    }

    void achtzehn()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Richte deinen Blick nach oben rechts, dort findest du deine Punkte-Anzeige. Jedes Mal, wenn du ein Geschenk einsammelst, steigt dein Punktestand.";
        Score.SetActive(true);


        freezplayer();
    }
    void neunzehn()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer();
        packaged = true;
    }
    void zwanzig()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Nun bist du für die eigentliche Herausforderung bereit – die reale Welt erwartet dich! :)";
        freezplayer();
    }

}
