using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnFood : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyObject", 120);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
