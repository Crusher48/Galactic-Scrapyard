using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairerScript : MonoBehaviour
{
    [SerializeField] RangeDetectorScript sensor;
    [SerializeField] float repairRate = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var obj in sensor.GetAllObjectsInRange())
        {
            obj.GetComponent<Health>().ChangeHealth(repairRate * Time.deltaTime);
        }
    }
}
