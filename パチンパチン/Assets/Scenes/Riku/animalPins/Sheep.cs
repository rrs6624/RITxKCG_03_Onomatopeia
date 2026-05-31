using UnityEngine;

public class Sheep : MonoBehaviour
{
    [Header("ーー スコアの設定 ーー")]
    [SerializeField] private int thisBallScore = 150; 

    [Header("ーー 吹き飛ばす力の設定 ーー")]
    [SerializeField] private float spikeForce = 15f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            
            if (ball != null)
            {
                ball.AddBallScore(thisBallScore);
            }

            if (playerRb != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                playerRb.linearVelocity = pushDirection * spikeForce;
            }
            Destroy(gameObject);
        }
    }
}