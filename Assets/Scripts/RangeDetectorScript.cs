using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Range detector sensor, attach this to a submodule for a cheap way to get targets
public class RangeDetectorScript : MonoBehaviour
{
    List<GameObject> objectsInRange;
    // Start is called before the first frame update
    void Awake()
    {
        objectsInRange = new List<GameObject>();
    }
    //gets the first object in range
    public GameObject GetObjectInRange()
    {
        if (objectsInRange.Count == 0)
            return null;
        return objectsInRange[0];
    }
    //gets the closest object in range
    public GameObject GetClosestObjectInRange()
    {
        GameObject closestObject = null;
        float closestDistance = 9999;
        foreach (var obj in objectsInRange)
        {
            float distance = (obj.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestObject = obj;
                closestDistance = distance;
            }    
        }
        return closestObject;
    }
    //gets all objects in range
    public List<GameObject> GetAllObjectsInRange()
    {
        return new List<GameObject>(objectsInRange);
    }
    //when an object enters the trigger, add it to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInRange.Add(collision.gameObject);
    }
    //when an object leaves the trigger, remove it from the list
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsInRange.Remove(collision.gameObject);
    }
}
