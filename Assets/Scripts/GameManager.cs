using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource selectQuizSound;

    Quiz            quiz;
    EndScreen       endScreen;
    CustomizeQuiz   customizeQuiz;
    StartScreen     startScreen;

    public int SelectedQuizIndex { get; private set; } = -1;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        customizeQuiz = FindObjectOfType<CustomizeQuiz>();
        startScreen = FindObjectOfType<StartScreen>();

        startScreen.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
        customizeQuiz.gameObject.SetActive(false);   
    }

    void Update()
    {
        if (quiz.isComplete) Invoke(nameof(ShowEndScreen), 3f);
    }

    void ShowEndScreen()
    {
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true);
        endScreen.ShowFinalScore();
    }

    public void OnSelectedQuiz(int selectedIndex)
    {
        selectQuizSound.Play();

        SelectedQuizIndex = selectedIndex;

        quiz.currentQuiz = new List<QuestionSO>();
        var selectedQuizCategory = quiz.quizCategories[selectedIndex];

        for (int i = 0; i < selectedQuizCategory.Count; i++)
        {
            quiz.currentQuiz.Add(selectedQuizCategory[i]);
        }
        Invoke(nameof(StartSelectedQuiz), 0.8f); 
    }

    void StartSelectedQuiz()
    {
        quiz.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
    }

    public void OnSelectCustomQuiz()
    {
        customizeQuiz.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
    }

    internal void OnStartCustomQuiz()
    {
        quiz.currentQuiz = new List<QuestionSO>();

        for (int i = 0; i < customizeQuiz.CustomQuestions.Count; i++)
        {
            quiz.currentQuiz.Add(customizeQuiz.CustomQuestions[i]);
        }
        quiz.progressBar.maxValue = customizeQuiz.CustomQuestions.Count;
        quiz.progressBar.value = 0;

        customizeQuiz.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
    }

    public void OnRestartCustomQuiz()
    {
        endScreen.gameObject.SetActive(false);
        quiz.currentQuiz = new List<QuestionSO>();

        for (int i = 0; i < customizeQuiz.CustomQuestions.Count; i++)
        {
            quiz.currentQuiz.Add(customizeQuiz.CustomQuestions[i]);
        }

        quiz.gameObject.SetActive(true);
        quiz.progressBar.maxValue = customizeQuiz.CustomQuestions.Count;
        quiz.progressBar.value = 0;
    }

    public void OnReplayLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
