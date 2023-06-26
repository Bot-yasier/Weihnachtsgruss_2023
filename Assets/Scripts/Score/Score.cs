using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    public List<GameObject> commonPackages = new List<GameObject>();
    public List<GameObject> epicPackages = new List<GameObject>();
    public List<GameObject> legendPackages = new List<GameObject>();

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private float commonSpawnChance = 0.7f;
    private float epicSpawnChance = 0.2f;
    private float legendSpawnChance = 0.1f;

    private void Start()
    {
        SpawnRandomPackages();
    }

    private void SpawnRandomPackages()
    {
        int packageCount = Random.Range(0, 3); // 0, 1 oder 2

        List<Transform> availableSpawnPoints = new List<Transform>();
        availableSpawnPoints.Add(spawnPoint1);
        availableSpawnPoints.Add(spawnPoint2);

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
                break; // Keine verfügbaren Spawnpoints mehr
            }

            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[randomSpawnIndex];
            availableSpawnPoints.RemoveAt(randomSpawnIndex);

            Instantiate(packagePrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private GameObject GetRandomPackageFromList(List<GameObject> packageList)
    {
        int randomIndex = Random.Range(0, packageList.Count);
        return packageList[randomIndex];
    }
}
