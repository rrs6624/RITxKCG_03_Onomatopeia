using System;
using System.Collections.Generic;
using UnityEngine;

public class BallObjectArgs : EventArgs
{
    public Ball BallObject { get; set; }
}

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    public GameObject normalBallPrefab;
    public GameObject chickenBallPrefab;
    public GameObject duckBallPrefab;
    public GameObject cowBallPrefab;
    public GameObject horseBallPrefab;
    public GameObject sheepBallPrefab;

    // For smooth Ball movement
    public Rigidbody2D ballRB;
    private List<Rigidbody2D> movingBalls;
    private List<Vector2> targetPosition;

    public Ball currentBall;
    public List<Ball> storage;          // Preview Queue
    public List<Ball> activeBalls;      // All balls that are on the board

    public event EventHandler<BallObjectArgs> OnDestroyBall;

    // Holding other ball sprites
    public Sprite normalBall;
    public Sprite chickenBall;
    public Sprite duckBall;
    public Sprite cowBall;
    public Sprite horseBall;
    public Sprite sheepBall;

    public int Count { get; private set; }

    private const int MAXCOUNT = 10;    // Maximum amount of balls that the player can hold

    private BallType currentType;
    private BallType nextType;

    public Camera Cam;

    public float speed;                      // Current movement speed for ball

    
    public System.Action onCountUIFunc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Previous memory has not been cleared.");
            return;
        }

        Instance = this;

        Cam = Camera.main;

        // Set up ball type
        currentType = BallType.Normal;
        nextType = BallType.Normal;

        // Set up storage 
        storage = new List<Ball>();
        activeBalls = new List<Ball>();
        movingBalls = new List<Rigidbody2D>();
        targetPosition = new List<Vector2>();

        // Call Set up function (FOR NOW - This could be called elsewhere)
        Setup(10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Unity Physics based stuff
    private void FixedUpdate()
    {
        for (int i = 0; i < movingBalls.Count; i++)
        {
            Rigidbody2D rigidbody = movingBalls[i];

            rigidbody.MovePosition(Vector2.MoveTowards(rigidbody.position, targetPosition[i], speed * Time.fixedDeltaTime));

            // If close to target position remove from list
            if (Vector2.Distance(rigidbody.position, targetPosition[i]) < 0.01f)
            {
                movingBalls.RemoveAt(i);
                targetPosition.RemoveAt(i);
                i--;
            }
        }
    }

    // If it is not smooth delta time should be called

    // Please call this function before game starts (Ball manager) - It sets up how many balls are used in this game or level
    // ゲーム開始前にこの関数（ボールマネージャー）を呼び出してください。この関数は、このゲームまたはレベルで使用されるボールの数を設定します。
    // Ensure that it is more than 10!!! | 必ず10以上であることを確認してください!!!
    public void Setup(int givenBall)
    {
        Count = givenBall;              // Set ball amount

        // Current ball on its position
        currentBall = CreateBall(
            currentType,
            CoordinateConversion(0.84f, 0.65f)
        );

        Count--;

        // Ball in storage
        for (int i = 0; i < 3; i++)
        {
            float ndcX = 0.84f + (0.04f * i);

            storage.Add(
                CreateBall(
                    BallType.Normal,
                    CoordinateConversion(ndcX, 0.78f)
                )
            );

            Count--;
        }

        speed = 1f;     // Set ball speed 
    }


    // Please when ball is launched (When player releases the click)
    public void LaunchCurrentBall()
    {
        activeBalls.Add(currentBall);

        Reload();
    }


    // Please call this function after a ball hits a cart 
    public void Reload()
    {
        // Free ballRB
        ballRB = null;

        // Deploy the ball from storage and free storage
        currentBall = storage[0];
        storage.RemoveAt(0);
        ballRB = currentBall.GetComponent<Rigidbody2D>();

        // Update types of balls
        currentType = nextType;
        nextType = BallType.Normal;

        // Set Target for ball to move
        SetTarget(ballRB,
            CoordinateConversion(0.84f, 0.65f));

        // Move storage stuff
        for (int i = 0; i < storage.Count; i++)
        {
            SetTarget(
                storage[i].GetComponent<Rigidbody2D>(),
                CoordinateConversion(0.84f + (0.04f * i), 0.78f)
            );
        }

        // Add a new ball to storage
        storage.Add(
            CreateBall(
                nextType,
                CoordinateConversion(0.92f, 0.78f)
            )
        );

        Count--;
    }


    // Please use this function when you need to increase the count of the total number of given balls
    // 与えられたボールの総数を増やす必要がある場合は、この関数を使用してください。
    public void IncreaseBallCount(int count)
    {
        if(Count < MAXCOUNT)
        {
            Count += count;
        }
        else
        {
            Count = MAXCOUNT;
        }

        // Call UI update function
        onCountUIFunc?.Invoke();
    }


    // Please call this function when a ball hits a peg
    // ボールがペグに当たったときにこの関数を呼び出してください
    public void UpdateBallType(BallType type)
    {
        nextType = type;
    }

    // When Ball sprite needs to be retrieved from the manager
    public Sprite GetSpriteForBallType(BallType type = BallType.Normal)
    {
        switch (type)
        {
            case BallType.Chicken:
                return chickenBall;

            case BallType.Cow:
                return cowBall;

            case BallType.Duck:
                return duckBall;

            case BallType.Horse:
                return horseBall;

            case BallType.Sheep:
                return sheepBall;
        }

        return normalBall;  // Case of default - Normal Ball
    }

    // Ensure consistent position over screen size
    private Vector3 CoordinateConversion(float NDCx, float NDCy)
    {
        return Cam.ViewportToWorldPoint(new Vector3(NDCx, NDCy, 0));
    }

    // Set target balls to move
    private void SetTarget(Rigidbody2D rigidBody, Vector2 targetVector)
    {
        int index = movingBalls.IndexOf(rigidBody);

        // If it is already registered update its target
        if (index >= 0)
        {
            targetPosition[index] = targetVector;
        }
        else
        {
            // If not in movingBalls, Add it to the list
            movingBalls.Add(rigidBody);
            targetPosition.Add(targetVector);
        }
    }

    // Generation of Balls
    private Ball CreateBall(BallType type, Vector2 position)
    {
        GameObject obj = SpawnBallByType(position, Quaternion.identity, type);

        Ball ball = obj.GetComponent<Ball>();

        return ball;
    }


    // Function used to spawn balls by type
    private GameObject SpawnBallByType(Vector2 spawnPos, Quaternion rotation, BallType type = BallType.Normal)
    {
        switch (type)
        {
            case BallType.Chicken:
                return Instantiate(chickenBallPrefab, spawnPos, rotation);

            case BallType.Cow:
                return Instantiate(cowBallPrefab, spawnPos, rotation);

            case BallType.Duck:
                return Instantiate(duckBallPrefab, spawnPos, rotation);

            case BallType.Horse:
                return Instantiate(horseBallPrefab, spawnPos, rotation);

            case BallType.Sheep:
                return Instantiate(sheepBallPrefab, spawnPos, rotation);
        }

        return Instantiate(normalBallPrefab, spawnPos, rotation);  // Case of default - Normal Ball
    }

    // Call when the ball is getting destroyed
    public void DestroyBall(Ball ball)
    {
        if (ball == null)
            return;

        if (currentBall == ball)
        {
            currentBall = null;
        }

        storage.Remove(ball);
        activeBalls.Remove(ball);

        OnDestroyBall?.Invoke(
            this,
            new BallObjectArgs
            {
                BallObject = ball
            }
        );

        Destroy(ball.gameObject);
    }
}