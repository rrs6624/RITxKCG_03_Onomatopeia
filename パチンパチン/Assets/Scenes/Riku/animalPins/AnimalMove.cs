using UnityEngine;
using System.Collections;

public class AnimalMove : MonoBehaviour
{
    enum AnimalState { Idle, Walk }

    [Header("ーー 範囲と速度の設定 ーー")]
    [SerializeField] private float walkRange = 5f;      
    [SerializeField] private float moveSpeed = 2f;      

    [Header("ーー 時間の設定 ーー")]
    [SerializeField] private float minIdleTime = 1f;    
    [SerializeField] private float maxIdleTime = 4f;    

    private Vector2 homePosition;   // 生まれた場所
    private Vector2 targetPosition; // 次の目的地
    private AnimalState currentState = AnimalState.Idle;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        homePosition = transform.position;

        StartCoroutine(Animalmove());
    }

    private IEnumerator Animalmove()
    {
        while (true)
        {
            if (currentState == AnimalState.Idle)
            {
                rb.linearVelocity = Vector2.zero;

                float idleDuration = Random.Range(minIdleTime, maxIdleTime);
                yield return new WaitForSeconds(idleDuration);

                targetPosition = GetRandomPositionInRange();
                currentState = AnimalState.Walk;
            }
            else if (currentState == AnimalState.Walk)
            {
                while (Vector2.Distance(transform.position, targetPosition) > 0.2f)
                {
                    yield return null;
                }

                currentState = AnimalState.Idle;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentState == AnimalState.Walk)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    private Vector2 GetRandomPositionInRange()
    {
        Vector2 randomCircle = Random.insideUnitCircle * walkRange;
        return homePosition + randomCircle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = Application.isPlaying ? (Vector3)homePosition : transform.position;
        Gizmos.DrawWireSphere(center, walkRange);
    }
}