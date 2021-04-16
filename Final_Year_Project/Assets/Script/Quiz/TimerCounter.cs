﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{
    private bool stopTimer = true;
    private float timeCount = 0;
    [Header("Bar")]
    public Image barColorArea;
    [Header("Minutes Timer UI")]
    public Text minutesTimer;
    [Header("Seconds Timer UI")]
    public Text secondsTimer;
    [Header("Timer control")]
    public float time;
    private float maxBarValue;
    [Header("Pause UI")]
    public GameObject pausePanel;
    public Button continueBtn;
    public Button pauseBtn;
    public Button leaveBtn;


    void Start()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            StopTimer();
            pausePanel.SetActive(true);
        });
        continueBtn.onClick.AddListener(() =>
        {
            stopTimer = false;
            pausePanel.SetActive(false);
        });
        leaveBtn.onClick.AddListener(() =>
        {
            GeneralScript.RedirectPageWithT("Main", "Redirecting to main page...", "Canvas");
        });
        maxBarValue = time;
    }

    void Update()
    {
        if (!stopTimer)
        {
            time -= Time.deltaTime;
            timeCount += Time.deltaTime;
            int timeToSeconds = System.Convert.ToInt32(time);
            int timeCountToMinutes = Mathf.FloorToInt(timeCount / 60);
            int timeCountToSeconds = Mathf.FloorToInt(timeCount - timeCountToMinutes * 60f);
            minutesTimer.text = string.Format("{0}:{1}", timeCountToMinutes.ToString("00"), timeCountToSeconds.ToString("00"));
            secondsTimer.text = timeToSeconds.ToString();
            barColorArea.fillAmount = time / maxBarValue;
            if (barColorArea.fillAmount >= 0.7 && barColorArea.fillAmount <= 1.0)
            {
                barColorArea.color = new Color32(59, 219, 109, 255);
            }
            else if (barColorArea.fillAmount >= 0.3 && barColorArea.fillAmount <= 0.7)
            {
                barColorArea.color = new Color32(225, 209, 89, 255);
            }
            else if (barColorArea.fillAmount <= 0.3)
            {
                barColorArea.color = new Color32(236, 102, 107, 255);
                if (barColorArea.fillAmount <= 0)
                {
                    ResetTimer();
                }
            }
        }
    }

    public void ResetTimer()
    {
        time = 30f;
    }

    public void StartTimer()
    {
        stopTimer = false;
    }

    public void StopTimer()
    {
        stopTimer = true;

    }
}