using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3 : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;

    void Update()
    {
        _currentScene.Value = SceneManager.GetActiveScene().buildIndex;
        _sceneLoader.DebugSceneLoader();
    }
}