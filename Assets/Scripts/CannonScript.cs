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
        if (cannonTimer <= 0) //attempt to fire the cannon
        {
            GameObject target = sensor.GetClosestObjectInRange();
            if (target)
            {
                GameObject spawnedProjectile = Instantiate(projectilePrefab);
                spawnedProjectile.transform.position = transform.position;
                //launch direction (with a bit of leading the target)
                Vector2 launchDirection = ((Vector2)target.transform.position - ((Vector2)transform.position-target.GetComponent<Rigidbody2D>().velocity*0.25f)).normalized * projectileVelocity;
                spawnedProjectile.GetComponent<ProjectileScript>().InitializeProjectile(projectileDamage, launchDirection);
                cannonTimer += cannonCooldown;
            }
        }
        else //decrement the cannon timer
        {
            cannonTimer -= Time.deltaTime;
        }
    }
}
