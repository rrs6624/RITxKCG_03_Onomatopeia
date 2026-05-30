using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("TestScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
