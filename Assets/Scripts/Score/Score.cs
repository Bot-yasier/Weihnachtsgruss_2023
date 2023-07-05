using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    public List<GameObject> commonPackages = new List<GameObject>();
    public List<GameObject> epicPackages = new List<GameObject>();
    public List<GameObject> legendPackages = new List<GameObject>();

    public Transform spawnPointsParent; // Reference to the parent object containing spawn points

    [Range(0f, 1f)] public float commonSpawnChance = 0.7f;
    [Range(0f, 1f)] public float epicSpawnChance = 0.2f;
    [Range(0f, 1f)] public float legendSpawnChance = 0.1f;

    public int minPackageCount = 0;
    public int maxPackageCount = 3;

    public DoorBlockerHandler doorBlockerHandler;
    private Playerstats playerStats;

    //[Range(0f, 1f)] public float luckValue = 0.5f;

    private void Start()
    {
        playerStats = FindObjectOfType<Playerstats>();
        SpawnRandomPackages();

        // Subscribe to the LuckUpEvent and define a method to handle it
        UpgradeHandler upgradeHandler = FindObjectOfType<UpgradeHandler>();
        if (upgradeHandler != null)
        {
            upgradeHandler.OnLuckUp += LuckUpEventHandler;
        }
    }

    private void SpawnRandomPackages()
    {
        if (!doorBlockerHandler.hasEnteredRoom) // Check if hasEnteredRoom is false in the DoorBlockerHandler script
        {
            int basePackageCount = Random.Range(minPackageCount, maxPackageCount + 1); // Inclusive range
            int packageCount = Mathf.CeilToInt(basePackageCount * playerStats.RoomLuck); // Adjust package count based on luck value

            List<Transform> availableSpawnPoints = GetSpawnPoints();

            for (int i = 0; i < packageCount; i++)
            {
                float randomValue = Random.value;
                GameObject packagePrefab = null;

                if (randomValue <= commonSpawnChance)
                {
                    packagePrefab = GetRandomPackageFromList(commonPackages);
                }
                else if (randomValue <= commonSpawnChance + epicSpawnChance)
                {
                    packagePrefab = GetRandomPackageFromList(epicPackages);
                }
                else if (randomValue <= commonSpawnChance + epicSpawnChance + legendSpawnChance)
                {
                    packagePrefab = GetRandomPackageFromList(legendPackages);
                }

                if (availableSpawnPoints.Count == 0)
                {
                    break; // No available spawn points left
                }

                int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];
                availableSpawnPoints.RemoveAt(randomSpawnIndex);

                Instantiate(packagePrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    private List<Transform> GetSpawnPoints()
    {
        List<Transform> spawnPoints = new List<Transform>();
        foreach (Transform child in spawnPointsParent)
        {
            spawnPoints.Add(child);
        }
        return spawnPoints;
    }

    private GameObject GetRandomPackageFromList(List<GameObject> packageList)
    {
        int randomIndex = Random.Range(0, packageList.Count);
        return packageList[randomIndex];
    }

    private void LuckUpEventHandler()
    {
        SpawnRandomPackages();
    }
}
