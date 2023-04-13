using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float _timeLeft = 60;
    private bool _timerOn;
    [SerializeField] private Text timerText;
    [SerializeField] private GameManager _gameManager;
    
    public void StartTimer(int timeLeft)
    {
        _timeLeft = timeLeft;
        _timerOn = true;
    }
    
    void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimer(_timeLeft);
            }
            else
            {
                Debug.Log("TIME'S UP!"); // for testing! 
                _gameManager.EndOfLevel();
                _timeLeft = 0;
                _timerOn = false;
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
