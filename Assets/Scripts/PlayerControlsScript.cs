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
    // Start is called before the first frame update
    void Start()
    {
        
    }
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
            print("Selecting Object!");
            grabbedObject = hoveredObject;
        }
        if (Input.GetMouseButtonUp(0))
        {
            print("Deselecting Object!");
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
                    print("Existing Joint Hit!");
                    activeJoint = jointCast.collider.gameObject;
                    activeJoint.GetComponent<JointHandlerScript>().DetatchJoint();
                }
                else
                {
                    print("Placing Joint");
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

                    layers = LayerMask.GetMask("Intangibles");
                    RaycastHit2D jointCast = Physics2D.Raycast(mousePos, Vector2.zero, 0, layers);
                    if (jointCast)
                    {
                        print("Attaching to existing joint");
                        jointCast.collider.GetComponent<JointHandlerScript>().DetatchJoint();
                        jointCast.collider.GetComponent<JointHandlerScript>().AttachJoint(activeJoint.GetComponent<JointHandlerScript>());
                    }
                    else
                    {
                        print("Attaching to new joint");
                        GameObject secondJoint = CreateJoint(hoveredObject, mousePos);
                        activeJoint.GetComponent<JointHandlerScript>().AttachJoint(secondJoint.GetComponent<JointHandlerScript>());
                    }
                }
                else
                {
                    print("Joint Failed!");
                    Destroy(activeJoint);
                    activeJoint = null;
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (grabbedObject)
        {
            Rigidbody2D grabbedBody = grabbedObject.GetComponent<Rigidbody2D>();
            Vector2 launchDirection = mousePos - (Vector2)grabbedObject.transform.position;
            launchDirection = launchDirection.normalized;
            grabbedBody.AddForce(launchDirection * gravityForce,ForceMode2D.Force);
        }
    }
    GameObject CreateJoint(GameObject target, Vector2 position)
    {
        GameObject newJoint = Instantiate(jointPrefab);
        newJoint.transform.position = (Vector3)position + new Vector3(0, 0, -1);
        newJoint.transform.parent = target.transform;
        newJoint.GetComponent<JointHandlerScript>().AttachToParent();
        return newJoint;
    }
}
