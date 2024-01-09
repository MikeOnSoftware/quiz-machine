using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("QUESTIONS")]
    [SerializeField] List<QuestionSO> questionsRandom;
    [SerializeField] List<QuestionSO> questionsNature;
    [SerializeField] List<QuestionSO> questionsSports;
    [SerializeField] List<QuestionSO> questionsCars;
    [SerializeField] internal List<QuestionSO> currentQuiz;

    [SerializeField] TextMeshProUGUI  questionText;
    QuestionSO currentQuestion;
    [SerializeField] internal List<List<QuestionSO>> quizCategories = new();

    [Header("ANSWERS")]
    [SerializeField] List<GameObject> answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("BUTTON COLORS")]
    [SerializeField] Sprite          defaultAnswerSprite;
    [SerializeField] Sprite          correctAnswerSprite;
    [SerializeField] Sprite          wrongAnswerSprite;

    [Header("TIMER")]
    [SerializeField] Image           timerImage;
    Timer myTimerScript;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] internal Slider progressBar;
    internal bool isComplete;
    int mSelectedQuizIndex;
    GameManager gameManager;


    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        myTimerScript = FindObjectOfType<Timer>();
        gameManager = FindObjectOfType<GameManager>();

        mSelectedQuizIndex = gameManager.SelectedQuizIndex;

        if (mSelectedQuizIndex != -1)
        {
            progressBar.maxValue = currentQuiz.Count;
            progressBar.value = 0;
        }

        quizCategories.Add(questionsRandom);
        quizCategories.Add(questionsNature);
        quizCategories.Add(questionsSports);
        quizCategories.Add(questionsCars);
    }

    void Update()
    {
        timerImage.fillAmount = myTimerScript.FillFraction;

        if (myTimerScript.LoadNexQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                Debug.Log("Complete");
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            myTimerScript.LoadNexQuestion = false;
        }
        else if (!hasAnsweredEarly && !myTimerScript.IsAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetAnswerButtonsState(false);
        }
    }
    
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion;

        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerButtons[i].SetActive(false);
        }
        for (int i = 0; i < currentQuestion.answers.Count; i++)
        {
            answerButtons[i].SetActive(true);
            TextMeshProUGUI answerButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerButtonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        SetAnswerButtonsState(false);
        myTimerScript.SetTimer(0);
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayAnswer(int index)
    {
         Image buttonImage;
         if (index == currentQuestion.GetCorrectAnswerIndex)
         {
             questionText.text = "Correct!";
             buttonImage = answerButtons[index].GetComponent<Image>();
             buttonImage.sprite = correctAnswerSprite;
            // SetButtonNextQuestionState(true);
             scoreKeeper.IncreaseCorrectAnswers();
         }
         else
         {
             correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex;
             string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
             questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
             buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
             buttonImage.sprite = correctAnswerSprite;

             if (hasAnsweredEarly) //check if there is pressed answer button to avoid exception
             {
                answerButtons[index].GetComponent<Image>().sprite = wrongAnswerSprite;
             }
        }
    }

    void SetButtonNextQuestionState(bool state)
    {
        var buttonNextQuestion = GameObject.Find("NextQuestion").GetComponent<Button>();
        buttonNextQuestion.interactable = state;
    }

    void SetAnswerButtonsState(bool state)
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }

    public void GetNextQuestion()
    {
        if (currentQuiz.Count > 0)
        {
            GetRandomQuestion();
            SetAnswerButtonsState(true);
            SetDefaultButtonsSprites();
            DisplayQuestion();
          //  SetButtonNextQuestionState(false);
            scoreKeeper.IncreaseQuestionsSeen();
            progressBar.value++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, currentQuiz.Count);
        currentQuestion = currentQuiz[index];
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex;

        if (currentQuiz.Contains(currentQuestion))
        {
            currentQuiz.Remove(currentQuestion);
        }
    }

    void SetDefaultButtonsSprites()
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

}

