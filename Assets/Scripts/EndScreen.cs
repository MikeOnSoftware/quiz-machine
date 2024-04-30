using UnityEngine;
using TMPro;


public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;

    const string WinMessage = "Congratulations!\nYou got a score of";
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore() => finalScoreText.text = $"{WinMessage} {scoreKeeper.CalculateScore()}%";
}
