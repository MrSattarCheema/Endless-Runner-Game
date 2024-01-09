using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnHurdles;
    public GameObject gamePausedPanel;
    public GameObject gameOverPanel;
    public Button pauseBtn;
    public TextMeshProUGUI liveScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public float highestScore;
    float playerScore = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highestScore = PlayerPrefs.GetFloat("highScore");
            Debug.Log("high score" + highestScore);
        }
    }
    public void scoreInc()
    {
        playerScore++;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
        liveScore.text = ((int)playerScore).ToString();
        if (playerScore > highestScore)
        {
            highestScore = playerScore;
            PlayerPrefs.SetFloat("highScore", highestScore);
            PlayerPrefs.Save();
        }
    }
    public void GameOver()
    {
        Destroy(player.gameObject);
        Destroy(spawnHurdles.gameObject);
        gameOverPanel.gameObject.SetActive(true);
        pauseBtn.gameObject.SetActive(false);
        score.text = ((int)playerScore).ToString();
        highScore.text = ((int)highestScore).ToString();
    }
    public void PasueGame()
    {
        gamePausedPanel.SetActive(true);
        player.gameObject.GetComponent<AudioSource>().Pause();
        pauseBtn.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        gamePausedPanel.SetActive(false);
        player.gameObject.GetComponent<AudioSource>().Play();
        pauseBtn.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void coinCollectSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
