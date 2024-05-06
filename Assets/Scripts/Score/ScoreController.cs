
using System;
using UnityEngine;

//
// Turmoil 1982 v2021.02.13
//
// 2021.02.04
//

public class ScoreController : MonoBehaviour
{
    public static ScoreController scoreController;

    public SpriteRenderer[] player1Score;

    public SpriteRenderer[] highScore;

    public Sprite[] numberDigits;

    public const int PLAYER_1 = 1;
    public const int HIGH_SCORE = 3;


    private void Awake()
    {
        scoreController = this;
    }


    public void InitialiseScores()
    {
        for (int scoreDigit = 0; scoreDigit < 5; scoreDigit++)
        {
            player1Score[scoreDigit].sprite = numberDigits[0];

            if (!GameController.gameController.highScoreSet)
            {
                highScore[scoreDigit].sprite = numberDigits[0];
            }
        }
    }


    public void UpdateScoreDisplay(int score, int display)
    {
        string scoreText = score.ToString();

        for (int scoreDigit = 0; scoreDigit < scoreText.Length; scoreDigit++)
        {
            string digitText = scoreText.Substring(scoreDigit, 1);

            int digit = Convert.ToInt32(digitText);

            switch (display)
            {
                case PLAYER_1:

                    UpdatePlayer1(scoreText, scoreDigit, digit);

                    break;

                case HIGH_SCORE:

                    UpdateHighScore(scoreText, scoreDigit, digit);

                    break;
            }
        }
    }


    private void UpdatePlayer1(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 00000
            case 5:

                player1Score[scoreDigit].sprite = numberDigits[digit];

                break;

            // 0000
            case 4:

                player1Score[scoreDigit + 1].sprite = numberDigits[digit];

                break;

            // 000
            case 3:

                player1Score[scoreDigit + 2].sprite = numberDigits[digit];

                break;

            // 00
            case 2:

                player1Score[scoreDigit + 3].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                player1Score[scoreDigit + 4].sprite = numberDigits[digit];

                break;
        }

    }


    private void UpdateHighScore(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 00000
            case 5:

                highScore[scoreDigit].sprite = numberDigits[digit];

                break;

            // 0000
            case 4:

                highScore[scoreDigit + 1].sprite = numberDigits[digit];

                break;

            // 000
            case 3:

                highScore[scoreDigit + 2].sprite = numberDigits[digit];

                break;

            // 00
            case 2:

                highScore[scoreDigit + 3].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                highScore[scoreDigit + 4].sprite = numberDigits[digit];

                break;
        }
    }


} // end of class
