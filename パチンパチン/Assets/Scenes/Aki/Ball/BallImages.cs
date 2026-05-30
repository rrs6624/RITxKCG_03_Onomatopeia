using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallImages", menuName = "Scriptable Objects/BallImages")]
public class BallImages : ScriptableObject
{
    [SerializeField]
    private Sprite normalBallImage;

    [SerializeField]
    private Sprite duckBallImage;

    [SerializeField]
    private Sprite cowBallImage;

    [SerializeField]
    private Sprite chickenBallImage;

    [SerializeField]
    private Sprite horseBallImage;

    [SerializeField]
    private Sprite sheepBallImage;

    public Sprite GetBallImage(BallType type)
    {
         return type switch
        {
            BallType.Normal => normalBallImage,
            BallType.Duck => duckBallImage,
            BallType.Cow => cowBallImage,
            BallType.Chicken => chickenBallImage,
            BallType.Horse => horseBallImage,
            BallType.Sheep => sheepBallImage,
            _ => null
        };
    }
}
