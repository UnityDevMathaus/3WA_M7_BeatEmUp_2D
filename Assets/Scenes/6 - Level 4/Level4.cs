using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private GameObject _maestroCharacters;
    private PlayersManager _playersManager;

    private void Awake()
    {
        _playersManager =_maestroCharacters.GetComponentInChildren<PlayersManager>();
    }

    void Update()
    {
        _currentScene.Value = SceneManager.GetActiveScene().buildIndex;
        _sceneLoader.DebugSceneLoader();
        if (_playersManager.AllPlayersAreDead())
        {
            _sceneLoader.GameOver();
        }
    }
}