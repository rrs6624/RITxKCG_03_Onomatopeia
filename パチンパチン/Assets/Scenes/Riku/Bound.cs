using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Bound : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 1.5f;
    [SerializeField]
     private float BigBounceForce = 2.0f;

    [SerializeField]
    private Vector3 boostedScale = new Vector3(2f, 2f, 2f);


    [SerializeField]
    private float maintainTime = 0.2f;
    [SerializeField]
    private float bounceCoolTime = 0.15f;

    private Vector3 originalScale;
    private float originalBounceForce;
    private bool isTesting = false;

    private float coolTimeTimer = 0f;

    private void Start()
    {
        originalScale = transform.localScale;
        originalBounceForce = bounceForce;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !isTesting)
        {
            StartCoroutine(FlashLargeCoroutine());
            Debug.Log("Space.");
        }

       
        if (coolTimeTimer > 0f)
        {
            coolTimeTimer -= Time.deltaTime;
        }
    }

    private IEnumerator FlashLargeCoroutine()
    {
        isTesting = true;

        bounceForce = BigBounceForce;
        transform.localScale = boostedScale;

        yield return new WaitForSeconds(maintainTime);

        transform.localScale = originalScale;
        bounceForce = originalBounceForce;

        isTesting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (coolTimeTimer > 0f) return;

            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                Vector2 currentVelocity = playerRb.linearVelocity;
                Vector2 oppositeVelocity = -currentVelocity * bounceForce;
                playerRb.linearVelocity = oppositeVelocity;

               
                coolTimeTimer = bounceCoolTime;
            }
        }
    }
}