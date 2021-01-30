using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetectorScript : MonoBehaviour
{
    List<GameObject> objectsInRange;
    // Start is called before the first frame update
    void Start()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInRange.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsInRange.Remove(collision.gameObject);
    }
}
