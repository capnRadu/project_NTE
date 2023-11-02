using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningObject : MonoBehaviour
{
    public bool isMoving = false;
    private bool isChecking = false;

    private void Update()
    {
        if (!isChecking)
        {
            isChecking = true;
            StartCoroutine(CheckMoving());
        }
    }

    private IEnumerator CheckMoving()
    {
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 finalPos = transform.position;

        if (startPos.x - finalPos.x > 0.05f || startPos.x - finalPos.x < -0.05f || startPos.y - finalPos.y > 0.05f || startPos.y - finalPos.y < -0.05f || startPos.z - finalPos.z > 0.05f || startPos.z - finalPos.z < -0.05f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        isChecking = false;
    }
}
