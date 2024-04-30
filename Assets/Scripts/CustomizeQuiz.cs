using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CustomizeQuiz : MonoBehaviour
{
    [Header("QUESTIONS")]
    [SerializeField] List<QuestionSO>       customQuestions;
    [SerializeField] Button                 addQuestion;
    [SerializeField] TextMeshProUGUI        questionTextInput;
    [SerializeField] TextMeshProUGUI        questionsCount;
    [Header("ANSWERS")]
    [SerializeField] List<GameObject>       answerButtons;
    [SerializeField] TMP_InputField         answersCountIntInput;
    int                                     mAnswersCount;
    [SerializeField] TMP_InputField         correctAnswerInput;
    [Header("ERROR")]
    [SerializeField] TextMeshProUGUI        errorMessage;

    public List<QuestionSO> CustomQuestions => customQuestions;

    const string InitialAnswersCount = "6";
    const string QuestionTextInitialMessage = "Type new question...";
    const string AnswerTextInitialMessage = "Type an answer...";

    QuestionSO currentQuestion;

    //All fields with possible user input are validated in Unity
    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            errorMessage.text = "If you don't see a keyboard, connect an external one or download the app for your device!";
        }
        answersCountIntInput.text = InitialAnswersCount;
        questionsCount.text = customQuestions.Count.ToString();
    }

    void Update()
    {
        if (answersCountIntInput.text != "")
        {
            if (int.Parse(answersCountIntInput.text) != mAnswersCount)
            {
                mAnswersCount = int.Parse(answersCountIntInput.text);

                for (int i = 0; i < answerButtons.Count; i++)
                {
                    answerButtons[i].SetActive(false);
                }
                for (int i = 0; i < mAnswersCount; i++)
                {
                    answerButtons[i].SetActive(true);
                }
            }
        }       
    }

    public void AddNewQuestion()
    {
        currentQuestion = ScriptableObject.CreateInstance<QuestionSO>();
        currentQuestion.SetQuestion = questionTextInput.text;
        currentQuestion.answers = new List<string>();
        currentQuestion.SetCorrectAnswerIndex = int.Parse(correctAnswerInput.text) - 1;

        for (int i = 0; i < mAnswersCount; i++)
        {
            TextMeshProUGUI answerButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            currentQuestion.answers.Add(answerButtonText.text);
        }
        customQuestions.Add(currentQuestion);
        questionsCount.text = customQuestions.Count.ToString();

        CleanFields();
    }

    void CleanFields()
    {
        answersCountIntInput.text = InitialAnswersCount;
        GameObject.Find("QuestionText").GetComponent<TMP_InputField>().text = QuestionTextInitialMessage;
        for (int i = 0; i < mAnswersCount; i++)
        {
            answerButtons[i].GetComponent<TMP_InputField>().text = AnswerTextInitialMessage;
        }
    }
    public void OpenTouchKeyboard() => TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
}
