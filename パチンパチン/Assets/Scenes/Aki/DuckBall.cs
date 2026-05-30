using UnityEngine;

public class DuckBall : Ball
{
    public override void HitAnimalPin(BallType type, int addScore)
    {
        // ’Êí‚Ì‹…‚ğ‘‚â‚·


        base.HitAnimalPin(BallType.Duck, addScore);
    }
}
