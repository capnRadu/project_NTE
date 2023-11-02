using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;

    public Transform head;
    private float spawnDistance = 0.8f;

    [SerializeField] private Canvas adCanvas;

    private void Start()
    {
        InvokeRepeating("DisplayAd", 5f, 10f);
    }

    void Update()
    {
        /*
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        */

        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;

        if (Vector3.Distance(transform.position, GameObject.FindWithTag("Right Hand").transform.position) < 0.2f || Vector3.Distance(transform.position, GameObject.FindWithTag("Left Hand").transform.position) < 0.2f)
        {
            adCanvas.gameObject.SetActive(false);
        }
    }

    private void DisplayAd()
    {
        adCanvas.gameObject.SetActive(true);
    }
}
