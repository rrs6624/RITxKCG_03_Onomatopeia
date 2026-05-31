using UnityEngine;
using System.Collections.Generic;

public class SheepMnager : MonoBehaviour
{
    [Header("ーー 生成するバネのプレハブ ーー")]
    [SerializeField] private GameObject springPrefab;

    [Header("ーー ステージ全体の範囲設定 ーー")]
    [SerializeField] private Vector2 minBounds; // ステージの左下 (x, y)
    [SerializeField] private Vector2 maxBounds; // ステージの右上 (x, y)

    [Header("ーー 重なり防止の設定 ーー")]
    [SerializeField] private float checkRadius = 0.6f; // バネの大きさより少し大きめの円で重なりをチェックします
    [SerializeField] private LayerMask obstacleLayer;  // 重なりを検知したいレイヤー

    private int maxSpawnCount = 3; // 配置する個数
    private int maxAttempts = 50;  // 無限ループ防止

    void Start()
    {
        SpawnSprings();
    }

    private void SpawnSprings()
    {
        if (springPrefab == null)
        {
            return;
        }

        int spawnedCount = 0;

        for (int i = 0; i < maxAttempts; i++)
        {
            if (spawnedCount >= maxSpawnCount) break;

            // 範囲からランダムな位置を決定
            float randomX = Random.Range(minBounds.x, maxBounds.x);
            float randomY = Random.Range(minBounds.y, maxBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, checkRadius, obstacleLayer);

            if (hit == null)
            {
                Instantiate(springPrefab, spawnPosition, Quaternion.identity);
                spawnedCount++;
            }
        }

        Debug.Log($"ステージにバネを {spawnedCount} 個配置");
    }

    // ステージ全体エリアを表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2, (minBounds.y + maxBounds.y) / 2, 0);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 1);
        Gizmos.DrawWireCube(center, size);
    }
}