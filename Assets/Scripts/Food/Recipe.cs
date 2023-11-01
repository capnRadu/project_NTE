using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Recipe : MonoBehaviour
{
    [SerializeField] private GameObject topBunPrefab;
    private int topBunIndex = 1;

    [SerializeField] private GameObject saladPrefab;
    private int saladIndex = 10;

    [SerializeField] private GameObject onionsPrefab;
    private int onionsIndex = 100;

    [SerializeField] private GameObject tomatoesPrefab;
    private int tomatoesIndex = 1000;

    [SerializeField] private GameObject cheesePrefab;
    private int cheeseIndex = 10000;

    [SerializeField] private GameObject pattyPrefab;
    private int pattyIndex = 100000;

    public int orderIndex = 1000000;

    private float spacing;

    private void Start()
    {
        spacing = GetComponent<BoxCollider>().size.y / 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Top Bun"))
        {
            AddIngredient(topBunPrefab, collision);
            orderIndex += topBunIndex;
            Debug.Log("orderIndex " + orderIndex);
        }

        if (collision.gameObject.CompareTag("Patty"))
        {
            AddIngredient(pattyPrefab, collision);
            orderIndex += pattyIndex;
            Debug.Log("orderIndex " + orderIndex);
        }

        if (collision.gameObject.CompareTag("Cheese"))
        {
            AddIngredient(cheesePrefab, collision);
            orderIndex += cheeseIndex;
            Debug.Log("orderIndex " + orderIndex);
        }

        if (collision.gameObject.CompareTag("Salad"))
        {
            AddIngredient(saladPrefab, collision);
            orderIndex += saladIndex;
            Debug.Log("orderIndex " + orderIndex);
        }

        if (collision.gameObject.CompareTag("Onions"))
        {
            AddIngredient(onionsPrefab, collision);
            orderIndex += onionsIndex;
            Debug.Log("orderIndex " + orderIndex);
        }

        if (collision.gameObject.CompareTag("Tomatoes"))
        {
            AddIngredient(tomatoesPrefab, collision);
            orderIndex += tomatoesIndex;
            Debug.Log("orderIndex " + orderIndex);
        }
    }

    protected void AddIngredient(GameObject ingredientPrefab, Collision collision)
    {
        spacing += collision.gameObject.GetComponent<BoxCollider>().size.y / 2;

        GameObject newIngredient = (GameObject)Instantiate(ingredientPrefab, transform.position + new Vector3(0, spacing, 0), transform.rotation);
        newIngredient.transform.parent = transform;
        GetComponent<BoxCollider>().center += new Vector3(0, collision.gameObject.GetComponent<BoxCollider>().size.y / 2, 0);
        GetComponent<BoxCollider>().size += new Vector3(0, collision.gameObject.GetComponent<BoxCollider>().size.y, 0);
        
        spacing += collision.gameObject.GetComponent<BoxCollider>().size.y / 2;

        Destroy(collision.gameObject);
    }
}
