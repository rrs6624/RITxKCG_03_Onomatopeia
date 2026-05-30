using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// スコア
    /// </summary>
    protected int ballScore = 0;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected BallType animalType;

    public BallType AnimalType => animalType;

    [SerializeField]
    protected BallType ballAbilityType;

    public BallType BallAbilityType => ballAbilityType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animalType = BallType.Normal;
    }

    /// <summary>
    /// スコアの加算 (AddBallScore) とスコアの取得 (GetBallScore)
    /// </summary>
    /// <param name="score">加算するスコア(AddScore)</param>
    virtual public void AddBallScore(int score)
    {
        ballScore += score;
    }

    /// <summary>
    /// ボールのスコアの取得 (GetBallScore)
    /// </summary>
    /// <returns>現在のスコア(BallScore)</returns>
    virtual public int GetBallScore()
    {
        return ballScore;
    }

    /// <summary>
    /// ピンに当たったときの処理 (HitAnimalPin)
    /// </summary>
    /// <param name="type">当たった相手の種類(HitPinType)</param>
    /// <param name="addScore">加算するスコア(AddScore)</param>
    virtual public void HitAnimalPin(BallType type, int addScore)
    {
        // 動物の画像をセット( set animal image)
        SetAnimalImage(type);

        // ボールのスコアを加算( add ball score)
        AddBallScore(addScore);
    }

    /// <summary>
    /// カートに当たったときの処理 (HitCart)
    /// </summary>
    virtual public void GoaltoCart()
    {

    }

    protected void SetAnimalImage(BallType type)
    {
        // ボールの種類に応じて画像を変更する処理
        // 例: spriteRenderer.sprite = GetSpriteForBallType(type);
    }
}
