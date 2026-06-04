using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;

public class BallLauncher : MonoBehaviour
{
    public static BallLauncher Instance;

    //initializing all the fields
    //すべてのフィ?ルドを初期化します
    private Rigidbody2D rb;
    private float maxForce = 20f;
    private float currentCharge = 0f;
    private float chargeRate = 10f;
    private bool fire = false;
    private bool isCharging = false;
    private float startPosY;
    private float endPosY;
    private bool returning = false;

    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private BallManager ballManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballManager = BallManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        //setting the start position so the launcher will bounce back to that later
        // 開始位置を設定することで、ラン?ャ?が後でその位置に戻るようにします
        startPosY = boxCollider.transform.localPosition.y;
        animator.SetBool("IsIdle", true); 
        endPosY = startPosY - 0.5f; // Adjust this value to control how far down the launcher goes when charging

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Previous memory has not been cleared.");
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            //as it moves down, it builds up momentum to launch the ball farther. 
            //落下するにつれて勢いが増し、??ルをより遠くまで飛ばします。
            currentCharge = Mathf.Min(currentCharge + chargeRate * Time.deltaTime, maxForce);
            //moves the platform down while it's not fully charged
            //プラットフォ??が完全に?電されていない状態で下に移動させる
            if (currentCharge < maxForce)
            {
                rb.MovePosition(new Vector2(rb.position.x, rb.position.y - currentCharge * Time.deltaTime));
                //boxCollider.transform.localPosition = new Vector2(boxCollider.transform.localPosition.x, Mathf.Min(boxCollider.transform.localPosition.y - currentCharge * Time.deltaTime, endPosY));
                //boxCollider.offset = new Vector2(boxCollider.offset.x, Mathf.Max(startPosY - currentCharge * Time.deltaTime, endPosY));
            }

            //stops once it reaches full
            //満杯になると停?します
            if (currentCharge >= maxForce)
            {
                isCharging = false;
                animator.SetBool("IsCharging", false);
                animator.SetBool("IsCharged", true);
            }
        }

        else if (fire)
        {
            //launches the ball with the current charge and resets the charge and starts returning to the start position
            //現在の電荷で??ルを発射し、電荷をリセットして開始位置に戻り始めます
            boxCollider.offset = new Vector2(boxCollider.offset.x, startPosY);
            fire = false;
            returning = true;
            rb.linearVelocity = new Vector2(0, currentCharge);
            currentCharge = 0;
            
            animator.SetBool("IsCharged", true);
        }

        if (returning && rb.position.y >= startPosY)
        {
            //returns to its start position
            //元の位置に戻ります
            returning = false;
            rb.linearVelocity = Vector2.zero;
            rb.MovePosition(new Vector2(rb.position.x, startPosY));
        }


        //inputs
        //入力
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isCharging = true;
            animator.SetBool("IsCharging", true);
            animator.SetBool("IsIdle", false);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isCharging = false;
            fire = true;
            animator.SetBool("IsCharging", false);
            animator.SetBool("IsFiring", true);
            BallManager.Instance.LaunchCurrentBall();
        }
    }

    public Vector2 GetPosition()
    {
        return rb.position;
    }
}
