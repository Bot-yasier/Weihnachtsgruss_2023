using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    public PlayerMovementMouse playerMovementMouse;
    public ProjectileSpawnerMouse projectileSpawnerMouse;
    public EnemyController enemyController;

    public GameObject ElasticWalls1;
    public GameObject DoubleShoot;
    public GameObject SeedUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.currentHealth <= 0)
        {

            ElasticWalls1.SetActive(true);
            DoubleShoot.SetActive(true);
            SeedUpgrade.SetActive(true);



        }
        
    }

    public void ElasticWalls()
    {

        projectileSpawnerMouse.enableElasticWalls = true;

    }

    public void SpeedUp()
    {

        playerMovementMouse.speed =20;

    }

    public void Multishoot()
    {
        projectileSpawnerMouse.numBullets = +2;

    }
}
