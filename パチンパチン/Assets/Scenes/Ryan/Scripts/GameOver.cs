using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    private int score;
    private ScoreManager manager;

    private int testScore = 10;
    
    
    [SerializeField] private TextMeshProUGUI textScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = ScoreManager.Instance;
        score = manager.Score;
        textScore.text =  "Here's your Score: " + testScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
