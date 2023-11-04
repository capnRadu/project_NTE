using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    public List<GameObject> customers = new List<GameObject>();
    public int customerNumber = 0;

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 2f, 15f);
    }

    private void SpawnCustomer()
    {
        customerNumber++;
        var getCustomersCount = GameObject.FindGameObjectsWithTag("Customer");

        if (getCustomersCount.Length < 4)
        {
            GameObject newCustomer = (GameObject)Instantiate(customerPrefab, transform.position, Quaternion.Euler(0, -90, 0));
            customers.Add(newCustomer);
        }
    }
}
