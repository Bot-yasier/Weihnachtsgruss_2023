using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeHandler : MonoBehaviour
{
    public PlayerMovementMouse playerMovementMouse;
    public ProjectileSpawnerMouse projectileSpawnerMouse;
    public Rigidbody2D player;

    public GameObject ElasticWalls1;
    public GameObject DoubleShoot;
    public GameObject SeedUpgrade;

    private List<EnemyController> enemyControllers;

    private void Start()
    {
        enemyControllers = new List<EnemyController>(FindObjectsOfType<EnemyController>());

        foreach (var enemyController in enemyControllers)
        {
            EnemyController.EnemyDeathEvent += OnEnemyDeath;
        }
    }

    private void OnDestroy()
    {
        foreach (var enemyController in enemyControllers)
        {
            EnemyController.EnemyDeathEvent -= OnEnemyDeath;
        }
    }

    public void ElasticWalls()
    {
        projectileSpawnerMouse.enableElasticWalls = true;
        DeactivateUpgrades();
    }

    public void SpeedUp()
    {
        playerMovementMouse.speed = 15;
        DeactivateUpgrades();
    }

    public void Multishoot()
    {
        projectileSpawnerMouse.numBullets += 2;
        DeactivateUpgrades();
    }

    void DeactivateUpgrades()
    {
        ElasticWalls1.SetActive(false);
        DoubleShoot.SetActive(false);
        SeedUpgrade.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    void OnEnemyDeath(EnemyController enemy)
    {
        Upgrade();
    }

    void Upgrade()
    {
        ElasticWalls1.SetActive(true);
        DoubleShoot.SetActive(true);
        SeedUpgrade.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
