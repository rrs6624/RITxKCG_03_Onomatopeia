using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    public GameObject Ballprefab;
    public GameObject currentBall;

    public List<GameObject> storage;          // This will store the instantiated ball objects that will be shown on the screen (Only 3 currently)

    public Sprite chickenBall;
    public Sprite duckBall;
    public Sprite cowBall;
    public Sprite horseBall;
    public Sprite sheepBall;

    public int Count { get; private set; }

    private BallType currentType;
    private BallType nextType;


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
        currentType = BallType.Normal;
        nextType = BallType.Normal;

        // Set up storage 
        storage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Please call this function before game starts (Ball manager) - It sets up how many balls are used in this game or level
    // ゲーム開始前にこの関数（ボールマネージャー）を呼び出してください。この関数は、このゲームまたはレベルで使用されるボールの数を設定します。
    // Ensure that it is more than 10!!! | 必ず10以上であることを確認してください!!!
    public void Setup(int givenBall)
    {
        Count = givenBall;

        // Instantiate current ball on screen(this means putting it on the right position as well)
        currentBall = Instantiate(Ballprefab);

        // Storage Setup
        // Instantiate 3 ball objects to be in storage (For loop with instantiation and push)s
       
    }

    // Please call this function after a ball hits a cart 
    public void Reload()
    {
        // Destroy this ball
        Destroy(currentBall);

        // Decrease count
        Count--;

        // Deploy the ball from storage and free storage
        currentBall = storage[0];
        storage.RemoveAt(0);

        // Set the current ball position

        // Instantiate another ball to add to storage (would also have to drop it - a little bit of physics)

        // Update the types of balls
        currentType = nextType;
        nextType = BallType.Normal;
    }


    // Please use this function when you need to increase the count of the total number of given balls
    // 与えられたボールの総数を増やす必要がある場合は、この関数を使用してください。
    public void IncreaseBallCount(int count)
    {
        Count += count;
    }


    // Please call this function when a ball hits a peg
    // ボールがペグに当たったときにこの関数を呼び出してください
    public void UpdateBallType(BallType type)
    {
        nextType = type;
    }

    public 
}
