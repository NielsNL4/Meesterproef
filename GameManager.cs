﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;
    private bool paused;
    public int currentScore;
    private int highscore;

    [SerializeField]
    private Text pointText;

    [SerializeField]
    private Text highScoreText;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
        }

        if(paused){
            Time.timeScale = 0;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
        }else{
            Time.timeScale = 1;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
        }

        pointText.text = "Score: " + currentScore.ToString();
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", currentScore);

        if(currentScore > PlayerPrefs.GetInt("HighScore", 0)){
            highscore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }

    public void Continue(){
        paused = false;
    }
}
