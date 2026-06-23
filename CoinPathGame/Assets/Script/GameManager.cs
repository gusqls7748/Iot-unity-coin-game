using UnityEngine;
using TMPro; // TMP를 사용하기 위해 필수!

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;

    // 유니티 인스펙터에서 Canvas 아래의 Text(TMP)를 끌어다 놓으세요.
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        UpdateScoreUI(); // 시작하자마자 0점으로 표시
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI(); // 점수가 오를 때마다 텍스트 업데이트
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}