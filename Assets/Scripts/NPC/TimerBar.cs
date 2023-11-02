using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;

public class TimerBar : MonoBehaviour
{
    public Image timerBar;

    public float maxTime = 50f;
    private float timeLeft;

    private void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        } else
        {
            //
        }
    }
}
