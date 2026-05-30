using UnityEngine;

public class ChickenBall : Ball
{
    public override void HitAnimalPin(BallType type, int addScore)
    {
        // ’Êí‚Ì‹…‚ğ‘‚â‚·
        base.HitAnimalPin(BallType.Chicken, addScore);
    }

    public override void GoaltoCart()
    {
        // c‚è‚Ì‹…‚Ì”‚ğ‘‚â‚·
    }
}
