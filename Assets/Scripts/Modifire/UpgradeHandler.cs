using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UpgradeHandler : MonoBehaviour
{
    public List<AudioClip> audioClips; // Die Liste der AudioClips
    private AudioSource audioSource;
    private Playerstats playerStats;
    public GameObject Modifireall;
    public bool tutorialbool = false;
    public PlayerMovementMouse playerMovementMouse;
    public ProjectileSpawnerMouse projectileSpawnerMouse;
    public Rigidbody2D playerrigid;
    public RandomModifire randomModifire;
    public PowerUpBar powerUpBar;
    public PlayerHealthController playerHealthController;
    public TextMeshProUGUI qtext1;
    public TextMeshProUGUI qtext2;
    public TextMeshProUGUI qtext3;


    public GameObject Modifire1;
    public GameObject Modifire2;
    public GameObject Modifire3;
    public GameObject q1;
    public GameObject q2;
    public GameObject q3;
    public Button ModiButton1;
    public Button ModiButton2;
    public Button ModiButton3;
    public Button qModiButton1;
    public Button qModiButton2;
    public Button qModiButton3;

    public string Button1value;
    public string Button2value;
    public string Button3value;

    int Akktuell = 0;
    int Plus = 2;
    int Neu = 0;
    int i = 1;

    int health;
    int zwei = 2;
    int eins = 1;

    private List<EnemyController> enemyControllers;

    // Define the LuckUpEvent delegate and event
    public delegate void LuckUpEvent();
    public event LuckUpEvent OnLuckUp;

    private void Update()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            for (int i = 0; i < powerUpBar.levelUpCount; i++)
            {
                Upgrade();
                powerUpBar.levelUpCount = 0;
            }
        }

        enemyControllers = new List<EnemyController>(FindObjectsOfType<EnemyController>());

        foreach (var enemyController in enemyControllers)
        {
            if (i == 1)
            {
                EnemyController.EnemyDeathEvent += OnEnemyDeath;
                i++;
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerStats = FindObjectOfType<Playerstats>();
        ModiButton1.onClick.AddListener(Button1Clicked);
        ModiButton2.onClick.AddListener(Button2Clicked);
        ModiButton3.onClick.AddListener(Button3Clicked);
        qModiButton1.onClick.AddListener(qModiButton1v);
        qModiButton2.onClick.AddListener(qModiButton2v);
        qModiButton3.onClick.AddListener(qModiButton3v);

        // Subscribe to the LuckUpEvent and define a method to handle it
        OnLuckUp += LuckUpEventHandler;
    }

    void qModiButton1v()
    {

        switch (Button1value)
        {
            case "PowerupIcons-Sheet_2":
                qtext1.text = "Elastische Mauern: Verwandelt die Wände in federnde Barrieren, die Schneebälle abprallen lassen und taktische Vorteile bieten.";
                break;
            case "PowerupIcons-Sheet_1":
                qtext1.text = "Mehrfachschuss: Erlaubt dem Spieler, mehrere Schneebälle gleichzeitig abzufeuern, um seine Feuerkraft zu erhöhen.";
                break;
            case "PowerupIcons-Sheet_4":
                qtext1.text = "Heilung: Gewährt dem Spieler eine zusätzliches Herz, um seine Überlebensfähigkeit zu stärken.";
                break;
            case "PowerupIcons-Sheet_5":
                qtext1.text = "Geschwindigkeit: Erhöht die Bewegungsgeschwindigkeit des Spielers ";
                break;
            case "PowerupIcons-Sheet_0":
                qtext1.text = "Glück: Erhöht die Glückschance des Spielers beim Finden von Paketen";
                break;
            default:
                Debug.Log(Button1value);
                break;
        }

    }

    void qModiButton2v()
    {
        switch (Button2value)
        {
            case "PowerupIcons-Sheet_2":
                qtext2.text = "Elastische Mauern: Verwandelt die Wände in federnde Barrieren, die Schneebälle abprallen lassen und taktische Vorteile bieten.";
                break;
            case "PowerupIcons-Sheet_1":
                qtext2.text = "Mehrfachschuss: Erlaubt dem Spieler, mehrere Schneebälle gleichzeitig abzufeuern, um seine Feuerkraft zu erhöhen.";
                break;
            case "PowerupIcons-Sheet_4":
                qtext2.text = "Heilung: Gewährt dem Spieler eine zusätzliches Herz, um seine Überlebensfähigkeit zu stärken.";
                break;
            case "PowerupIcons-Sheet_5":
                qtext2.text = "Geschwindigkeit: Erhöht die Bewegungsgeschwindigkeit des Spielers ";
                break;
            case "PowerupIcons-Sheet_0":
                qtext2.text = "Glück: Erhöht die Glückschance des Spielers beim Finden von Paketen";
                break;
            default:
                Debug.Log(Button2value);
                break;
        }

    }

    void qModiButton3v()
    {

        switch (Button3value)
        {
            case "PowerupIcons-Sheet_2":
                qtext3.text = "Elastische Mauern: Verwandelt die Wände in federnde Barrieren, die Schneebälle abprallen lassen und taktische Vorteile bieten.";
                break;
            case "PowerupIcons-Sheet_1":
                qtext3.text = "Mehrfachschuss: Erlaubt dem Spieler, mehrere Schneebälle gleichzeitig abzufeuern, um seine Feuerkraft zu erhöhen.";
                break;
            case "PowerupIcons-Sheet_4":
                qtext3.text = "Heilung: Gewährt dem Spieler eine zusätzliches Herz, um seine Überlebensfähigkeit zu stärken.";
                break;
            case "PowerupIcons-Sheet_5":
                qtext3.text = "Geschwindigkeit: Erhöht die Bewegungsgeschwindigkeit des Spielers ";
                break;
            case "PowerupIcons-Sheet_0":
                qtext3.text = "Glück: Erhöht die Glückschance des Spielers beim Finden von Paketen";
                break;
            default:
                Debug.Log(Button3value);
                break;
        }
    }

    private void OnDestroy()
    {
        foreach (var enemyController in enemyControllers)
        {
            EnemyController.EnemyDeathEvent -= OnEnemyDeath;
        }
    }

    public void Button1Clicked()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            AudioClip randomClip = audioClips[randomIndex];

            // Spiele den zufälligen AudioClip ab
            audioSource.PlayOneShot(randomClip);
        }
        switch (Button1value)
        {
            case "PowerupIcons-Sheet_2":
                ElasticWalls();
                break;
            case "PowerupIcons-Sheet_1":
                Multishoot();
                break;
            case "PowerupIcons-Sheet_4":
                Heal();
                break;
            case "PowerupIcons-Sheet_5":
                SpeedUp();
                break;
            case "PowerupIcons-Sheet_0":
                LuckUp();
                break;
            default:
                Debug.Log(Button1value);
                break;
        }
    }

    public void Button2Clicked()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            AudioClip randomClip = audioClips[randomIndex];

            // Spiele den zufälligen AudioClip ab
            audioSource.PlayOneShot(randomClip);
        }
        switch (Button2value)
        {
            case "PowerupIcons-Sheet_2":
                ElasticWalls();
                break;
            case "PowerupIcons-Sheet_1":
                Multishoot();
                break;
            case "PowerupIcons-Sheet_4":
                Heal();
                break;
            case "PowerupIcons-Sheet_5":
                SpeedUp();
                break;
            case "PowerupIcons-Sheet_0":
                LuckUp();
                break;
            default:
                Debug.Log(Button2value);
                break;
        }
    }

    public void Button3Clicked()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            AudioClip randomClip = audioClips[randomIndex];

            // Spiele den zufälligen AudioClip ab
            audioSource.PlayOneShot(randomClip);
        }
        switch (Button3value)
        {
            case "PowerupIcons-Sheet_2":
                ElasticWalls();
                break;
            case "PowerupIcons-Sheet_1":
                Multishoot();
                break;
            case "PowerupIcons-Sheet_4":
                Heal();
                break;
            case "PowerupIcons-Sheet_5":
                SpeedUp();
                break;
            case "PowerupIcons-Sheet_0":
                LuckUp();
                break;
            default:
                Debug.Log(Button3value);
                break;
        }
    }

    public void ElasticWalls()
    {
        projectileSpawnerMouse.enableElasticWalls = true;
        DeactivateUpgrades();
    }

    public void LuckUp()
    {
        playerStats.RoomLuck =+ 0.3f;
        Debug.Log("Luck");
        DeactivateUpgrades();

        // Raise the LuckUpEvent
        OnLuckUp?.Invoke();
    }

    public void SpeedUp()
    {
        Akktuell = playerStats.movementSpeed;
        Neu = Akktuell + Plus;
        playerStats.movementSpeed = Neu;
        DeactivateUpgrades();
    }

    public void Multishoot()
    {
        projectileSpawnerMouse.numBullets += 1;
        DeactivateUpgrades();
    }

    void DeactivateUpgrades()
    {
        tutorialbool = true;
        Modifire1.SetActive(false);
        Modifire2.SetActive(false);
        Modifire3.SetActive(false);
        Modifireall.SetActive(false);
        q1.SetActive(false);
        q2.SetActive(false);
        q3.SetActive(false);
        playerMovementMouse.enabled = true;
        playerrigid.constraints = RigidbodyConstraints2D.None;
        playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnEnemyDeath(EnemyController enemy)
    {
        if (powerUpBar != null)
        {
            // Hier kannst du auf das PowerUpBar-Objekt sicher zugreifen
            powerUpBar.IncreaseFillAmount();
        }
    }

    void Upgrade()
    {
      
        tutorialbool = true;
        Modifire1.SetActive(true);
        Modifire2.SetActive(true);
        Modifire3.SetActive(true);
        Modifireall.SetActive(true);
        randomModifire.AssignImages();
        playerMovementMouse.enabled = false;
        playerrigid.constraints = RigidbodyConstraints2D.FreezePosition;
        GameObject[] enemyAmmoObjects = GameObject.FindGameObjectsWithTag("EnemyAmmo");

        foreach (GameObject enemyAmmoObject in enemyAmmoObjects)
        {
            Destroy(enemyAmmoObject);
        }

        Image imageComponent1 = Modifire1.GetComponent<Image>();
        Image imageComponent2 = Modifire2.GetComponent<Image>();
        Image imageComponent3 = Modifire3.GetComponent<Image>();

        Button1value = imageComponent1.sprite.name;
        Button2value = imageComponent2.sprite.name;
        Button3value = imageComponent3.sprite.name;
    }

    // Define the method to handle the LuckUpEvent
    private void LuckUpEventHandler()
    {
        // Perform actions or logic specific to the LuckUpEvent
        // For example:
        Debug.Log("LuckUpEvent was triggered!");
    }
   public void Heal()
   {
        health = playerHealthController.currentHealth;
        if (health == 10)
        {


        }
        if(health == 9)
        {
            playerHealthController.currentHealth = health + eins;
        }
        else
        {
            playerHealthController.currentHealth = health + zwei;
        }
        DeactivateUpgrades();
    }
}
