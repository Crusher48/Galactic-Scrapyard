using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointHandlerScript : MonoBehaviour
{
    SpringJoint2D springJoint;
    LineRenderer jointRenderer;
    JointHandlerScript otherHandler = null;
    private void Awake()
    {
        springJoint = GetComponent<SpringJoint2D>();
        jointRenderer = GetComponent<LineRenderer>();
    }
    public void AttachToParent()
    {
        FixedJoint2D fixedJoint = GetComponent<FixedJoint2D>();
        fixedJoint.connectedBody = transform.parent.GetComponent<Rigidbody2D>();
    }
    //attaches this joint to another joint object
    public void AttachJoint(JointHandlerScript other)
    {
        springJoint.connectedBody = other.GetComponent<Rigidbody2D>();
        springJoint.enabled = true;
        otherHandler = other;
        other.otherHandler = this;
    }
    //detaches this joint
    public void DetatchJoint()
    {
        if (otherHandler != null)
        {
            springJoint.enabled = false;
            otherHandler.otherHandler = null;
            Destroy(otherHandler.gameObject);
        }
    }
    private void OnDestroy()
    {
        DetatchJoint();
    }
    //do the line renderer
    private void Update()
    {
        if (springJoint.enabled)
        {
            jointRenderer.enabled = true;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(transform.position);
            positions.Add(springJoint.connectedBody.transform.position);
            jointRenderer.SetPositions(positions.ToArray());
        }
        else
        {
            jointRenderer.enabled = false;
        }
    }
}
