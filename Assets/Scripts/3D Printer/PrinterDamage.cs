using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PrinterDamage : MonoBehaviour
{
    [SerializeField] private Image backgroundBar;
    [SerializeField] private Image timerBar;
    public float maxHealth = 60f;
    public float printerHealth;

    [SerializeField] private ParticleSystem smokeParticles;
    private ParticleSystem.EmissionModule smokeEmission;
    private bool isSmoke = false;

    [SerializeField] private Transform spawnPoint;
    InstantiateLimit instantiateLimit;

    private void Start()
    {
        printerHealth = maxHealth;
        backgroundBar.transform.position = transform.position + new Vector3(0.1f, 0.2f, 0.15f);
        backgroundBar.gameObject.SetActive(true);

        instantiateLimit = spawnPoint.GetComponent<InstantiateLimit>();

        smokeParticles.transform.position = transform.position + new Vector3(0.1f, 0.2f, 0.15f);
        smokeEmission = smokeParticles.emission;
        smokeEmission.rateOverTime = 0;
    }

    private void Update()
    {
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
        if (other.gameObject.CompareTag("Cleaner") && printerHealth < maxHealth)
        {
            if (other.gameObject.GetComponent<CleaningObject>().isMoving)
            {
                printerHealth += Time.deltaTime * 5;
                timerBar.fillAmount = printerHealth / maxHealth;
            }
        }
    }

    public void IncreaseDeterioration()
    {
        if (printerHealth > 0 && !instantiateLimit.isInstantiated)
        {
            printerHealth -= 5;
            timerBar.fillAmount = printerHealth / maxHealth;
        }
    }
}
