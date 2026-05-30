using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;

public class BallLauncher : MonoBehaviour
{

    //initializing all the fields
    //すべてのフィールドを初期化します
    private Rigidbody2D rb;
    private float maxForce = 100f;
    private float currentCharge = 0f;
    private float chargeRate = 10f;
    private bool fire = false;
    private bool isCharging = false;
    private float startPosY;
    private bool returning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //setting the start position so the launcher will bounce back to that later
        // 開始位置を設定することで、ランチャーが後でその位置に戻るようにします
        startPosY = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            //as it moves down, it builds up momentum to launch the ball farther. 
            //落下するにつれて勢いが増し、ボールをより遠くまで飛ばします。
            currentCharge = Mathf.Min(currentCharge + chargeRate * Time.deltaTime, maxForce);
            //moves the platform down while it's not fully charged
            //プラットフォームが完全に充電されていない状態で下に移動させる
            if (currentCharge < maxForce)
            {
                rb.MovePosition(new Vector2(rb.position.x, rb.position.y - currentCharge * Time.deltaTime));
            }

            //stops once it reaches full
            //満杯になると停止します
            if (currentCharge >= maxForce)
            {
                isCharging = false;
            }
        }

        else if (fire)
        {
            //launches the ball with the current charge and resets the charge and starts returning to the start position
            //現在の電荷でボールを発射し、電荷をリセットして開始位置に戻り始めます
            fire = false;
            returning = true;
            rb.linearVelocity = new Vector2(0, currentCharge);
            currentCharge = 0;
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
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isCharging = false;
            fire = true;
        }
    }
}
