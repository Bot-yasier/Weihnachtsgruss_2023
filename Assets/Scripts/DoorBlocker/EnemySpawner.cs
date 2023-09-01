using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Referenz auf das Enemy-Prefab
    public Transform[] spawnPoints; // Array der Spawnpoints

    public CollisionCounter collisionCounter;

    public static int minEnemies;  // Minimale Anzahl der zu spawnenden Gegner
    public static int maxEnemies;  // Maximale Anzahl der zu spawnenden Gegner

    private bool[] spawnPointOccupied; // Array zur �berpr�fung, ob ein Spawnpoint belegt ist

    private void Start()
    {
        spawnPointOccupied = new bool[spawnPoints.Length]; // Initialisierung des Arrays
        collisionCounter = FindObjectOfType<CollisionCounter>();
    }
    private void Update()
    {
        //Debug.Log(maxEnemies);
        maxEnemies = collisionCounter.maxEnemys;
        minEnemies = collisionCounter.minEnemys;
    }

    public void SpawnEnemies()
    {
        int numEnemies = Random.Range(minEnemies, maxEnemies + 1); // Zuf�llige Anzahl von Gegnern

        for (int i = 0; i < numEnemies; i++)
        {
            int randomSpawnIndex = GetRandomSpawnIndex(); // Zuf�lliger Index eines freien Spawnpoints

            if (randomSpawnIndex != -1)
            {
                Transform spawnPoint = spawnPoints[randomSpawnIndex]; // Ausgew�hlter freier Spawnpoint

                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Instanziierung des Gegners

                spawnPointOccupied[randomSpawnIndex] = true; // Markieren des Spawnpoints als belegt
            }
        }
    }

    private int GetRandomSpawnIndex()
    {
        int[] availableIndices = GetAvailableSpawnIndices(); // Array der verf�gbaren Spawnindices

        if (availableIndices.Length > 0)
        {
            int randomIndex = Random.Range(0, availableIndices.Length); // Zuf�lliger Index aus den verf�gbaren Indices
            return availableIndices[randomIndex];
        }

        return -1; // Falls kein freier Spawnpoint gefunden wurde
    }

    private int[] GetAvailableSpawnIndices()
    {
        System.Collections.Generic.List<int> availableIndices = new System.Collections.Generic.List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPointOccupied[i])
            {
                availableIndices.Add(i); // Hinzuf�gen des freien Spawnindices zur Liste
            }
        }

        return availableIndices.ToArray(); // Umwandlung der Liste in ein Array
    }
}
