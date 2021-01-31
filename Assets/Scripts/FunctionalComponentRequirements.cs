using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionalComponentRequirements : MonoBehaviour
{
    [SerializeField] GameObject targetToDisable;
    Health healthComponent;
    public float timeUntilDepower = 0;
    private void Awake()
    {
        healthComponent = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if there is a powersource
        int layers = LayerMask.GetMask("PowerZone");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layers);
        if (hit)
            timeUntilDepower = 5;
        else
            timeUntilDepower = Mathf.Max(0, timeUntilDepower - Time.deltaTime);
        //final decision, deactivate if damaged or unpowered
        targetToDisable.SetActive(GetComponent<Health>().IsDamaged() == false && timeUntilDepower > 0);
    }
}
