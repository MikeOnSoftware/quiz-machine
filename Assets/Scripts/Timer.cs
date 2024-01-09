using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float mTimeToComplateQuestion   = 5f;
    [SerializeField] float mTimeToShowCorrectAnswer  = 3f;
    float mTimerValue;
    float mFillFraction;
    bool mIsAnsweringQuestion = false;
    bool mLoadNexQuestion = false;

    public float FillFraction
    {
        get { return mFillFraction; }
    }
    public bool IsAnsweringQuestion
    {
        get { return mIsAnsweringQuestion; }
    }
    public bool LoadNexQuestion
    {
        get { return mLoadNexQuestion; }
        internal set { mLoadNexQuestion = value; }
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        mTimerValue -= Time.deltaTime;

        if (mIsAnsweringQuestion)
        {
            if (mTimerValue > 0)
            {
                mFillFraction = mTimerValue / mTimeToComplateQuestion;
            }
            else
            {
                mIsAnsweringQuestion = false;
                mTimerValue = mTimeToShowCorrectAnswer;
            }
        }
        else
        {
            if (mTimerValue > 0)
            {
                mFillFraction = mTimerValue / mTimeToShowCorrectAnswer;
            }
            else
            {
                mIsAnsweringQuestion = true;
                mTimerValue = mTimeToComplateQuestion;
                mLoadNexQuestion = true;
            }
        }
    }

    internal void SetTimer(int value)
    {
        mTimerValue = value;
    }
}
