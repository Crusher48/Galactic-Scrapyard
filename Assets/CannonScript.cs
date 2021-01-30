using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] RangeDetectorScript sensor;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileVelocity = 1;
    [SerializeField] float projectileDamage = 10;
    [SerializeField] float cannonCooldown = 1;
    float cannonTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (cannonTimer <= 0)
        {
            GameObject target = sensor.GetClosestObjectInRange();
            if (target)
            {
                GameObject spawnedProjectile = Instantiate(projectilePrefab);
                spawnedProjectile.transform.position = transform.position;
                spawnedProjectile.GetComponent<ProjectileScript>().InitializeProjectile(projectileDamage, (target.transform.position - transform.position).normalized*projectileVelocity);
                cannonTimer += cannonCooldown;
            }
        }
        else
        {
            cannonTimer -= Time.deltaTime;
        }
    }
}
