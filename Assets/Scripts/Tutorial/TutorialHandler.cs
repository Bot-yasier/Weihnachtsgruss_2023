using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


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
            default:
                Debug.Log("Nicht gut");
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
    { tutorialText.text = "Beginnen wir mit der Steuerung. Der Spieler läuft immer dort hin wo sich die maus befindent."; }

    void zwei()
    { tutorialText.text = "Jetzt bist du dran, versuche die Geschenke einzusammeln! "; Paket1.SetActive(true);  }

    void drei()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); 

    }

    void vier()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Super gemacht! Tipp: Mit eine klick kannst du eine Rolle ausführen, dies hilft dir in schwierigen Situationen besser auszuweichen. Versuche das einmal!";
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
        tutorialText.text = "Achtung ein Schneeman! Nimm dich immer vor Schneemänner in Acht, die wollen dir nichts gutes.";
        Enemy.SetActive(true);
        freezplayer();

    }
    void neun()
    {
        tutorialText.text = "Oben Rechts siehst du nun deine Herzen. Immer wenn du von einem Schneeman getroffen wirst wird dir ein halbes Herz abgezogen."; Herzen.SetActive(true);
    }
    void zehn()
    {
        tutorialText.text = "Dein Spieler wird den Schneeman austomatisch mit schneebällen bewerfen. Bist du bereit?";
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
        tutorialText.text = "Perfekt! Darf ich vorstellen deine Powerup Bar. Jedes mal wenn du ein Schneeman besiegst steigt dein Level. ";
        Powerbar.SetActive(true);
        freezplayer();
    }
    void dreizehn()
    { tutorialText.text = "Gehe nun in den nächsten Raum, dort warten weitere Schneemänner"; }
}
