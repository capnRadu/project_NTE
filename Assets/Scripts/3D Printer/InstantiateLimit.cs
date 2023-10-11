using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLimit : MonoBehaviour
{
    public bool isInstantiated = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isInstantiated = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isInstantiated = false;
        }
    }
}
