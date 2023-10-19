using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customerText;
    private TextMeshProUGUI currentText;

    [SerializeField] private string[] orderRecipes;
    private string currentRecipe;

    [SerializeField] private int[] orderRecipesIndex;
    private int currentRecipeIndex = 0;

    [SerializeField] private Transform[] waypoints;
    private float moveSpeed = 0.5f;
    private string state;

    CustomerSpawner customersList;
    private int customerIndex;

    [SerializeField] private GameObject timerBar;
    TimerBar timerBarScript;
    private float orderTimer = 20f;
    private GameObject newTimer;

    private void Start()
    {
        // currentRecipe = orderRecipes[Random.Range(0, orderRecipes.Length)];
        for (int i = 0; i < orderRecipesIndex.Length; i++)
        {
            if (i == 0)
            {
                orderRecipesIndex[i] = 1;
                currentRecipeIndex += orderRecipesIndex[i];
            }
            else
            {
                orderRecipesIndex[i] = Random.Range(0, 4);
                currentRecipeIndex *= 10;
                currentRecipeIndex += orderRecipesIndex[i];
            }
        }
        Debug.Log(currentRecipeIndex);

        StartCoroutine(ChangeState("spawn"));
        customersList = GameObject.FindWithTag("Spawner").GetComponent<CustomerSpawner>();
        customerIndex = customersList.customers.Count - 1;

        timerBarScript = timerBar.GetComponent<TimerBar>();
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
                    var _canvas2 = GameObject.Find("Canvas 2").GetComponent<Canvas>();
                    var _text = Instantiate(customerText, this.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity) as TextMeshProUGUI;
                    // _text.text = currentRecipe;

                    for (int i = 0; i < orderRecipesIndex.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                _text.text += "Bottom Bun x" + orderRecipesIndex[i] + " ";
                                break;
                            case 1:
                                _text.text += "Patty x" + orderRecipesIndex[i] + "\n";
                                break;
                            case 2:
                                _text.text += "Cheese x" + orderRecipesIndex[i] + " ";
                                break;
                            case 3:
                                _text.text += "Bacon x" + orderRecipesIndex[i] + "\n";
                                break;
                            case 4:
                                _text.text += "Top Bun x" + orderRecipesIndex[i] + "\n";
                                break;
                        }
                    }

                    _text.transform.SetParent(_canvas2.transform, true);
                    _text.transform.localScale = Vector3.one;
                    currentText = _text;

                    newTimer = Instantiate(timerBar, this.transform.position + new Vector3(0f, 0.7f, 0f), Quaternion.identity) as GameObject;
                    // timerBarScript.maxTime = orderTimer;
                    newTimer.transform.SetParent(_canvas2.transform, true);
                    newTimer.transform.localScale = Vector3.one;

                    state = "order";
                }
                break;

            case "order":
                if (newTimer && newTimer.GetComponent<TimerBar>().timerBar.fillAmount <= 0)
                {
                    state = "destroy";
                    currentText.text = ":(";
                    Destroy(newTimer.gameObject);
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
        if (/* collision.gameObject.CompareTag(currentRecipe) */ collision.gameObject.CompareTag("Bottom Bun") && currentRecipeIndex == collision.gameObject.GetComponent<Recipe>().orderIndex && state == "order")
        {
            currentText.text = ":)";
            Destroy(collision.gameObject);
            StartCoroutine(ChangeState("destroy"));
            Destroy(newTimer.gameObject);
        }
    }

    IEnumerator ChangeState(string newState)
    {
        yield return new WaitForSeconds(1f);
        state = newState;
    }
}
