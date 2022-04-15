using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMng : MonoBehaviour
{
    public Text[] ScoreText;
    public Text HighScoreText;
    public GameObject GameOverPanel;
    public GameObject StartBtn;
    public GameObject Ready;
    public UnityAction Start;
    public UnityAction OnGameStart;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        GameOverPanel.SetActive(false);
        Ready.SetActive(false);
        StartBtn.SetActive(true);
        Start += GameStart;
        OnGameStart += GameStartClick;
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        StartBtn.SetActive(true);
    }

    public void SetScore(int score)
    {
        for (int i = 0; i < ScoreText.Length; ++i)
            ScoreText[i].text = ScoreText[i].gameObject.activeSelf ? score.ToString() : null;
    }

    public void SetHighScore(int score)
    {
        HighScoreText.text = score.ToString();
    }

    public void GameStartClick()
    {
        Ready.SetActive(false);
    }

    public void GameStart()
    {
        StartBtn.SetActive(false);
        GameOverPanel.SetActive(false);
        Ready.SetActive(true);
        SceneManager.LoadScene("Game");
    }

    public void Go()
    {
        OnGameStart?.Invoke();
    }

    public void StartBtnClick()
    {
        Start?.Invoke();
    }
}
