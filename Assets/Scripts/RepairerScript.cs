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
        List<GameObject> repairTargets = sensor.GetAllObjectsInRange();
        //if powered, add ourself as a component
        if (GetComponent<FunctionalComponentRequirements>().timeUntilDepower > 0)
            repairTargets.Add(this.gameObject);
        //remove targets that are at full health
        for (int x = repairTargets.Count-1; x >= 0; x--) 
        {
            Health healthComponent = repairTargets[x].GetComponent<Health>();
            if (healthComponent.health == healthComponent.maxHealth)
                repairTargets.RemoveAt(x);
        }
        //split repair among remaining targets
        foreach (var obj in repairTargets)
        {
            obj.GetComponent<Health>().ChangeHealth((repairRate / repairTargets.Count) * Time.deltaTime);
        }
    }
}
