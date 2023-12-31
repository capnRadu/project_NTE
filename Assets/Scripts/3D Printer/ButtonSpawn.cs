using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSpawn : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private GameObject printer;
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private UnityEvent onRelease;
    InstantiateLimit instantiateLimit;
    PrinterDamage printerDamage;
    private GameObject presser;
    private bool isPressed;

    private void Start()
    {
        isPressed = false;
        instantiateLimit = spawnPoint.GetComponent<InstantiateLimit>();
        printerDamage = printer.GetComponent<PrinterDamage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && (other.gameObject.CompareTag("Left Hand") || other.gameObject.CompareTag("Right Hand")))
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void SpawnObject(AudioSource soundEffect)
    {
        if (!instantiateLimit.isInstantiated && printerDamage.printerHealth > 0)
        {
            GameObject newObject = Instantiate(prefabObject, spawnPoint.transform.position, Quaternion.identity);
            soundEffect.Play();
        }
    }
}
