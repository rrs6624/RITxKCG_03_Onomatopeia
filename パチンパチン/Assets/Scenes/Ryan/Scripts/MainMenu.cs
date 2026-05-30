using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene("TestScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
