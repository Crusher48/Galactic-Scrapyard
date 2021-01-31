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
    [SerializeField] GameObject turret;
    float cannonTimer = 0;
    // Update is called once per frame
    void Update()
    {
        GameObject target = sensor.GetClosestObjectInRange();
        if (target)
        {
            LookCannonAt(target.transform.position);
        }
        if (cannonTimer <= 0) //attempt to fire the cannon
        {
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
    //sets the rotation of the node
    public void LookCannonAt(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        Vector3 eulers = turret.transform.eulerAngles;
        eulers.z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg+90;
        turret.transform.eulerAngles = eulers;
    }
}
