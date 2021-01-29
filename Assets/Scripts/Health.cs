using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public void ChangeHealth(float netChange)
    {
        health += netChange;
        if (health < 0)
            Destroy(gameObject);
    }
    public bool IsDamaged()
    {
        return health <= maxHealth / 2;
    }
}
