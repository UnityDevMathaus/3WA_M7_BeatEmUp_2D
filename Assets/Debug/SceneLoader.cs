using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private IntVariable _lastScene;
    [SerializeField] private IntVariable _currentScene;

    public void DebugSceneLoader()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GoToNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameOver();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            RetryCurrentLevel();
        }
    }

    public void GoToNextLevel()
    {
        //Choix personnel d'aller le chercher via SceneManager plutôt que via le ScriptableObject.
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        nextLevel = (nextLevel > _lastScene.Value) ? 0 : nextLevel;
        SceneManager.LoadScene(nextLevel);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("X - GameOver");
    }

    public void RetryCurrentLevel()
    {
        SceneManager.LoadScene(_currentScene.Value);
    }
}