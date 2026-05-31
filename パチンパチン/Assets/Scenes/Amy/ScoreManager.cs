using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int Score { get; private set; }

    public System.Action onScoreChangedFunc;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Previous memory has not been cleared.");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize score
        Score = 0;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // --------------------------------------------------------------
    // Change score: These functions should be called inside the balls when they hit a pin
    // 変更スコア：    これらの関数は、ボールがピンに当たったときにボール内部で呼び出される必要があります。
    // --------------------------------------------------------------

    // Increases the score count of that amount
    // その金額分のスコアカウントを増加させる
    public void IncreaseScore(int increment)
    {
        Score += increment;

        // Call the function to update the score display
        onScoreChangedFunc?.Invoke();
    }

    // Decreases the score count of that amount
    // その金額のスコアカウントを減らします
    public void DecreaseScore(int decrement)
    {
        Score -= decrement;

        // Call the function to update the score display
        onScoreChangedFunc?.Invoke();
    }
}
