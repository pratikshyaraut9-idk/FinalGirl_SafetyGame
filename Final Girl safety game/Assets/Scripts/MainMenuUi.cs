using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(("SampleScene"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}