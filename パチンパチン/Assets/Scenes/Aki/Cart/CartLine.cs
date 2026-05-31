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
        // 左に移動(Moving left)
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // もし開始位置からリセット距離文移動したら、位置をリセット(If it has moved the reset distance from the start position, reset the position)
        if (transform.position.x <= startPosX - resetPosX)
        {
            transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
        }
    }
}
