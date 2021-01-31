using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] AudioClip projectileAudio;
    float projectileLifetime = 5;
    float projectileDamage = 10;
    //initialize the projectile with the given damage and velocity
    public void InitializeProjectile(float damage,Vector2 launchVelocity)
    {
        AudioObject.CreateAudioObject(projectileAudio,transform.position);
        projectileDamage = damage;
        GetComponent<Rigidbody2D>().AddForce(launchVelocity,ForceMode2D.Impulse);
    }
    //decrement lifetime, destroy when it hits zero
    void Update()
    {
        projectileLifetime -= Time.deltaTime;
        if (projectileLifetime < 0)
            Destroy(gameObject);
    }
    //Destroy the projectile and damage whatever was hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.collider.GetComponent<Health>();
        if (health)
            health.ChangeHealth(-projectileDamage);
        Destroy(gameObject);
    }
}
