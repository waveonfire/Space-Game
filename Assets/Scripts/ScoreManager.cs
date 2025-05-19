using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int score = 0;

    void Start ()
    {
        UpdateScoreText();
    }

    public void AddScore (int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText ()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}