using UnityEngine;
using System.Collections;

public class DoorBlockerHandler : MonoBehaviour
{
    public  Collider2D[] colliders; // Die Liste der Collider, die du überprüfen möchtest
    public  EnemySpawner enemySpawner;
    public bool oneCollision = false;


  
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        foreach (Collider2D collider in colliders)
        {        
            collider.enabled = true;
        }
    }
    private IEnumerator enemyspawn()
    {
        yield return new WaitForSeconds(3f);
        
            enemySpawner.SpawnEnemies(); 
           
        
     
    }

    private void OpenDoors()
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void PlayerCollWithEnterence()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("collision");
        if (oneCollision == false)
        {
            foreach (Collider2D collider in colliders)
            {

                collider.enabled = false;
               
            }
            oneCollision = true;
            StartCoroutine(wait());
            StartCoroutine(enemyspawn());
        }
        else
        {
            if (enemies.Length == 0)
            {
                Debug.Log(enemies.Length);
                OpenDoors();

            }
        }
    }
}
