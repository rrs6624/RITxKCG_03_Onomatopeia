using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// スコア
    /// </summary>
    protected int ballScore;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    protected BallType animalType;

    /// <summary>
    /// 現在の動物の種類 (CurrentAnimalType)
    /// </summary>
    public BallType AnimalType => animalType;

    [SerializeField]
    protected BallType ballAbilityType;

    /// <summary>
    /// ??ルの?力の種類 (BallAbilityType)
    /// </summary>
    public BallType BallAbilityType => ballAbilityType;

    /// <summary>
    /// スコア?示用のTextMeshProコン??ネント (TextMeshPro component for score display)
    /// </summary>
    [SerializeField]
    private TextMeshPro tmp;

    private float startFontSize = 8.0f;

    private float addFontSize = 1.0f;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private BallManager ballManager;

    [SerializeField]
    private AudioClip spawnSE;

    [SerializeField]
    private AudioClip goalSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animalType = BallType.Normal;
        ballManager = BallManager.Instance;

        SetAnimalImage();

        // スコアの初期化 (Initialize score)
        ballScore = 0;

        // スコア?示の更新( update score display)
        tmp.text = ballScore.ToString();
        tmp.fontSize = startFontSize;

        // ス??ンSEの再生 (Play spawn sound effect)
        audioSource.PlayOneShot(spawnSE);
    }

    /// <summary>
    /// スコアの加算 (AddBallScore) とスコアの取得 (GetBallScore)
    /// </summary>
    /// <param name="score">加算するスコア(AddScore)</param>
    virtual public void AddBallScore(int score)
    {
        ballScore += score;

        // スコア?示の更新( update score display)
        tmp.text = ballScore.ToString();
        tmp.fontSize += addFontSize;
    }

    /// <summary>
    /// ??ルのスコアの取得 (GetBallScore)
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
        animalType = type;
        BallManager.Instance.UpdateBallType(type);

        // 動物の画像をセット( set animal image)
        SetAnimalImage();

        // ??ルのスコアを加算( add ball score)
        AddBallScore(addScore);
    }

    /// <summary>
    /// カ?トに当たったときの処理 (HitCart)
    /// </summary>
    virtual public void GoaltoCart()
    {
        // ゴ?ルSEの再生 (Play goal sound effect)
        audioSource.PlayOneShot(goalSE);

        BallManager.Instance.DestroyBall(this);
    }

    protected void SetAnimalImage()
    {
        // ??ルの画像をセット( set ball image)
        spriteRenderer.sprite = BallManager.Instance.GetSpriteForBallType(animalType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("Collision");
            BallManager.Instance.Collieded();
        }
    }
}
