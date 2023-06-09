using UnityEngine;
using System.Collections;
using Pathfinding;

public class DoorBlockerHandler : MonoBehaviour
{
    public Collider2D[] colliders; // Die Liste der Collider, die du �berpr�fen m�chtest
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

    public bool hasEnteredRoom = false; // New variable to track if the player has entered the room

    public void Start()
    {
        xachse = Mathf.RoundToInt(Roomposition.position.x);
        yachse = Mathf.RoundToInt(Roomposition.position.y);
        zachse = Mathf.RoundToInt(Roomposition.position.z);
    }

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
            }

            hasEnteredRoom = true; // Set the flag to true indicating the player has entered the room
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
