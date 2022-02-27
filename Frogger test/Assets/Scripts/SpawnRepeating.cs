using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRepeating : Spawner
{
    [Min(0.1f)] public float spawnDelayMin;
    [Min(0.1f)] public float spawnDelayMax;

    private float timeToSpawn;

    private void Update()
    {
        if (timeToSpawn > 0)
        {
            timeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            timeToSpawn = Random.Range(spawnDelayMin, spawnDelayMax);
        }
    }
}
