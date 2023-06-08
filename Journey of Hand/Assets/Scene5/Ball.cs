using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LaunchBall launcher;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoardArea"))
        {
            
            Invoke("invokeObject",1f);
        }
    }
    private void invokeObject()
    {
        launcher.ReaetTarget();
    }
}
