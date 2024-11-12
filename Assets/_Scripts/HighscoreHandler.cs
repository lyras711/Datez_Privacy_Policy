using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    private int[] highscores = new int[4];
    [SerializeField] private GameObject[] highscoreUI;
    [SerializeField] private TMPro.TMP_Text[] highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScores();
    }

    public void SetHighScore(string category, int highscore)
    {
        switch (category)
        {
            case "General":
                if(highscore > highscores[0])
                {
                    PlayerPrefs.SetInt("General_Highscore", highscore);
                    SetHighscoreUI(0, highscore);
                }
                break;
            case "Sports":
                if (highscore > highscores[1])
                {
                    PlayerPrefs.SetInt("Sports_Highscore", highscore);
                    SetHighscoreUI(1, highscore);
                }
                break;
            case "Geography":
                if (highscore > highscores[2])
                {
                    PlayerPrefs.SetInt("Geography_Highscore", highscore);
                    SetHighscoreUI(2, highscore);
                }
                break;
            case "History":
                if (highscore > highscores[3])
                {
                    PlayerPrefs.SetInt("History_Highscore", highscore);
                    SetHighscoreUI(3, highscore);
                }
                break;
        }
    }

    void SetHighscoreUI(int cat, int score)
    {
        highscores[cat] = score;
        highscoreUI[cat].SetActive(true);
        highscoreText[cat].text = score.ToString();
    }

    void LoadHighScores()
    {
        if (PlayerPrefs.HasKey("General_Highscore"))
            SetHighscoreUI(0, PlayerPrefs.GetInt("General_Highscore"));
        if (PlayerPrefs.HasKey("Sports_Highscore"))
            SetHighscoreUI(0, PlayerPrefs.GetInt("Sports_Highscore"));
        if (PlayerPrefs.HasKey("Geography_Highscore"))
            SetHighscoreUI(0, PlayerPrefs.GetInt("Geography_Highscore"));
        if (PlayerPrefs.HasKey("History_Highscore"))
            SetHighscoreUI(0, PlayerPrefs.GetInt("History_Highscore"));
    }
}
