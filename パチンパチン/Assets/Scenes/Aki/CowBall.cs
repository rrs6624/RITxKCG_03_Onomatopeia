using UnityEngine;

public class CowBall : Ball
{
    [SerializeField]
    private int scoreMultiplier;

    public override int GetBallScore()
    {
        return base.GetBallScore() * scoreMultiplier;
    }
}
