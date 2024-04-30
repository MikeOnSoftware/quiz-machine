using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplateQuestion   = 5f;
    [SerializeField] float timeToShowCorrectAnswer  = 3f;
    float timerValue;
    float fillFraction;
    bool isAnsweringQuestion = false;

    public float FillFraction => fillFraction;
    public bool IsAnsweringQuestion => isAnsweringQuestion;
    public bool LoadNexQuestion { get; internal set; } = false;

    void Update() => UpdateTimer();

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToComplateQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToComplateQuestion;
                LoadNexQuestion = true;
            }
        }
    }

    internal void SetTimer(int value) => timerValue = value;
}
