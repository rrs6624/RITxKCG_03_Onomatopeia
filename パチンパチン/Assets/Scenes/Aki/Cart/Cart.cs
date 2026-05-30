using UnityEngine;

/// <summary>
/// Cart
/// </summary>
public class Cart : MonoBehaviour
{
    /// <summary>
    /// BoxCollider2D
    /// </summary>
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private BallType cartType;

    /// <summary>
    /// 一致した際のスコア倍率(MatchedScoreMultiplier)
    /// </summary>
    [SerializeField]
    private int matchedScoreMultiplier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    /// <summary>
    /// 当たり判定 (OnTriggerEnter2D)
    /// </summary>
    /// <param name="collision">other</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get Ball component from the collided object
        var ball = collision.GetComponent<Ball>();

        // If there is no Ball component, exit the method
        if (ball == null)
        {
            return;
        }

        // スコア
        int score = 0;

        // 現在の動物種類取得
        var currentAnimalType = ball.AnimalType;

        // カートの動物種類と一致していれば
        if (currentAnimalType == cartType)
        {
            score = ball.GetBallScore() * matchedScoreMultiplier;

            // もしニワトリなら
            if (ball.AnimalType == BallType.Chicken)
            {
                // 残りのボール数を増やす
            }
        }
        else
        {
            score = ball.GetBallScore();
        }

        // スコア加算

        // カートに入った際の処理
        ball.GoaltoCart();
    }
}
