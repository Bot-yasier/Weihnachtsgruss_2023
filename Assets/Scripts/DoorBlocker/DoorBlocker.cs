using UnityEngine;
using System.Collections;
public class DoorBlocker : MonoBehaviour
{
    public Collider2D doorBlocker;
    private bool door = false;
    public EnemySpawner enemySpawner;
    bool waitcollide = false;
    
    private IEnumerator wait()
    {

        yield return new WaitForSeconds(1f);
        waitcollide = true;
    }
    private IEnumerator spawn()
    {

        yield return new WaitForSeconds(3f);
        enemySpawner.SpawnEnemies();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          
                doorBlocker.enabled = false;

                StartCoroutine(spawn());
                StartCoroutine(wait());

        }
    }


    void Update()
    {
      

        // Überprüfen, ob noch aktive Feinde vorhanden sind
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(waitcollide == true)
        {
            if (enemies.Length >= 1)
            {


                BlockDoors();

            }

            if (enemies.Length == 0)
            {

                OpenDoors();

            }

        }




    }

    private void OpenDoors()
    {
        // Türen öffnen

        doorBlocker.enabled = true;
        DoorClasses.PlayerInRoom = false;
    }
    private void BlockDoors()
    {
        // Türen schliessen

        doorBlocker.enabled = true;
    }
}