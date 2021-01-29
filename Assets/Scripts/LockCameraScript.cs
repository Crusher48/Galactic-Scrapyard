using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraScript : MonoBehaviour
{
    public Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = targetObject.transform.position + new Vector3(0,0,-10);
    }
}
