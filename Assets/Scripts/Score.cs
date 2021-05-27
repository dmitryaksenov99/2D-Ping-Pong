using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text txtScore;
    [SerializeField] Text txtMaxScore;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;
    [SerializeField] Dropdown ballColor;
    const string BALL_COLOR_KEY = "ball_color";

    public void PlayGame()
    {

        game.SetActive(true);
        menu.SetActive(false);

    }
    public void ShowMenu()
    {
        game.SetActive(false);
        menu.SetActive(true);
    }
    void OnEnable()
    {
        game.SetActive(false);
        DrawScore();
        DrawMaxScore();
        SetCurrentBallColor();
    }

    public void ChangeBallColor()
    {
        PlayerPrefs.SetInt(BALL_COLOR_KEY, ballColor.value);
    }
    public void SetCurrentBallColor()
    {
        ballColor.value = PlayerPrefs.GetInt(BALL_COLOR_KEY, 0);
    }
    public Color GetBallColor()
    {
        int colorValue = PlayerPrefs.GetInt(BALL_COLOR_KEY, 0);
        string _color = ballColor.options[colorValue].text;
        ColorUtility.TryParseHtmlString(_color, out Color color);
        return color;
    }
    public void UpdateScore(Player player)
    {
        int score = GetScore(player);
        score++;
        SetScore(player, score);
        DrawScore();
    }
    public void SetScore(Player player, int score)
    {
        PlayerPrefs.SetInt(player.ToString(), score);
    }
    public int GetScore(Player player)
    {
        int score = PlayerPrefs.GetInt(player.ToString(), 0);
        return score;
    }
    public void DrawScore()
    {
        int score1 = GetScore(Player.Player1);
        int score2 = GetScore(Player.Player2);

        txtScore.text = string.Format("{0}\n{1}\n{2}", score1, "☰", score2);
    }

    public void ResetScore()
    {
        SetScore(Player.Player1, 0);
        SetScore(Player.Player2, 0);
    }

    public void SetMaxScore()
    {
        int newScore1 = GetScore(Player.Player1);
        int newScore2 = GetScore(Player.Player2);

        int currentScore1 = GetMaxScore(Player.Player1);
        int currentScore2 = GetMaxScore(Player.Player2);

        if (newScore1 > currentScore1)
        {
            SetMaxScore(Player.Player1, newScore1);
         
        }
        if (newScore2 > currentScore2)
        {
            SetMaxScore(Player.Player2, newScore2);
        }
    }
    public void DrawMaxScore()
    {
        int score1 = GetMaxScore(Player.Player1);
        int score2 = GetMaxScore(Player.Player2);

        txtMaxScore.text = string.Format("{0}\n{1}\n{2}", score1, "MAX SCORE", score2);
    }
    public int GetMaxScore(Player player)
    {
        int score = PlayerPrefs.GetInt(player.ToString() + "_max_score", 0);
        return score;
    }
    public void SetMaxScore(Player player, int score)
    {
        PlayerPrefs.SetInt(player.ToString() + "_max_score", score);
    }
}
