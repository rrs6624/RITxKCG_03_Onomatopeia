using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Bounced : MonoBehaviour
{
    enum ThisBallType
    { 
        Horse,
        Duck,
        Sheep,
        Chicken,
        Cow
    }
    [SerializeField] 
    private ThisBallType thisBallType=ThisBallType.Horse;
    [Header("ーー 吹き飛ばす力の設定 ーー")]
    [SerializeField] private float spikeForce = 15f;
    [SerializeField] private float blastRadius = 2.5f;

    [Header("ーー 見た目の設定 ーー")]
    [SerializeField] private Vector3 boostedScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private float maintainTime = 0.2f;

    private Vector3 originalScale;
    private bool isTesting = false; // 連打防止

    private void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // スペースキーが押された瞬間、かつ演出中でなければ実行
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !isTesting)
        {
            StartCoroutine(FlashLargeCoroutine());
            DetonateSpike();
            Debug.Log("Spike Activated!");
        }
    }

    // 起動時に一瞬だけ巨大化させる演出
    private IEnumerator FlashLargeCoroutine()
    {
        isTesting = true;

        transform.localScale = boostedScale;
        yield return new WaitForSeconds(maintainTime);
        transform.localScale = originalScale;

        isTesting = false;
    }

    // 範囲内のプレイヤーを検知して吹き飛ばす
    private void DetonateSpike()
    {
        // 中心から半径 blastRadius 内のコライダーをすべて取得
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                Rigidbody2D playerRb = hit.GetComponent<Rigidbody2D>();

                if (playerRb != null)
                {    var ball = playerRb.gameObject.GetComponent<Ball>();

                    if(ball != null)
                    {
                        if (ball.BallAbilityType == BallType.Horse)
                        {
                            Vector2 pushDirection = (hit.transform.position - transform.position).normalized;
                            playerRb.linearVelocity += pushDirection * spikeForce*2;
                        }
                        else
                        {
                            Vector2 pushDirection = (hit.transform.position - transform.position).normalized;
                            playerRb.linearVelocity += pushDirection * spikeForce;
                        }
                        //ball.HitAnimalPin() 
                    }

                    // 中心からプレイヤーへの方向（ベクトル）を計算して力を加える
                    

                
                }
            }
        }
    }

    // Unityエディタ上で有効範囲（赤色の円）を表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}