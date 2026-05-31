using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textManager : MonoBehaviour
{
    public TextMeshProUGUI score_text = null;

    void Start()
    {
        if (score_text != null)
        {
            score_text.text = ScoreManager.Instance.Score.ToString();
        }
        else
        {
            Debug.LogError("インスペクターにテキストがセットされていません");
        }
    }

    void Update()
    {
    }
}