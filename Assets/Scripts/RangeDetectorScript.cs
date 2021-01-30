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
    public GameObject GetObjectInRange()
    {
        if (objectsInRange.Count == 0)
            return null;
        return objectsInRange[0];
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
