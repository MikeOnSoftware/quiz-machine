using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers  = 0;
    int questionsSeen   = 0;

    public int GetCorrectAnswers() => correctAnswers;

    public void IncreaseCorrectAnswers() => correctAnswers++;

    public int GetQuestionsSeen() => questionsSeen;

    public void IncreaseQuestionsSeen() => questionsSeen++;

    public int CalculateScore() => Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
}
