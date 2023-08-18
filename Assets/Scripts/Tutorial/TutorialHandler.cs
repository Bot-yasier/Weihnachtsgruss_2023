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

    // Start is called before the first frame update
    void Start()
    {
        tutorialbutton.onClick.AddListener(tutorialbuttonclicked);
        tutorialText.text = "Hallo Hauptling, bereit f�r ein Abenteuer!";
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        freezplayer();

    }

    // Update is called once per frame
    void Update()
    {
        if (klicker == true)
        {
            if (Input.GetMouseButtonDown(0)) // �berpr�fen, ob die linke Maustaste geklickt wurde
            {
                clickcounter++; // Z�hler erh�hen

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
                f�nf();
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
                zw�lf();
                break;
            case 13:
                dreizehn();
                break;
            case 14:
                vierzehn();
                break;
            case 15:
                f�nfzehn();
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
    { tutorialText.text = "Beginnen wir mit der Steuerung an. Du bewegst dich immer zur Position deines Mauszeigers."; }

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
        tutorialText.text = "Super gemacht! Tipp: Mit eine klick kannst du eine Rolle ausf�hren, dies hilft dir in schwierigen Situationen besser auszuweichen. Versuche das einmal!";
        freezplayer();

    }

    void f�nf()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); klicker = true;
    }
    void sechs()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Du hast es drauf ;) Gehen nun in den n�chsten Raum dort wartet eine �berraschung auf dich!";
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
        tutorialText.text = "Achtung ein Schneeman! Nimm dich immer vor Schneem�nner in Acht, die wollen dir nichts gutes.";
        Enemy.SetActive(true);
        freezplayer();

    }
    void neun()
    {
        tutorialText.text = "Oben Links siehst du nun deine Herzen. Immer wenn du von einem Schneeman getroffen wirst wird dir ein halbes Herz abgezogen."; Herzen.SetActive(true);
    }
    void zehn()
    {
        tutorialText.text = "Dein Spieler wird den Schneeman austomatisch mit schneeb�llen bewerfen. Bist du bereit?";
    }
    void elf()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer();
        enemysd = true;
    }

    void zw�lf()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        tutorialText.text = "Perfekt! Darf ich vorstellen deine Powerup Bar. Jedes mal wenn du ein Schneeman besiegst steigt dein Level. ";
        Powerbar.SetActive(true);
        freezplayer();
    }
    void dreizehn()
    { tutorialText.text = "Gehe nun in den n�chsten Raum, dort warten weitere Schneem�nner"; }

    void vierzehn()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); Enemy1.SetActive(true); Enemy2.SetActive(true);
    }
    void f�nfzehn()
    {
      
        tutorialTextg.SetActive(true);
        tutorialText.text = "Super du hast ein Level UP :). W�hle eine der drei F�higkeiten aus, diese verbessern dein Spieler";
        freezplayer();
    }
    void sechszehn()
    {
        freezplayer();
        tutorialbuttong.SetActive(true);
        tutorialText.text = "Gehe nun in den N�chsten Raum, dort warten Geschenke auf dich!";
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
        tutorialText.text = "Schau oben Rechts, dort sind deine Punkte. Jedes Mal wenn du ein Geschenk einsammelst werden deine Punkte erh�ht.";
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
        tutorialText.text = "Jetzt bist du bereit f�r die richtige Welt :)";
        freezplayer();
    }

}
