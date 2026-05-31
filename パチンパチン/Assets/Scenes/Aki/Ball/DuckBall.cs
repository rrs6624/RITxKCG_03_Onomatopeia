using UnityEngine;

public class DuckBall : Ball
{
    bool isHitAnimalPin = false;

    public override void HitAnimalPin(BallType type, int addScore)
    {
        // ランダムに座標をずらす用の値
        float randomOffsetX = Random.Range(-0.5f, 0.5f);
        float randomOffsetY = Random.Range(-0.5f, 0.5f);

        // 値を反映させる
        Vector3 spawnPosition = new Vector3(transform.position.x + randomOffsetX, transform.position.y + randomOffsetY, transform.position.z);

        // 通常の球を増やす
        if (!isHitAnimalPin)
        {
            BallManager.Instance.SpawnBonusBall(spawnPosition, BallType.Normal);

            isHitAnimalPin = true;
        }

        base.HitAnimalPin(BallType.Duck, addScore);
    }
}
