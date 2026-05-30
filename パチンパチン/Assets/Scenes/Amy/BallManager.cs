using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    public GameObject[] storage;          // This will store the instantiated ball objects that will be shown on the screen

    public int Count { get; private set; }

    private BallType current;
    private BallType next;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Previous memory has not been cleared.");
            return;
        }

        Instance = this;

        // Set up ball type
        current = BallType.Normal;
        next = BallType.Normal;

        // Set up storage 
        storage = new GameObject[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Please call this function before game starts (Ball manager) - It sets up how many balls are used in this game or level
    // ゲーム開始前にこの関数（ボールマネージャー）を呼び出してください。この関数は、このゲームまたはレベルで使用されるボールの数を設定します。
    // Ensure that it is more than 10!!!
    public void Setup(int givenBall)
    {
        Count = givenBall;


    }
}
