using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string       question = "My question text...";
    [SerializeField] internal List<string> answers;
    [SerializeField] int          correctAnswerIndex;

    public string GetQuestion
    {
        get { return question; }
        private set { question = value; }
    }
    public int GetCorrectAnswerIndex
    {
        get { return correctAnswerIndex; }
        private set { correctAnswerIndex = value; }
    }
    public string GetAnswer(int index) => return answers[index];

    //For custom quiz
    public string SetQuestion
    {
        get { return question; }
        set { question = value; }
    }
    public int SetCorrectAnswerIndex
    {
        get { return correctAnswerIndex; }
        set { correctAnswerIndex = value; }
    }
}
