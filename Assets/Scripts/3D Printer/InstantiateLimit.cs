using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class InstantiateLimit : MonoBehaviour
{
    public bool isInstantiated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isInstantiated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isInstantiated = false;
        }
    }
}
