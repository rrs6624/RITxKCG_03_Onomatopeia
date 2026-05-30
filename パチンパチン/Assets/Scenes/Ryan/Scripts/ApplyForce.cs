using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class ApplyForce : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool goUp = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int speed = 10;
        if (goUp)
        {
            rb.AddForce(new Vector3(0, speed));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        goUp = false;
    }
}
