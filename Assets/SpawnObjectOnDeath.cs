using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnDeath : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    private void OnDestroy()
    {
        GameObject newObject = Instantiate(objectToSpawn);
        newObject.transform.position = transform.position;
    }
}
