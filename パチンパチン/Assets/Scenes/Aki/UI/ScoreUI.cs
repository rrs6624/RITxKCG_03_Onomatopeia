using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private ScoreManager scoreManager;

    private TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = ScoreManager.Instance;

        scoreText = GetComponent<TextMeshProUGUI>();

        scoreText.text = scoreManager.Score.ToString();
    }

    /// <summary>
    /// スコア変更時に呼び出すメソッド (Method to call when score changes)
    /// </summary>
    public void ScoreChanged()
    {
        scoreText.text = scoreManager.Score.ToString();
    }

}
