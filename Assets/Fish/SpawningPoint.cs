using System.Collections.Generic;
using UnityEngine;

public class SpawningPoint : MonoBehaviour
{
    [SerializeField] private Fish[] fishPrefabs;
    [SerializeField] private List<Fish> fishChildren;
    private bool canSpawn = true;
    public bool CanSpawn { get { return canSpawn; } }

    private void Awake()
    {
        PoolFish();
    }

    private void PoolFish()
    {
        foreach (Fish fishPrefab in fishPrefabs)
        {
            Fish fish1 = Instantiate(fishPrefab, GetPosition(), GetRotation(), transform);
            fishChildren.Add(fish1);
            fish1.DisableFishProcess();
        }
    }

    public void BlockSpawner()
    {
        canSpawn = false;
    }

    public void UnblockSpawner()
    {
        canSpawn = true;
    }

    public Fish GetRandomFishChild()
    {
        return fishChildren[Random.Range(0, fishChildren.Count)];
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
