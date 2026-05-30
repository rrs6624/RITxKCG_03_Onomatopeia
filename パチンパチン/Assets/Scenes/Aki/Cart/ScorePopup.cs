using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro tmp;

    [SerializeField]
    private float popupSpeed;

    [SerializeField]
    private float popupDuration;

    private float timer;

    private void Start()
    {
        // 一定時間後にポップアップを削除する(Destroy the popup after a certain duration)
        Destroy(gameObject, popupDuration);

        timer = 0f;
    }
    public void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        var progress = timer / popupDuration;

        PopupMove();

        FadeOut(progress);

        ScaleUpDown(progress);
    }

    public void SetScore(int score)
    {
        tmp.text = score.ToString();
    }

    private void PopupMove()
    {
        // ポップアップを上に移動させる(Move the popup upwards)
        transform.position += Vector3.up * popupSpeed * Time.deltaTime;
    }

    private void FadeOut(float progress)
    {
        Color color = tmp.color;

        // 徐々に透明にするためにアルファ値を減少させる(Decrease the alpha value to fade out)
        color.a = 1.0f - progress;

        tmp.color = color;
    }

    private void ScaleUpDown(float progress)
    {
        float scale = 1.0f;

        if(progress < popupDuration * 0.4f)
        {
            // 最初の40%の時間は拡大する(Scale up during the first 40% of the duration)
            scale = Mathf.Lerp(1.0f, 1.5f, progress / (popupDuration * 0.4f));
        }
        else if (progress < popupDuration * 0.8f)
        {
            // 次の40%の時間は縮小する(Scale down during the next 40% of the duration)
            scale = Mathf.Lerp(1.5f, 1.0f, (progress - popupDuration * 0.4f) / (popupDuration * 0.4f));
        }

        transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
