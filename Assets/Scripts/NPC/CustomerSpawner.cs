using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    public List<GameObject> customers = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 2f, 10f);
    }

    private void SpawnCustomer()
    {
        var getCustomersCount = GameObject.FindGameObjectsWithTag("Customer");

        if (getCustomersCount.Length < 4)
        {
            GameObject newCustomer = (GameObject)Instantiate(customerPrefab, transform.position, Quaternion.identity);
            customers.Add(newCustomer);
        }
    }
}
