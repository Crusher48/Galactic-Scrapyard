using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public string objectname;
    public string description;
    public float health = 100;
    public float maxHealth = 100;
    Color baseColor;
    private void Awake()
    {
        baseColor = GetComponent<SpriteRenderer>().color;
        health = maxHealth;
    }
    //adds or removes health from the thing
    public void ChangeHealth(float netChange)
    {
        health = Mathf.Clamp(health+netChange,0,maxHealth);
        if (health <= 0)
            Destroy(gameObject);
        GetComponent<SpriteRenderer>().color = baseColor * (0.5f+0.5f*(health / maxHealth));
    }
    //determines if the component is damaged and nonfunctional
    public bool IsDamaged()
    {
        return health <= maxHealth / 2;
    }
}
