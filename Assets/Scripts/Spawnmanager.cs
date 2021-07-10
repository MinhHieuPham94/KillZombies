using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
    public GameObject zombie;

    private float minSpawnTime = 0.2f;
    private float maxSpawnTime = 3f;
    private float lastSpawnTime = 0;
    private float spawnTime = 0;
    private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastSpawnTime + spawnTime)
        {
            Spawn();
        }
    }

    void UpdateSpawnTime()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Spawn()
    {
        float spawnPointX = Random.Range(-113f, 217f);
        float spawnPointZ = Random.Range(-150f,-80f);
        spawnPoint = new Vector3 (spawnPointX,0,spawnPointZ);

        Instantiate(zombie,spawnPoint,Quaternion.identity);

        UpdateSpawnTime();
    }
}
