using UnityEngine;

/// <summary>
/// カートを動かすライン(CartLine)
/// </summary>
public class CartLine : MonoBehaviour
{
    /// <summary>
    /// Speed
    /// </summary>
    [SerializeField]
    private float moveSpeed;

    private float startPosX;

    [SerializeField]
    private float resetPosX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        HorizontalMove();
    }

    /// <summary>
    /// 平行移動 (HorizontalMove)
    /// </summary>
    private void HorizontalMove()
    {
        // 右に移動(Moving right)
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // もしリセット位置を超えたら座標を開始位置に戻す (If it exceeds the reset position, return the coordinates to the starting position)
        if (transform.position.x - startPosX >= resetPosX)
        {
            transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
        }
    }
}
