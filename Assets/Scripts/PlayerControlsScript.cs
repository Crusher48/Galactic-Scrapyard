using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsScript : MonoBehaviour
{
    [SerializeField] GameObject jointPrefab;
    [SerializeField] float gravityForce = 5;

    Vector2 mousePos;
    GameObject grabbedObject = null;
    GameObject activeJoint = null;
    //Called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        GameObject hoveredObject = null;
        LayerMask layers = LayerMask.GetMask("Component", "Enemy");
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f,layers);
        if (hit)
        {
            hoveredObject = hit.collider.gameObject;
        }
        if (Input.GetMouseButtonDown(0))
        {
            grabbedObject = hoveredObject;
        }
        if (Input.GetMouseButtonUp(0))
        {
            grabbedObject = null;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (hoveredObject)
            {
                layers = LayerMask.GetMask("Intangibles");
                RaycastHit2D jointCast = Physics2D.Raycast(mousePos, Vector2.zero, 0, layers);
                if (jointCast)
                {
                    activeJoint = jointCast.collider.gameObject;
                    activeJoint.GetComponent<JointHandlerScript>().DetachJoint();
                }
                else
                {
                    GameObject newJoint = CreateJoint(hoveredObject, mousePos);
                    activeJoint = newJoint;
                }
            }
            else
            {
                activeJoint = null;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (activeJoint)
            {
                if (hoveredObject != null && hoveredObject != activeJoint.transform.parent.gameObject)
                {
                    //attempt to hit an existing joint
                    layers = LayerMask.GetMask("Intangibles");
                    RaycastHit2D jointCast = Physics2D.Raycast(mousePos, Vector2.zero, 0, layers);
                    if (jointCast)
                    {
                        //attach to an existing joint
                        jointCast.collider.GetComponent<JointHandlerScript>().DetachJoint();
                        jointCast.collider.GetComponent<JointHandlerScript>().AttachJoint(activeJoint.GetComponent<JointHandlerScript>());
                    }
                    else
                    {
                        //create a new joint to attach to
                        GameObject secondJoint = CreateJoint(hoveredObject, mousePos);
                        activeJoint.GetComponent<JointHandlerScript>().AttachJoint(secondJoint.GetComponent<JointHandlerScript>());
                    }
                }
                else
                {
                    //joint connection failed, destroy joint
                    Destroy(activeJoint);
                    activeJoint = null;
                }
            }
        }
    }
    //Called once every physics update
    void FixedUpdate()
    {
        if (grabbedObject) //if we have a grabbed object, pull it towards the mouse cursor
        {
            Rigidbody2D grabbedBody = grabbedObject.GetComponent<Rigidbody2D>();
            Vector2 launchDirection = mousePos - (Vector2)grabbedObject.transform.position;
            launchDirection = launchDirection.normalized;
            grabbedBody.AddForce(launchDirection * gravityForce,ForceMode2D.Force);
        }
    }
    //Creates a joint
    GameObject CreateJoint(GameObject target, Vector2 position)
    {
        GameObject newJoint = Instantiate(jointPrefab);
        newJoint.transform.position = (Vector3)position + new Vector3(0, 0, -1);
        newJoint.transform.parent = target.transform;
        newJoint.GetComponent<JointHandlerScript>().AttachToParent();
        return newJoint;
    }
}
