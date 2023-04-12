using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject endUI;
    public TMP_Text scoreText;

    int score = 0;
    bool gameOver = false;
    
    void Start()
    {
        StartGame();
    }

    
    void Update()
    {
        // If we haven't set the game over
        if (!gameOver)
        {
            // Update the score text
            UpdateScoreText();

            // If music is still playing, game is still active
            if (musicSource.isPlaying)
            {
                // If the score drops below 0, end the game
                if (score < 0)
                {
                    GameOver();
                }
            }
            // If the music stopped playing, game should end
            else
            {
                GameOver();
            }
        }
    }

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void StartGame()
    {
        score = 0;
        UpdateScore(score);

        endUI.SetActive(false);

        gameOver = false;

        musicSource.Play();
    }

    public void GameOver()
    {
        // Set the flag
        gameOver = true;

        // Display the end UI
        endUI.SetActive(true);

        // Stop the music if playing
        musicSource.Stop();

        
    }
}
