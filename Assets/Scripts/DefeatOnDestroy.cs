using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject.Find("MainCamera").GetComponent<PlayerControlsScript>().EndGame("Defeat...");
    }
}
