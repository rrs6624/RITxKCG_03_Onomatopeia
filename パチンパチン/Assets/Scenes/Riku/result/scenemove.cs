using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SwitchScene()
    {
        SceneManager.LoadScene("Start Screen", LoadSceneMode.Single);
    }
}