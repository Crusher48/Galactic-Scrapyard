using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsScript : MonoBehaviour
{
    [SerializeField] GameObject audioObjectReference;
    [SerializeField] UIHandler canvasHandler;
    [SerializeField] LineRenderer gravityForceRenderer;
    [SerializeField] GameObject jointPrefab;
    [SerializeField] float baseGravityForce = 5;
    [SerializeField] float gravityForceBonus = 5;
    [SerializeField] float maxJointLength = 5;
    [SerializeField] AudioClip tetherSoundEffect;
    [SerializeField] AudioSource thrustAudioSource;

    Vector2 mousePos;
    GameObject grabbedObject = null;
    GameObject activeJoint = null;
    Vector2 dragPointOffset = Vector2.zero;
    Vector2 hoveredHitPoint;
    float cameraSize = 8;
    private void Awake()
    {
        AudioObject.audioObjectReference = audioObjectReference;
    }
    //Called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            canvasHandler.OpenPauseMenu();
        if (Input.GetButton("Jump"))
        {
            Camera.main.orthographicSize = 50;
        }
        else
        {
            cameraSize += -Input.mouseScrollDelta.y;
            cameraSize = Mathf.Clamp(cameraSize, 5, 20);
            Camera.main.orthographicSize = cameraSize;
        }
        //get the mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        GameObject hoveredObject = null;
        //get a hovered component or enemy
        LayerMask layers = LayerMask.GetMask("Component", "Enemy", "Asteroids");
        RaycastHit2D hit = Physics2D.CircleCast(mousePos, 0.2f, Vector2.zero,0,layers);
        if (hit)
        {
            hoveredObject = hit.collider.gameObject;
            hoveredHitPoint = hit.point;
        }
        if (hoveredObject)
            canvasHandler.PrintObjectDetails(hoveredObject.GetComponent<Health>());
        else
            canvasHandler.HideObjectDetailsPanel();
        //select an object to grab
        if (Input.GetMouseButtonDown(0))
        {
            grabbedObject = hoveredObject;
            dragPointOffset = grabbedObject.transform.InverseTransformPoint(hoveredHitPoint);
            thrustAudioSource.Play();
        }
        //unselect a grabbed object
        if (Input.GetMouseButtonUp(0))
        {
            grabbedObject = null;
            thrustAudioSource.Stop();
        }
        //attach one end of a joint to the hovered object
        if (Input.GetMouseButtonDown(1))
        {
            if (grabbedObject) //if grabbing an object, attach the joint at the grab point
            {
                GameObject newJoint = CreateJoint(grabbedObject, grabbedObject.transform.TransformPoint(dragPointOffset));
                activeJoint = newJoint;
            }
            else if (hoveredObject)
            {
                GameObject newJoint = CreateJoint(hoveredObject, hoveredHitPoint);
                activeJoint = newJoint;
            }
            else
            {
                activeJoint = null;
            }
        }
        //complete a joint attach
        if (Input.GetMouseButtonUp(1))
        {
            if (activeJoint)
            {
                if (hoveredObject != null && hoveredObject != activeJoint.transform.parent.gameObject)
                {
                    //create a new joint to attach to
                    GameObject secondJoint = CreateJoint(hoveredObject, hoveredHitPoint);
                    activeJoint.GetComponent<JointHandlerScript>().AttachJoint(secondJoint.GetComponent<JointHandlerScript>(),maxJointLength);
                    AudioObject.CreateAudioObject(tetherSoundEffect, activeJoint.transform.position);
                }
                else
                {
                    //joint connection failed, destroy joint
                    Destroy(activeJoint);
                    activeJoint = null;
                }
            }
        }
        //Detach joints
        if (Input.GetMouseButton(2))
        {
            layers = LayerMask.GetMask("Intangibles");
            RaycastHit2D jointCast = Physics2D.Raycast(mousePos, Vector2.zero, 0, layers);
            if (jointCast)
                Destroy(jointCast.collider.gameObject);
        }
        //update line renderer
        if (grabbedObject)
        {
            gravityForceRenderer.enabled = true;
            List<Vector3> positions = new List<Vector3>();
            positions.Add(grabbedObject.transform.TransformPoint(dragPointOffset));
            positions.Add(mousePos);
            gravityForceRenderer.SetPositions(positions.ToArray());
        }
        else
        {
            gravityForceRenderer.enabled = false;
        }
    }
    //Called once every physics update
    void FixedUpdate()
    {
        if (grabbedObject) //if we have a grabbed object, pull it towards the mouse cursor
        {
            Rigidbody2D grabbedBody = grabbedObject.GetComponent<Rigidbody2D>();
            Vector2 launchDirection = mousePos - ((Vector2)grabbedBody.transform.TransformPoint(dragPointOffset) + grabbedBody.velocity*0.25f); //adds a small amount of velocity negation to this
            launchDirection = launchDirection.normalized;//launchDirection /= Mathf.Max(launchDirection.magnitude, 1);
            //amplify from gravity zones
            int layers = LayerMask.GetMask("ForceZone");
            var hits = Physics2D.RaycastAll(grabbedBody.transform.TransformPoint(dragPointOffset), Vector2.zero, 0, layers);
            grabbedBody.AddForceAtPosition(launchDirection * (baseGravityForce+gravityForceBonus*hits.Length), grabbedBody.transform.TransformPoint(dragPointOffset), ForceMode2D.Force);
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
    public void EndGame(string endText)
    {
        canvasHandler.OpenPauseMenu(endText);
    }
}
