using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairerScript : MonoBehaviour
{
    [SerializeField] RangeDetectorScript sensor;
    [SerializeField] float repairRate = 5;

    // Update is called once per frame
    void Update()
    {
        //Repair all components in range
        foreach(var obj in sensor.GetAllObjectsInRange())
        {
            obj.GetComponent<Health>().ChangeHealth(repairRate * Time.deltaTime);
        }
    }
}
