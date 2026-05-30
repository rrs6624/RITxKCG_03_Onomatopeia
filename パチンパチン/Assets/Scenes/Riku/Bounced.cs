using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Bounced : MonoBehaviour
{
    [Header("ーー 吹き飛ばす力の設定 ーー")]
    [SerializeField]
    private float spikeForce = 15f; // 釘が発動したときの純粋な「吹き飛ばすパワー」ですわ

    [SerializeField]
    private float blastRadius = 2.5f; // 釘が届く「攻撃範囲（半径）」ですわ

    [Header("ーー 見た目の設定 ーー")]
    [SerializeField]
    private Vector3 boostedScale = new Vector3(2f, 2f, 2f);
    [SerializeField]
    private float maintainTime = 0.2f;

    private Vector3 originalScale;
    private bool isTesting = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // スペースキーが押された「瞬間」だけ、起動しますわ！
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !isTesting)
        {
            StartCoroutine(FlashLargeCoroutine());

            // 【重要】大きくなると同時に、周囲のプレイヤーを吹き飛ばす処理を呼び出しますわ！
            DetonateSpike();

            Debug.Log("Spike Activated!");
        }
    }

    private IEnumerator FlashLargeCoroutine()
    {
        isTesting = true;

        // 押された瞬間にガッと大きくして「トゲが飛び出した感」を出しますわ
        transform.localScale = boostedScale;

        yield return new WaitForSeconds(maintainTime);

        // 自動で元の釘の大きさに戻ります
        transform.localScale = originalScale;

        isTesting = false;
    }

    // 周囲のプレイヤーを探してブッ飛ばす、今回の主役となる処理ですわ
    private void DetonateSpike()
    {
        // 1. 釘の中心から半径 blastRadius の円の中にいる、すべてのコライダーを感知します
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach (var hit in hitColliders)
        {
            // 2. もし見つけた相手のタグが "Player" だったら……
            if (hit.CompareTag("Player"))
            {
                Rigidbody2D playerRb = hit.GetComponent<Rigidbody2D>();

                if (playerRb != null)
                {
                    // 3. 釘の中心からプレイヤーへの「逃げる方向（ベクトル）」を計算しますわ！
                    Vector2 pushDirection = (hit.transform.position - transform.position).normalized;

                    // 4. その方向に向かって、勢いよく速度を上書き（あるいは追加）します
                    //（前の速度を完全に無視して吹き飛ばすなら =、勢いを足すなら += にしますわ）
                    playerRb.linearVelocity = pushDirection * spikeForce;
                }
            }
        }
    }

    // Unityのエディタ画面に、釘の「有効範囲」を赤い円で表示させますわ！調整がとても楽になりますの
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}