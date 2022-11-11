using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;

    //game timer
    public float timeRemaining;
    public bool timerRunning;
    public string timerDisplay;

    public bool startOfGame; 

    //delivery streak timer
    public float streakTimeRemaining;
    public bool streakTimerRunning; 

    private void Start()
    {
        timeRemaining = 150f; //overcooked2 1-1 timer starts at 2:30
        timerRunning = false;
        startOfGame = true; 

        streakTimerRunning = false; 
    }

    private void FixedUpdate()
    {

        #region Timer
        if (startOfGame == true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //start timer on space (can be changed later, just for testing rn)
            {
                timerRunning = true;
                startOfGame = false;
            }
        }

        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(timeRemaining / 60); //get minutes
            float seconds = Mathf.FloorToInt(timeRemaining % 60); //get seconds

            timerDisplay = string.Format("{0:0}:{1:00}", minutes, seconds); //formats it in 0:00 format
            //this string is grabbed by the ui object 

            if(timeRemaining <= 0) 
            { 
                timerRunning = false; //turn timer off once out of time
            } 
        }

        #endregion

        Debug.Log(timerDisplay); 

    }
}
