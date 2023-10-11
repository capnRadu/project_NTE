using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Recipe : MonoBehaviour
{
    [SerializeField] private GameObject topBunPrefab;
    private int topBunIndex = 1;

    [SerializeField] private GameObject pattyPrefab;
    private int baconIndex = 10;

    [SerializeField] private GameObject cheesePrefab;
    private int cheeseIndex = 100;

    [SerializeField] private GameObject baconPrefab;
    private int pattyIndex = 1000;

    public int orderIndex = 10000;

    private float spacing = 0f;

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

        if (collision.gameObject.CompareTag("Bacon"))
        {
            AddIngredient(baconPrefab, collision);
            orderIndex += baconIndex;
            Debug.Log("orderIndex " + orderIndex);
        }
    }

    protected void AddIngredient(GameObject ingredientPrefab, Collision collision)
    {
        GameObject newIngredient = (GameObject)Instantiate(ingredientPrefab, transform.position + new Vector3(0, collision.transform.localScale.y + spacing, 0), transform.rotation);
        newIngredient.transform.parent = transform;
        GetComponent<BoxCollider>().center += new Vector3(0, collision.gameObject.GetComponent<BoxCollider>().size.y / 2, 0);
        GetComponent<BoxCollider>().size += new Vector3(0, collision.gameObject.GetComponent<BoxCollider>().size.y, 0);
        
        spacing += collision.transform.localScale.y;

        Destroy(collision.gameObject);
    }
}
