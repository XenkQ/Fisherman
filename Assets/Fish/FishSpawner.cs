using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private SpawningPoint[] leftSpawningPoints;
    [SerializeField] private SpawningPoint[] rightSpawningPoints;
    private string[] directions = { "Left", "Right" };
    private bool canSpawnFish = true;

    private void FixedUpdate()
    {
        if(canSpawnFish)
        {
            SpawnRandomFishInRandomPoint();
        }
    }

    private void SpawnRandomFishInRandomPoint()
    {
        string direction = GetRandomDirection();
        if (direction == "Left")
        {
            SpawnFishInRandomPoint(leftSpawningPoints);
        }
        else if (direction == "Right")
        {
            SpawnFishInRandomPoint(rightSpawningPoints);
        }
    }

    private string GetRandomDirection()
    {
        return directions[Random.Range(0, directions.Length)];
    }

    private void SpawnFishInRandomPoint(SpawningPoint[] spawningPoints)
    {
        int spawningPointIndex = GetRandomReadySpawningPointIndex(spawningPoints);
        if (spawningPointIndex > -1 && spawningPoints[spawningPointIndex].CanSpawn)
        {
            Fish fishToSpawn = spawningPoints[spawningPointIndex].GetRandomFishChild();
            fishToSpawn.spawningPointIndex = spawningPointIndex;
            fishToSpawn.gameObject.SetActive(true);
        }
    }

    private int GetRandomReadySpawningPointIndex(SpawningPoint[] points)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].CanSpawn) { indexes.Add(i); }
        }

        if (indexes.Count < 1)
        {
            return -1;
        }
        else
        {
            int index = indexes[Random.Range(0, indexes.Count)];
            return index;
        }
    }

    public void BlockCorrespondingSpawningPointsIndexes(int pointIndex)
    {
        if(pointIndex > -1)
        {
            leftSpawningPoints[pointIndex].BlockSpawner();
            rightSpawningPoints[pointIndex].BlockSpawner();
        }
    }

    public void UnblockCorrespondingSpawningPointsIndexes(int pointIndex)
    {
        if(pointIndex > -1)
        {
            leftSpawningPoints[pointIndex].UnblockSpawner();
            rightSpawningPoints[pointIndex].UnblockSpawner();
        }
    }
}
