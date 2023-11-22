using UnityEngine;
using System.Collections;
using Pathfinding;

public class DoorBlockerHandler : MonoBehaviour
{
    public Collider2D[] colliders; // Die Liste der Collider, die du überprüfen möchtest
    public Collider2D[] triggers;
    public EnemySpawner enemySpawner;
    public bool oneCollision = false;
    public GameObject Astar;
    public Transform Roomposition;
    public float xachse;
    public float yachse;
    public float zachse;
    private float xAktuell;
    private float yAktuell;
    private float zAktuell;
    private float newx;
    private float newy;
    private float newz;
    private GridGraph gg;
    public GameObject unique;
    public bool Bossroom;
    public GameObject ExtraEnemy1;
    public GameObject ExtraEnemy2;

    public bool HaufenSpawn;

    public bool hasEnteredRoom = false; // New variable to track if the player has entered the room

    public void Start()
    {
        xachse = Mathf.RoundToInt(Roomposition.position.x);
        yachse = Mathf.RoundToInt(Roomposition.position.y);
        zachse = Mathf.RoundToInt(Roomposition.position.z);
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0f);
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
        HaufenSpawn = true;
    }

    private IEnumerator enemyspawn()
    {
        yield return new WaitForSeconds(0f);
        if(Bossroom == true)
        {
            ExtraEnemy1.SetActive(true);
            ExtraEnemy2.SetActive(true);
            Bossroom = false;
        }
        enemySpawner.SpawnEnemies();
        Astar.SetActive(true);
        gg = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan();
        Vector3 currentCenter = gg.center;
        xAktuell = currentCenter.x;
        yAktuell = currentCenter.y;
        zAktuell = currentCenter.z;

        newx = xAktuell + xachse;
        newy = yAktuell + yachse;
        newz = zAktuell + zachse;
        gg.center = new Vector3(newx, newy, newz);
        AstarPath.active.Scan();

        //Score.SpawnRandomPackages();
    }

    private void OpenDoors()
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
            Astar.SetActive(false);
            HaufenSpawn = false;
        }

        GameObject[] enemyAmmoObjects = GameObject.FindGameObjectsWithTag("EnemyAmmo");

        foreach (GameObject enemyAmmoObject in enemyAmmoObjects)
        {
            Destroy(enemyAmmoObject);
        }
    }

    public void PlayerCollWithEnterence()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("collision");

        if (!hasEnteredRoom) // Check if the player has not entered the room yet
        {
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
                HaufenSpawn = false;
            }

            foreach (Collider2D trigger in triggers)
            {
                trigger.enabled = false;
            }

            hasEnteredRoom = true; // Set the flag to true indicating the player has entered the room
            unique.SetActive(false);
            StartCoroutine(wait());
            StartCoroutine(enemyspawn());
        }
        else
        {
            if (enemies.Length == 0)
            {
                Debug.Log(enemies.Length);
                OpenDoors();

                GameObject[] enemyMarkers = GameObject.FindGameObjectsWithTag("EnemyMarker");

                foreach (GameObject enemyMarker in enemyMarkers)
                {
                    Destroy(enemyMarker);
                }
            }
        }
    }
}
