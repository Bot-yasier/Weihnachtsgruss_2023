using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    public PlayerMovementMouse playerMovementMouse;
    public ProjectileSpawnerMouse projectileSpawnerMouse;
    public EnemyController enemyController;
    public EnemyController enemyController2;
    public EnemyController enemyController3;
    public GameObject ElasticWalls1;
    public GameObject DoubleShoot;
    public GameObject SeedUpgrade;
    public Rigidbody2D player;

    bool b = false;
    bool a = false;
    bool c = false;

    // Update is called once per frame
    void Update()
    {
        if (enemyController.currentHealth <= 0 && !b)
        {

            b = true;
            Upgrade();
        }

        if (enemyController2.currentHealth <= 0 && !a)
        {
           
            a = true;
            Upgrade();

        }
        if (enemyController3.currentHealth <= 0 && !c)
        {
           
            c = true;
            Upgrade();

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
        playerMovementMouse.enabled = true;
        projectileSpawnerMouse.enabled = true;
        player.constraints = RigidbodyConstraints2D.None;
        enemyController.enabled = true;
        enemyController2.enabled = true;
        enemyController3.enabled = true;
    }

    void Upgrade()
    {
        ElasticWalls1.SetActive(true);
        DoubleShoot.SetActive(true);
        SeedUpgrade.SetActive(true);
        playerMovementMouse.enabled = false;
        projectileSpawnerMouse.enabled = false;
        player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        enemyController.enabled = false;
        enemyController2.enabled = false;
        enemyController3.enabled = false;

    }
}