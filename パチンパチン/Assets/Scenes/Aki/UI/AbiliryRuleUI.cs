using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbiliryRuleUI : MonoBehaviour
{
    [SerializeField]
    private Button changeLanguageButton;

    private TextMeshProUGUI buttonText;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite japaneseSprite;

    [SerializeField]
    private Sprite englishSprite;

    private bool isEnglish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonText = changeLanguageButton.GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = englishSprite;

        isEnglish = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickChangeLanguage()
    {
        if (isEnglish)
        {
            buttonText.text = "EN";
            image.sprite = japaneseSprite;
        }
        else
        {
            buttonText.text = "JP";
            image.sprite = englishSprite;
        }

        isEnglish = !isEnglish;
    }
}
