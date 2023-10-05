using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customerText;
    private TextMeshProUGUI currentText;

    [SerializeField] private string[] orderRecipes;
    private string currentRecipe;

    [SerializeField] private Transform[] waypoints;
    private float moveSpeed = 0.5f;
    private string state;

    CustomerSpawner customersList;
    private int customerIndex;

    private void Start()
    {
        currentRecipe = orderRecipes[Random.Range(0, orderRecipes.Length)];
        StartCoroutine(ChangeState("spawn"));
        customersList = GameObject.FindWithTag("Spawner").GetComponent<CustomerSpawner>();
        customerIndex = customersList.customers.Count - 1;
    }

    private void Update()
    {
        switch (state)
        {
            case "spawn":
                if (customerIndex == 0 || (!customersList.customers[customerIndex - 1] && customerIndex > 0))
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, moveSpeed * Time.deltaTime);
                } 
                else if (Vector3.Distance(customersList.customers[customerIndex].transform.position, customersList.customers[customerIndex - 1].transform.position) > .8f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, moveSpeed * Time.deltaTime);
                }

                if (Vector3.Distance(transform.position, waypoints[0].transform.position) < .2f)
                {
                    var _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                    var _text = Instantiate(customerText, this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity) as TextMeshProUGUI;
                    _text.text = currentRecipe;
                    _text.transform.SetParent(_canvas.transform, true);
                    _text.transform.localScale = Vector3.one;
                    currentText = _text;

                    state = "order";
                }
                break;

            case "destroy":
                transform.position = Vector3.MoveTowards(transform.position, waypoints[1].transform.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, waypoints[1].transform.position) < .2f)
                {
                    Destroy(currentText.gameObject);
                    Destroy(this.gameObject);
                }
                break;
        }

        if (currentText)
        {
            currentText.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(currentRecipe) && state == "order")
        {
            currentText.text = ":)";
            Destroy(collision.gameObject);
            StartCoroutine(ChangeState("destroy"));
        }
    }

    IEnumerator ChangeState(string newState)
    {
        yield return new WaitForSeconds(1f);
        state = newState;
    }
}
