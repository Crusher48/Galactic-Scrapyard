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
    //attach the fixed joint to the object this joint is on
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
    public void DetachJoint()
    {
        if (otherHandler != null)
        {
            springJoint.enabled = false;
            otherHandler.otherHandler = null;
            Destroy(otherHandler.gameObject);
        }
    }
    //detach on destruction
    private void OnDestroy()
    {
        DetachJoint();
    }
    //do the line renderer
    private void Update()
    {
        if (springJoint.enabled) //line from this joint to the other joint
        {
            jointRenderer.enabled = true;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(transform.position);
            positions.Add(springJoint.connectedBody.transform.position);
            jointRenderer.SetPositions(positions.ToArray());
        }
        else //disable line renderer
        {
            jointRenderer.enabled = false;
        }
    }
}
