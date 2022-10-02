using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Score scoreScript;
    private void OnTriggerEnter(Collider other)
    {
        scoreScript.AddLeavingtMinion();
        Debug.Log("Death Zone");
        Destroy(other.gameObject);
        
    }
}
