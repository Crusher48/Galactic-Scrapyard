using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float delayBetweenSpawns = 10;
    float spawnTimer = 0;

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer += delayBetweenSpawns;
            GameObject spawn = Instantiate(objectToSpawn);
            float spawnAngle = Random.Range(0, 2 * Mathf.PI);
            spawn.transform.position = transform.position + new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0)*1.5f;
        }
    }
}
