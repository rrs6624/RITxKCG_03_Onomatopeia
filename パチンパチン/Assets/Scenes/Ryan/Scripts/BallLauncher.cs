using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;

public class BallLauncher : MonoBehaviour
{
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
        startPosY = rb.position.y;
    }

    //public void OnShoot(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        Debug.Log("BEGINNING CHARGE!");
    //        isCharging = true;
    //    }
    //    else if (context.canceled)
    //    {
    //        Debug.Log("Charge Fired!");
    //        isCharging = false;
    //        fire = true;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            currentCharge = Mathf.Min(currentCharge + chargeRate * Time.deltaTime, maxForce);
            rb.MovePosition(new Vector2(rb.position.x,rb.position.y - currentCharge * Time.deltaTime));
            if (currentCharge >= maxForce)
            {
                isCharging = false;
            }
        }
        else if (fire)
        {
            fire = false;
            returning = true;
            rb.linearVelocity = new Vector2(0, currentCharge);
            currentCharge = 0;
        }

        if (returning && rb.position.y >= startPosY)
        {
            returning = false;
            rb.linearVelocity = Vector2.zero;
            rb.MovePosition(new Vector2(rb.position.x, startPosY));
        }

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
