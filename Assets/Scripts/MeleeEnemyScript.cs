using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyScript : MonoBehaviour
{
    [SerializeField] RangeDetectorScript sensor;
    [SerializeField] float speed = 1;
    [SerializeField] float damagePerSecond = 10;
    [SerializeField] GameObject targetObject = null;
    List<GameObject> objectsInContact;
    private void Start()
    {
        objectsInContact = new List<GameObject>();
    }
    private void Update()
    {
        foreach (var target in objectsInContact)
        {
            print("Damaging Target!");
            target.GetComponent<Health>().ChangeHealth(-damagePerSecond * Time.deltaTime);
            print(target.GetComponent<Health>().health);
        }
    }
    private void FixedUpdate()
    {
        targetObject = sensor.GetClosestObjectInRange();
        if (targetObject == null) return;
        Vector2 targetDirection = (targetObject.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(targetDirection * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        objectsInContact.Add(collision.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        objectsInContact.Remove(collision.gameObject);
    }
}
