using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 1f, 20f);
    }

    private void SpawnCustomer()
    {
        var getCount = GameObject.FindGameObjectsWithTag("Customer");

        if(getCount.Length <= 1)
        {
            Instantiate(customerPrefab);
        }
    }
}
