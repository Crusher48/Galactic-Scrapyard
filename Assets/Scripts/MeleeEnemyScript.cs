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
        //attempt to get the target
        targetObject = sensor.GetClosestObjectInRange();
        //for each enemy in range, damage it
        for (int x = objectsInContact.Count-1; x >= 0; x--)
        {
            GameObject target = objectsInContact[x];
            target.GetComponent<Health>().ChangeHealth(-damagePerSecond * Time.deltaTime);
        }
    }
    //if there is a target, move towards it
    private void FixedUpdate()
    {
        if (targetObject == null) return;
        Vector2 targetDirection = ((Vector2)targetObject.transform.position - ((Vector2)transform.position+GetComponent<Rigidbody2D>().velocity*0.25f)).normalized;
        GetComponent<Rigidbody2D>().AddForce(targetDirection * speed);
    }
    //add objects to the list of objects that take damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        objectsInContact.Add(collision.gameObject);
    }
    //remove objects from the list of objects that take damage
    private void OnCollisionExit2D(Collision2D collision)
    {
        objectsInContact.Remove(collision.gameObject);
    }
}
