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

    bool freezebool = false;
    bool unfreezebool = false;

    int FrenchInt;

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
        FrenchInt = PlayerPrefs.GetInt("French");
        tutorialbutton.onClick.AddListener(tutorialbuttonclicked);
        if(FrenchInt == 1)
        {
            tutorialText.text = "Salut ! Par� pour l�aventure ? Entre rennes, nous nous tutoyons. Nous esp�rons que c'est OK.";
        }
        else { tutorialText.text = "Hallo. Bereit f�r ein Abenteuer? Unter Renntieren duzen wir uns. Wir hoffen, das ist OK."; }
      
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
        if(unfreezebool == false)
        {
            playerMovementMouse.enabled = true;
            playerrigid.constraints = RigidbodyConstraints2D.None;
            playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.enabled = true;
            unfreezebool = true;
            freezebool = false;
        }

    }
    void freezplayer()
    {
        if (freezebool == false)
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
            unfreezebool = false;
            freezebool = true;

        }


    }

    void eins()
    {
        if (FrenchInt == 1)
        {
            tutorialText.text = "Mais commen�ons par le syst�me de commande: le joueur se dirige toujours vers le pointeur de la souris.";
        }
        else { tutorialText.text = "Lass uns mit der Steuerung beginnen: Der Spieler bewegt sich stets zur Position des Mauszeigers."; }
    }

    void zwei()
    {
        if (FrenchInt == 1)
        {
            tutorialText.text = "Maintenant, c�est � toi. Essaie de retrouver les cadeaux.";
        }
        else { tutorialText.text = "Jetzt bist du an der Reihe � versuche die Geschenke einzusammeln!"; }
         Paket1.SetActive(true);  
    }

    void drei()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); 

    }

    void vier()
    {
        tutorialbuttong.SetActive(true);
        tutorialTextg.SetActive(true);
        if (FrenchInt == 1)
        {
            tutorialText.text = "Super ! Voici une astuce: d�un clic de souris, tu peux ex�cuter un petit saut. Cela te permettra d'�viter les situations difficiles. Fais un test maintenant!";
        }
        else { tutorialText.text = "Super gemacht! Hier ist ein Tipp: Mit einem Klick kannst du einen H�pfer ausf�hren. Das hilft dir, in schwierigen Situationen besser auszuweichen. Probier es doch gleich mal aus!"; }
        
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
        if (FrenchInt == 1)
        {
            tutorialText.text = "Tu as tout compris ;) Maintenant, tu peux aller dans l�espace suivant. Une surprise t�y attend.";
        }
        else {tutorialText.text = "Du hast es drauf ;) Geh nun in den n�chsten Raum. Dort wartet eine �berraschung auf dich!"; }
        
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
        if (FrenchInt == 1)
        {
            tutorialText.text = "Attention! Un bonhomme de neige! Sois toujours sur tes gardes avec eux. Les bonshommes de neige sont rarement anim�s de bonnes intentions.";
        }
        else { tutorialText.text = "Vorsicht, ein Schneemann! Sei stets auf der Hut vor ihnen, denn Schneem�nner haben selten Gutes im Sinn."; }
        
        Enemy.SetActive(true);
        freezplayer();

    }
    void neun()
    {
        if (FrenchInt == 1)
        {
            tutorialText.text = "En haut � gauche, il y a un c�ur. D�s que tu te fais attraper par un bonhomme de neige, tu perds un demi-c�ur.";
        }
        else { tutorialText.text = "Oben links ist deine Herz-Anzeige. Jedes Mal, wenn dich ein Schneemann erwischt, verlierst du ein halbes Herz."; }
         Herzen.SetActive(true);
    }
    void zehn()
    {
        if (FrenchInt == 1)
        {
            tutorialText.text = "Ton renne bombardera automatiquement le bonhomme de neige avec des boules de neige. Tu es pr�t?";
        }
        else { tutorialText.text = "Dein Renntier wird automatisch Schneeb�lle auf den Schneemann werfen. Bist du bereit?"; }
        
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
        if (FrenchInt == 1)
        {
            tutorialText.text = "Parfait. Je vais te pr�senter ta barre de power-up � chaque fois que tu l�emportes sur un bonhomme de neige, ton niveau augmente.";
        }
        else { tutorialText.text = "Perfekt! Erlaube mir, deine Powerup-Leiste vorzustellen. Jedes Mal, wenn du einen Schneemann besiegst, steigt dein Level an."; }
        
        Powerbar.SetActive(true);
        freezplayer();
        Door3.SetActive(false);
    }
    void dreizehn()
    {
        if (FrenchInt == 1)
        {
            tutorialText.text = "Maintenant, tu peux te rendre dans l�espace suivant. Plusieurs bonshommes de neige t�y attendent. Attention : les bonshommes de neige t�enverront aussi des boules de neige, qui sont color�es en rouge.";
        }
        else { tutorialText.text = "Begib dich nun in den n�chsten Raum, wo weitere Schneem�nner auf dich warten. Achtung: Die Schneem�nner werden auch Schneeb�lle zur�ck werfen, diese sind rot eingef�rbt."; }
        
    }

    void vierzehn()
    {
        tutorialbuttong.SetActive(false);
        tutorialTextg.SetActive(false); unfreezplayer(); //Enemy1.SetActive(true);
                                                         Enemy2.SetActive(true);
    }
    void f�nfzehn()
    {
      
        tutorialTextg.SetActive(true);
        if (FrenchInt == 1)
        {
            tutorialText.text = "Bravo! Tu es pass� � l��chelon sup�rieur! Choisis l�une des trois comp�tences pour am�liorer ton niveau de jeu.";
        }
        else { tutorialText.text = "Gro�artig, du hast ein Level-Up erreicht! W�hle eine von drei F�higkeiten aus, um deine Spielst�rke zu verbessern."; }

        
        freezplayer();
        Door4.SetActive(false);
    }
    void sechszehn()
    {
        unfreezplayer();
        freezplayer();
        tutorialbuttong.SetActive(true);
        if (FrenchInt == 1)
        {
            tutorialText.text = "Maintenant, tu peux te diriger vers l�espace suivant, o� des cadeaux t�attendent.";
        }
        else { tutorialText.text = "Mache dich nun auf den Weg in den n�chsten Raum, wo Geschenke auf dich warten!"; }
        
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
        if (FrenchInt == 1)
        {
            tutorialText.text = "Ton score s�affiche en haut � droite. � chaque fois que tu ramasses un cadeau, ton nombre de points augmente.";
        }
        else { tutorialText.text = "Oben rechts findest du deine Punkte-Anzeige. Jedes Mal, wenn du ein Geschenk einsammelst, steigt dein Punktestand."; }
       
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
        if (FrenchInt == 1)
        {
            tutorialText.text = "Tu es maintenant pr�t pour relever le v�ritable d�fi. Le jeu n�attend plus que toi. Tu as cinq essais. C�est le meilleur parcours qui compte. Bonne chance!";
        }
        else { tutorialText.text = "Nun bist du f�r die eigentliche Herausforderung bereit � das Spiel erwartet dich. Du hast f�nf Versuche. Der beste Durchgang z�hlt. Viel Gl�ck"; }
       
        freezplayer();
    }

}
