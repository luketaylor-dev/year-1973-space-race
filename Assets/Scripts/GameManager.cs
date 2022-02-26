using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player1;
    public GameObject player2;

    public Vector3 player1Start;
    public Vector3 player2Start;

    public TextMeshProUGUI player1txt;
    public TextMeshProUGUI player2txt;

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas pauseMenuCanvas;
    public Slider slider;
    public TextMeshProUGUI player1Win;
    public TextMeshProUGUI player2Win;
    

    public float roundLength = 30f;

    public bool inMenu = true;
    public bool inPauseMenu = false;

    private int player1Score;
    private int player2Score;

    private float gameStart;
    private void Awake()
    {
        instance = this;
        var position = GameManager.instance.player1.transform.position;
        player1Start = new Vector3(position.x, position.y, position.z);
        position = player2.transform.position;
        player2Start = new Vector3(position.x, position.y, position.z);
        slider.value = 0;
        player1Win.enabled = false;
        player2Win.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMenu) return;
        var time = Time.time - gameStart;
        slider.value =  (roundLength - time) / roundLength;
        if (roundLength - time <= 0)
        {
            EndGame();
        }

        if (!inMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !inPauseMenu)
            {
                Pause();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && inPauseMenu)
            {
                Resume();
            }
        }
    }

    private void EndGame()
    {
        if (player1Score > player2Score)
        {
            player1Win.enabled = true;
        }
        else if (player2Score > player1Score)
        {
            player2Win.enabled = true;
        }
        ResetGame();
        gameCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    private void ResetGame()
    {
        GetComponent<Spawner>().DestroyAstroids();
        inMenu = true;
        slider.value = 1;
        player1Score = 0;
        player2Score = 0;
        player1txt.text = player1Score.ToString();
        player2txt.text = player2Score.ToString();
        ResetShip(true);
        ResetShip(false);
    }
    

    public void Score(bool didPlayer1Score)
    {
        if (didPlayer1Score)
        {
            player1Score++;
            player1txt.text = player1Score.ToString();
        }
        else
        {
            player2Score++;
            player2txt.text = player2Score.ToString();
        }
    }

    public void ResetShip(bool isPlayer1)
    {
        if (isPlayer1)
        {
            player1.transform.position = player1Start;
        }
        else
        {
            player2.transform.position = player2Start;
        }
    }

    private void SetupGame(int astroidSpawns)
    {
        gameStart = Time.time;
        GetComponent<Spawner>().SpawnAstroids(astroidSpawns);
        menuCanvas.enabled = false;
        player1Win.enabled = false;
        player2Win.enabled = false;
        gameCanvas.enabled = true;
        inMenu = false;
    }
    
    public void Easy()
    {
        Debug.Log("Easy Click");
        ResetGame();
        SetupGame(25);

    }

    public void Medium()
    {
        Debug.Log("Medium Click");
        ResetGame();
        SetupGame(50);
    }

    public void Hard()
    {       
        Debug.Log("Hard Click");
        ResetGame();
        SetupGame(75);
    }

    public void Pause()
    {
        inPauseMenu = true;
        pauseMenuCanvas.enabled = true;

    }

    public void Resume()
    {
        pauseMenuCanvas.enabled = false;
        inPauseMenu = false;
    }

    public void QuitToMenu()
    {
        ResetGame();
        pauseMenuCanvas.enabled = false;
        gameCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
