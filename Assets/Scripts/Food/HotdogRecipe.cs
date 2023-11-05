using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogRecipe : MonoBehaviour
{
    [SerializeField] private GameObject sausagePrefab;
    private int sausageIndex = 1;

    public int orderIndex = 10;

    private float spacing;

    private void Start()
    {
        spacing = GetComponent<BoxCollider>().size.y / 2;

        Invoke("DestroyObject", 120);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sausage"))
        {
            AddIngredient(sausagePrefab, collision);
            orderIndex += sausageIndex;
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

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}