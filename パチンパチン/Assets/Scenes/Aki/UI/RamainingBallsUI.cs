using TMPro;
using UnityEngine;

public class RamainingBallsUI : MonoBehaviour
{
    private BallManager ballManager;

    private TextMeshProUGUI tmp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballManager = BallManager.Instance;
        ballManager.onCountUIFunc += RemainingCountChanged;

        tmp = GetComponent<TextMeshProUGUI>();

        tmp.text = ballManager.Count.ToString();
    }

    public void RemainingCountChanged()
    {
        tmp.text = ballManager.Count.ToString();
    }
}
