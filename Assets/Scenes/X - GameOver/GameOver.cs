using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;

    void Update()
    {
        _sceneLoader.DebugSceneLoader();
    }
}