using System;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    private enum ScoreType
    {
        Cow,
        Chicken,
        Duck,
        Horse,
        Sheep
    }
    [SerializeField]
    private ScoreType targetAnimal = ScoreType.Cow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
