using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("Level_1");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
