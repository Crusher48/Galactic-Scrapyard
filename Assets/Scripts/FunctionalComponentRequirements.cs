using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionalComponentRequirements : MonoBehaviour
{
    [SerializeField] GameObject targetToDisable;
    Health healthComponent;
    float timeSinceLastPower = 0;
    private void Awake()
    {
        healthComponent = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthComponent.IsDamaged()) //if the component is damaged, deactivate it
            targetToDisable.SetActive(false);
        else //check to see if there is a powersource
        {
            int layers = LayerMask.GetMask("PowerZone");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, layers);
            if (hit)
                targetToDisable.SetActive(true);
            else
                targetToDisable.SetActive(false);
        }
    }
}
