using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : Singleton<GUIManager>
{
    public GameObject homeGUI;
    public GameObject gameGUI;

    public Dialog EndPanel;
    public Dialog PausePanel;

    public Image FireRateFilled;
    public Text timeText;
    public Text Score;

    private Dialog m_curDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public void showGameGUI(bool codition)
    {
        if (homeGUI)
        {
            homeGUI.SetActive(!codition);
        }
        if (gameGUI)
        {
            gameGUI.SetActive(codition);
        }
    }

    public void UpdateTime(string time)
    {
        if (timeText)
        {
            timeText.text = time;
        }
    }

    public void UpdateScore(int score)
    {
        if (Score)
        {
            Score.text = score.ToString();
        }
    }

    public void UpdateFireRate(float rate)
    {
        if (FireRateFilled)
        {
            FireRateFilled.fillAmount = rate; 
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (PausePanel)
        {
            PausePanel.UpdateDialog("Pause Game", "Best Score: " + Prefabs.get_bestScores());
            PausePanel.show(true);
            m_curDialog = PausePanel;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (m_curDialog)
        {
            m_curDialog.show(false);
        }
    }

    public void Home()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Replay()
    {
        if (m_curDialog)
        {
            m_curDialog.show(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }

}
