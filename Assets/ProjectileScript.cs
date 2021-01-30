using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    float projectileLifetime = 5;
    float projectileDamage = 10;
    public void InitializeProjectile(float damage,Vector2 launchVelocity)
    {
        projectileDamage = damage;
        GetComponent<Rigidbody2D>().AddForce(launchVelocity,ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        projectileLifetime -= Time.deltaTime;
        if (projectileLifetime < 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.collider.GetComponent<Health>();
        if (health)
            health.ChangeHealth(-projectileDamage);
        Destroy(gameObject);
    }
}
