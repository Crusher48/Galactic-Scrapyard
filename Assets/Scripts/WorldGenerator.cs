using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
class WeightedEntry
{
    public GameObject spawnedObject;
    public int weight;
}
public class WorldGenerator : MonoBehaviour
{
    [SerializeField] GameObject spawningCenter; //the player ship that spawning is centered on
    [SerializeField] float minSpawningDistance; //minimum spawning distance from the player
    [SerializeField] float maxSpawningDistance; //maximum spawning distance from the player
    [SerializeField] float maxSpawnAttempts; //maximum attempts to find a clear spawning zone
    [SerializeField] float spawnRadius; //radius of a spawning zone
    [SerializeField] int minSpawnCount; //minimum amount of objects in each new spawn
    [SerializeField] int maxSpawnCount; //amount of objects in each new spawn
    [SerializeField] int startingSpawns; //amount of spawns to do at the start of the game
    [SerializeField] float spawnCooldown; //time between new spawns
    float spawnTimer; //timer tracking the spawn cooldown
    [SerializeField] List<WeightedEntry> spawnables; //a list of spawnables
    List<GameObject> weightedSpawnList; //the weighted list of all the spawnables
    // Start is called before the first frame update
    void Start()
    {
        weightedSpawnList = new List<GameObject>();
        //generate the weighted spawn list
        foreach (WeightedEntry entry in spawnables)
        {
            for (int x = 0; x < entry.weight; x++)
                weightedSpawnList.Add(entry.spawnedObject);
        }
        //do initial spawns
        for (int x = 0; x < startingSpawns; x++)
            SpawnDebrisField();
        spawnTimer = spawnCooldown;
    }
    void SpawnDebrisField()
    {
        Vector2 wreckSpawnPoint = Vector2.zero;
        //attempt to find a clear area to spawn the debris
        for (int x = 0; x < maxSpawnAttempts; x++)
        {
            float wreckAngle = Random.Range(0, 2 * Mathf.PI);
            float wreckDistance = Random.Range(minSpawningDistance, maxSpawningDistance);
            Vector2 potentialSpawnPoint = (Vector2)spawningCenter.transform.position + new Vector2(Mathf.Cos(wreckAngle), Mathf.Sin(wreckAngle)) * wreckDistance;
            LayerMask layers = LayerMask.GetMask("Component");
            RaycastHit2D hit = Physics2D.CircleCast(potentialSpawnPoint, spawnRadius, Vector2.zero, 0, layers);
            if (!hit)
                wreckSpawnPoint = potentialSpawnPoint;
        }
        if (wreckSpawnPoint == Vector2.zero)
        {
            print("Spawn Failed!");
            return;
        }
        //spawn the debris
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);
        for (int x = 0; x < spawnCount; x++)
        {
            //pick the debris spawning point
            float spawnAngle = Random.Range(0, 2 * Mathf.PI);
            float spawnDistance = Random.Range(0,spawnRadius);
            GameObject spawnedObject = Instantiate(weightedSpawnList[Random.Range(0, weightedSpawnList.Count)]);
            //set the object position
            spawnedObject.transform.position = wreckSpawnPoint + new Vector2(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle))*spawnDistance;
            //set the object rotation
            Vector3 eulers = spawnedObject.transform.eulerAngles;
            eulers.z = Random.Range(0, 360);
            spawnedObject.transform.eulerAngles = eulers;
            if (spawnedObject.layer == LayerMask.NameToLayer("Component"))
            {
                Health healthComponent = spawnedObject.GetComponent<Health>();
                healthComponent.health = healthComponent.maxHealth * Random.Range(0.2f, 0.6f);
                healthComponent.ChangeHealth(0); //update the color
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnDebrisField();
            spawnTimer = spawnCooldown;
        }
    }
}
