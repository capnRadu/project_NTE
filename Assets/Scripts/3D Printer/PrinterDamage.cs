using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PrinterDamage : MonoBehaviour
{
    [SerializeField] private Image backgroundBar;
    [SerializeField] private Image timerBar;
    public float maxHealth = 100f;
    public float printerHealth;
    private bool damageBar = false;

    [SerializeField] private ParticleSystem smokeParticles;
    private ParticleSystem.EmissionModule smokeEmission;
    private bool isSmoke = false;

    [SerializeField] private Transform spawnPoint;
    InstantiateLimit instantiateLimit;

    [SerializeField] private GameObject spawner;
    CustomerSpawner customerSpawner;

    private void Start()
    {
        printerHealth = maxHealth;
        backgroundBar.transform.position = transform.position + new Vector3(0.1f, 0.2f, 0.15f);

        instantiateLimit = spawnPoint.GetComponent<InstantiateLimit>();

        smokeParticles.transform.position = transform.position + new Vector3(0.1f, 0.2f, 0.15f);
        smokeEmission = smokeParticles.emission;
        smokeEmission.rateOverTime = 0;

        customerSpawner = spawner.GetComponent<CustomerSpawner>();
    }

    private void Update()
    {
        if (customerSpawner.customerNumber > 14 && !damageBar)
        {
            backgroundBar.gameObject.SetActive(true);
            damageBar = true;
        }

        if (printerHealth <= 0 && !isSmoke)
        {
            smokeEmission.rateOverTime = 50;
            isSmoke = true;
        }
        else if (printerHealth > 0 && isSmoke)
        {
            smokeEmission.rateOverTime = 0;
            isSmoke = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cleaner") && printerHealth < maxHealth && damageBar)
        {
            if (other.gameObject.GetComponent<CleaningObject>().isMoving)
            {
                printerHealth += Time.deltaTime * 15;
                timerBar.fillAmount = printerHealth / maxHealth;
            }
        }
    }

    public void IncreaseDeterioration()
    {
        if (printerHealth > 0 && !instantiateLimit.isInstantiated && damageBar)
        {
            printerHealth -= 5;
            timerBar.fillAmount = printerHealth / maxHealth;
        }
    }
}
