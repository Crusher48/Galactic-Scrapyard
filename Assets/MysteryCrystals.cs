using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCrystals : MonoBehaviour
{
    [SerializeField] int crystalsNeeded = 5;
    [SerializeField] float repulsionForce = 1;
    [SerializeField] RangeDetectorScript sensor;
    bool victoryTriggered = false;
    // Update is called once per frame
    void Update()
    {
        var allObjectsInRange = sensor.GetAllObjectsInRange();
        int crystalsFound = 1;
        foreach (var otherObject in allObjectsInRange)
        {
            if (otherObject.GetComponent<MysteryCrystals>())
            {
                crystalsFound++;
            }
            else
            {
                otherObject.GetComponent<Rigidbody2D>().AddForce((transform.position - otherObject.transform.position).normalized * repulsionForce);
            }
        }
        if (crystalsFound >= crystalsNeeded && victoryTriggered == false)
        {
            GameObject.Find("MainCamera").GetComponent<PlayerControlsScript>().EndGame("Victory!");
            victoryTriggered = true;
        }
    }
}
