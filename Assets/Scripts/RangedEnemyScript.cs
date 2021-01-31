using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyScript : MonoBehaviour
{
    [SerializeField] RangeDetectorScript sensor;
    [SerializeField] float speed = 1;
    [SerializeField] GameObject forceSource;
    GameObject targetObject = null;
    private void Update()
    {
        //attempt to get the target
        targetObject = sensor.GetClosestObjectInRange();
        if (targetObject == null)
            targetObject = GameObject.Find("PlayerShip");
        //for each enemy in range, damage it
    }
    //if there is a target, move towards it
    private void FixedUpdate()
    {
        if (targetObject == null) return;
        //get the direction to the target
        Vector2 directionToTarget = ((Vector2)targetObject.transform.position - ((Vector2)transform.position + GetComponent<Rigidbody2D>().velocity * 0.25f)).normalized;
        //get the actual target position
        Vector2 targetLocation = (Vector2)targetObject.transform.position - 5 * directionToTarget;
        Vector2 targetDirection = (targetLocation - (Vector2)transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForceAtPosition(targetDirection * speed, forceSource.transform.position);
    }
}
