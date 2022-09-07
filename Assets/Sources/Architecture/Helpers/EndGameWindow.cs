using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DescriptionText;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI GameResultText;
    [SerializeField] private Button RestartGameButton;
    [SerializeField] private Button BackToMenuButton;
    private static readonly string MainMenuSceneName = "Menu";

    private void OnEnable()
    {
        RestartGameButton.onClick.AddListener(RestartLevel);
        BackToMenuButton.onClick.AddListener(GoToMenu);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void Show(string description, int score, bool isWinner)
    {
        if (isWinner)
        {
            RestartGameButton.gameObject.SetActive(false);
            GameResultText.text = "You won";
        }
        DescriptionText.text = description;
        ScoreText.text = $"Your snake length was {score}";
    }
    
    private void OnDisable()
    {
        RestartGameButton.onClick.RemoveListener(RestartLevel);
        BackToMenuButton.onClick.RemoveListener(GoToMenu);
    }
}
