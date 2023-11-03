using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CustomerOrder : MonoBehaviour
{
    // Text
    [SerializeField] private TextMeshProUGUI customerText;
    private TextMeshProUGUI currentText;

    // Order index number
    private int[] orderRecipesIndex;
    private int currentRecipeIndex = 0;

    // Movement
    [SerializeField] private Transform[] waypoints;
    private float moveSpeed = 0.5f;
    private string state;
    private Animator modelAnimator;

    // List of spawned NPCs
    CustomerSpawner customersList;
    private int customerIndex;

    // Timer
    [SerializeField] private GameObject timerBar;
    TimerBar timerBarScript;
    private GameObject newTimer;
    private float orderTimer = 20f;

    private void Start()
    {
        // Pick a random NPC model and keep a reference to its animator
        int randomModelNumber = Random.Range(0, transform.childCount);
        for (int j = 0; j < transform.childCount; j++)
        {
            if (j != randomModelNumber)
            {
                Destroy(transform.GetChild(j).gameObject);
            }
            else
            {
                transform.GetChild(j).gameObject.SetActive(true);
                modelAnimator = transform.GetChild(j).gameObject.GetComponent<Animator>();
            }
        }

        // Pick a random order between hotdog / hamburger / today's special (custom player recipe)
        int randomOrder = Random.Range(1, 4);
        // Cycle through the recipe ingredients, pick a random number for them, and generate final order index
        switch (randomOrder)
        {
            case 1: // Hamburger
                orderRecipesIndex = new int[7];
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
                break;
            case 2: // Hotdog
                orderRecipesIndex = new int[2];
                for (int i = 0; i < orderRecipesIndex.Length; i++)
                {
                    if (i == 0)
                    {
                        orderRecipesIndex[i] = 1;
                        currentRecipeIndex += orderRecipesIndex[i];
                    }
                    else
                    {
                        orderRecipesIndex[i] = Random.Range(1, 4);
                        currentRecipeIndex *= 10;
                        currentRecipeIndex += orderRecipesIndex[i];
                    }
                }
                break;
            case 3: // Today's special
                orderRecipesIndex = new int[0];
                break;
        }
        Debug.Log(currentRecipeIndex);

        StartCoroutine(ChangeState("spawn", true, false));

        customersList = GameObject.FindWithTag("Spawner").GetComponent<CustomerSpawner>();
        customerIndex = customersList.customers.Count - 1;

        timerBarScript = timerBar.GetComponent<TimerBar>();
    }

    private void Update() 
    {
        // Cycle through states
        switch (state)
        {
            case "spawn":
                // Check whether or not there is another NPC in front of this one and change movement and animation accordingly
                if ((customerIndex == 0 || (!customersList.customers[customerIndex - 1] && customerIndex > 0)) ||
                    (Vector3.Distance(customersList.customers[customerIndex].transform.position, customersList.customers[customerIndex - 1].transform.position) > .8f))
                {
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, moveSpeed * Time.deltaTime);
                    modelAnimator.SetBool("isWalking", true);
                }
                else
                {
                    modelAnimator.SetBool("isWalking", false);
                }

                // Check if NPC reached the counter
                if (Vector3.Distance(transform.position, waypoints[0].transform.position) < .2f)
                {
                    var _canvas2 = GameObject.Find("Canvas 2").GetComponent<Canvas>();
                    var _text = Instantiate(customerText, this.transform.position, Quaternion.identity) as TextMeshProUGUI;

                    // Check ordered recipe and display text with each number of required ingredients
                    if (orderRecipesIndex.Length == 7)
                    {
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
                                    _text.text += "Tomatoes x" + orderRecipesIndex[i] + "\n";
                                    break;
                                case 4:
                                    _text.text += "Onions x" + orderRecipesIndex[i] + "\n";
                                    break;
                                case 5:
                                    _text.text += "Salad x" + orderRecipesIndex[i] + "\n";
                                    break;
                                case 6:
                                    _text.text += "Top Bun x" + orderRecipesIndex[i] + "\n";
                                    break;
                            }
                        }
                    }
                    else if (orderRecipesIndex.Length == 2)
                    {
                        for (int i = 0; i < orderRecipesIndex.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    _text.text += "Hotdog Bun x" + orderRecipesIndex[i] + " ";
                                    break;
                                case 1:
                                    _text.text += "Sausage x" + orderRecipesIndex[i] + "\n";
                                    break;
                            }
                        }
                    }
                    else if (orderRecipesIndex.Length == 0)
                    {
                        _text.text += "Today's Special";
                    }

                    _text.transform.SetParent(_canvas2.transform, true);
                    _text.transform.localScale = Vector3.one;
                    currentText = _text;

                    newTimer = Instantiate(timerBar, this.transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity) as GameObject;
                    // timerBarScript.maxTime = orderTimer;
                    newTimer.transform.SetParent(_canvas2.transform, true);
                    newTimer.transform.localScale = Vector3.one;

                    state = "order";
                    modelAnimator.SetBool("isWalking", false);
                }
                break;

            case "order":
                // Check if the timer has ran out before the order was received
                if (newTimer && newTimer.GetComponent<TimerBar>().timerBar.fillAmount <= 0)
                {
                    state = "destroy";
                    modelAnimator.SetBool("isWalking", true);
                    this.transform.rotation = Quaternion.Euler(0, -90, 0);
                    currentText.text = ":(";
                    Destroy(newTimer.gameObject);
                }
                break;

            case "destroy":
                // Go to destroy point
                transform.position = Vector3.MoveTowards(transform.position, waypoints[1].transform.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, waypoints[1].transform.position) < .2f)
                {
                    Destroy(currentText.gameObject);
                    Destroy(this.gameObject);
                }
                break;
        }

        // Update text position
        if (currentText)
        {
            currentText.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ordered recipe has all the required ingredients
        if (state == "order")
        {
            if ( (collision.gameObject.CompareTag("Bottom Bun") && orderRecipesIndex.Length == 7 && currentRecipeIndex == collision.gameObject.GetComponent<Recipe>().orderIndex) ||
            (collision.gameObject.CompareTag("Hotdog Bun") && orderRecipesIndex.Length == 2 && currentRecipeIndex == collision.gameObject.GetComponent<HotdogRecipe>().orderIndex) ||
            ((collision.gameObject.CompareTag("Bottom Bun") || collision.gameObject.CompareTag("Hotdog Bun")) && orderRecipesIndex.Length == 0) )
            {
                currentText.text = ":)";
                Destroy(collision.gameObject);
                StartCoroutine(ChangeState("destroy", true, true));
                Destroy(newTimer.gameObject);
            }
        }
    }

    // Change states, while updating animation type, and model rotation
    IEnumerator ChangeState(string newState, bool isWalking, bool rotate)
    {
        yield return new WaitForSeconds(1f);
        state = newState;
        modelAnimator.SetBool("isWalking", isWalking);

        if (rotate)
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}