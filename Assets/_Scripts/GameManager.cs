using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static QuestionDataLoader;
using static CustomPackManager;

public class GameManager : MonoBehaviour
{
    QuestionDataLoader questionData;
    List<Question> questions = new List<Question>();

    public int marginForError = 5;

    [Header("MainScreen")]
    public TMP_Text question;
    public TMP_Text displayAnswer;
    public TMP_Text livesText;
    public TMP_Text scoreText;
    public Slider slider;
    int submitedAnswer;
    int livesToSubstract = 0;
    int lives = 100;
    int score = 0;
    int scoreToAdd = 0;

    [Header("Answer")]
    public TMP_Text int1Text;
    public TMP_Text int2Text;
    public TMP_Text int3Text;
    public TMP_Text int4Text;
    private int int1 = 1;
    private int int2 = 9;
    private int int3 = 9;
    private int int4 = 9;

    [Header("SubmitScreen")]
    public GameObject submitScreen;
    public GameObject menuScreen;
    public GameObject gameScreen;
    public TMP_Text guessText;
    public TMP_Text correctText;
    public TMP_Text moeText;
    public TMP_Text s_livesText;
    public TMP_Text s_scoreText;

    Question selectedQuestion;

    bool gameOver = false;
    string chosenCategory;

    // Start is called before the first frame update
    void Start()
    {
        questionData = GetComponent<QuestionDataLoader>();
    }

    void SetQuestion()
    {
        selectedQuestion = questions[Random.Range(0, questions.Count)];

        question.text = selectedQuestion.Fact;
    }

    bool CheckAnswer()
    {
        submitedAnswer = (int1 * 1000) + (int2 * 100) + (int3 * 10) + (int4);

        livesToSubstract = Mathf.Abs(submitedAnswer - selectedQuestion.Year);
        if (livesToSubstract == 0 || livesToSubstract <= marginForError)
            return true;
        else
            return false;
    }

    public void Submit()
    {
        if(CheckAnswer())
        {
            if (livesToSubstract == 0)
                scoreToAdd = 3;
            else
                scoreToAdd = 1;

            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }
        else
        {
            scoreToAdd = 0;
            if (lives > livesToSubstract)
            {
                lives -= livesToSubstract;
            }
            else
            {
                lives = 0;
                gameOver = true;
            }
            livesText.text = "Lives: " + lives;
        }

        submitScreen.SetActive(true);

        guessText.text = submitedAnswer.ToString();
        correctText.text = selectedQuestion.Year.ToString();

        moeText.text = "Margin of error: " + marginForError + " years";
        s_livesText.text = "Lives remaining: " + lives + " (~ " + livesToSubstract + ")";
        s_scoreText.text = "Score: " + score + " (+ " + scoreToAdd + ")";
    }

    public void Continue()
    {
        SetQuestion();
        submitScreen.SetActive(false);
        if(gameOver)
        {
            GetComponent<HighscoreHandler>().SetHighScore(chosenCategory, score);
            lives = 100;
            score = 0;
            livesText.text = "Lives: " + lives;
            scoreText.text = "Score: " + score;
            gameOver = false;
        }
    }

    public void BackToMenu()
    {
        gameScreen.SetActive(false);
        menuScreen.SetActive(true);
        
        lives = 100;
        score = 0;
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        gameOver = false;
    }

    public void SelectCategory(string cat)
    {
        chosenCategory = cat;
        questions = questionData.GetQuestionsByCategory(cat);
        StartGame();
    }

    public void SelectCustomPack(CustomQuestionPack customPack)
    {
        questions.Clear();

        foreach (CustomQuestion question in customPack.Questions)
        {
            Debug.Log(question.Fact);
            Question questionToAdd = new Question("Custom", question.Fact, question.Year);
            questions.Add(questionToAdd);
        }

        StartGame();
    }

    public void UpdateCounterNumber1(bool up)
    {
        if(up)
        {
            int1++;
            if (int1 > 9)
                int1 = 0;
        }
        else
        {
            int1--;
            if (int1 < 0)
                int1 = 9;
        }
        int1Text.text = int1.ToString();
    }

    public void UpdateCounterNumber2(bool up)
    {
        if (up)
        {
            int2++;
            if (int2 > 9)
                int2 = 0;
        }
        else
        {
            int2--;
            if (int2 < 0)
                int2 = 9;
        }
        int2Text.text = int2.ToString();
    }

    public void UpdateCounterNumber3(bool up)
    {
        if (up)
        {
            int3++;
            if (int3 > 9)
                int3 = 0;
        }
        else
        {
            int3--;
            if (int3 < 0)
                int3 = 9;
        }
        int3Text.text = int3.ToString();
    }

    public void UpdateCounterNumber4(bool up)
    {
        if (up)
        {
            int4++;
            if (int4 > 9)
                int4 = 0;
        }
        else
        {
            int4--;
            if (int4 < 0)
                int4 = 9;
        }
        int4Text.text = int4.ToString();
    }

    void StartGame()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(true);
        SetQuestion();
    }
}
