using UnityEngine;
using System.Collections;
using Pathfinding;

public class DoorBlockerHandler : MonoBehaviour
{
    public Collider2D[] colliders; // Die Liste der Collider, die du überprüfen möchtest
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

    }

    
    private void OpenDoors()
    {
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
            Astar.SetActive(false);
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
                //Creating Astar Component


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