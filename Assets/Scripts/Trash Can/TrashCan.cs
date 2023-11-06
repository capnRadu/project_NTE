using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private AudioSource destroySound;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        destroySound.Play();
    }
}
