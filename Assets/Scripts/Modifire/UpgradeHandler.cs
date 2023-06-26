using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public PlayerMovementMouse playerMovementMouse;
    public ProjectileSpawnerMouse projectileSpawnerMouse;
    public Rigidbody2D playerrigid;
    public RandomModifire randomModifire;
    public PowerUpBar powerUpBar;

    public GameObject Modifire1;
    public GameObject Modifire2;
    public GameObject Modifire3;

    public Button ModiButton1;
    public Button ModiButton2;
    public Button ModiButton3;

    public string Button1value;
    public string Button2value;
    public string Button3value;

    int Akktuell = 0;
    int Plus = 2;
    int Neu = 0;
    int i = 1;


    private List<EnemyController> enemyControllers;
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
        ModiButton1.onClick.AddListener(Button1Clicked);
        ModiButton2.onClick.AddListener(Button2Clicked);
        ModiButton3.onClick.AddListener(Button3Clicked);
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
        switch (Button1value)
        {
            case "Bounce":
                ElasticWalls();
                break;
            case "Double":
                Multishoot();
                break;
            case "Faster":
                SpeedUp();
                break;
            case "UpgradeSpeed":
                SpeedUp();

                break;
            default:
                Debug.Log(Button1value);
                break;
        }
    }
    public void Button2Clicked()
    {
        switch (Button2value)
        {
            case "Bounce":
                ElasticWalls();
                break;
            case "Double":
                Multishoot();
                break;
            case "Faster":
                SpeedUp();
                break;
            case "UpgradeSpeed":
                SpeedUp();

                break;
            default:
                Debug.Log(Button2value);
                break;
        }

    }
    public void Button3Clicked()
    {
        switch (Button3value)
        {
            case "Bounce":
                ElasticWalls();
                break;
            case "Double":
                Multishoot();
                break;
            case "Faster":
                SpeedUp();
                break;
            case "UpgradeSpeed":
                SpeedUp();

                break;
            default:
                Debug.Log(Button3value);
                break;
        }

    }

    //Ab hier sind alles Upgrades

    public void ElasticWalls()
    {
        projectileSpawnerMouse.enableElasticWalls = true;
        DeactivateUpgrades();
    }

    public void SpeedUp()
    {

        Akktuell = playerMovementMouse.speed;

        Neu = Akktuell + Plus;

        playerMovementMouse.speed = Neu;
        
        DeactivateUpgrades();
    }

    public void Multishoot()
    {
        projectileSpawnerMouse.numBullets += 1;
        DeactivateUpgrades();
    }


    //End Upgrade


    void DeactivateUpgrades()
    {
        Modifire1.SetActive(false);
        Modifire2.SetActive(false);
        Modifire3.SetActive(false);
        playerMovementMouse.enabled = true;
        playerrigid.constraints = RigidbodyConstraints2D.None;
        playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnEnemyDeath(EnemyController enemy)
    {     
            powerUpBar.IncreaseFillAmount();
      
    }

    void Upgrade()
    {
        Modifire1.SetActive(true);
        Modifire2.SetActive(true);
        Modifire3.SetActive(true);
        randomModifire.AssignImages();
        playerMovementMouse.enabled = false;
        playerrigid.constraints = RigidbodyConstraints2D.FreezePosition;

        Image imageComponent1 = Modifire1.GetComponent<Image>();
        Image imageComponent2 = Modifire2.GetComponent<Image>();
        Image imageComponent3 = Modifire3.GetComponent<Image>();

        Button1value = imageComponent1.sprite.name;
        Button2value = imageComponent2.sprite.name;
        Button3value = imageComponent3.sprite.name;

    }
}
