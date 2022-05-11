using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    public Text scoreText;
    public Text bestScoreText;

    [Header("Gameobjects")]
    public GameObject bestScore;
    public GameObject myButton;

    [Header("Sounds")]
    public AudioSource bounceSound;
    public AudioSource scoreIncreaseSound;
    public AudioSource failSound;

    void Start()
    {
        PlayerPrefs.SetInt("Skorum", BallControl.bestScore);
        scoreText.text = "Score " + BallControl.score;
    }


    public void ScoreWrite()
    {
        scoreText.text = "Score " + BallControl.score;
    }
    public void BestScore()
    {
        scoreText.text = "Your Score " + BallControl.score;
        bestScoreText.text = "Best Score " + BallControl.bestScore;
        PlayerPrefs.SetInt("Skorum", BallControl.bestScore);
        bestScore.SetActive(true);
        myButton.SetActive(true);

    }
    public void Restart()
    {
        myButton.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    } 
}
