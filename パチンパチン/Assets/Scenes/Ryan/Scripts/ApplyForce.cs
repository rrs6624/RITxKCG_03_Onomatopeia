using UnityEngine;
using UnityEngine.InputSystem;


public class ApplyForce : MonoBehaviour
{

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int speed = 10;
        rb.AddForce(new Vector3( 0, speed));

    }
}
