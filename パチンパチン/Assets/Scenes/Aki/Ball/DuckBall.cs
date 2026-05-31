using UnityEngine;

public class DuckBall : Ball
{
    bool isHitAnimalPin = false;

    public override void HitAnimalPin(BallType type, int addScore)
    {
        // ’Êí‚Ì‹…‚ğ‘‚â‚·
        if (!isHitAnimalPin)
        {
            //BallManager.Instance.

            isHitAnimalPin = true;
        }

        base.HitAnimalPin(BallType.Duck, addScore);
    }
}
