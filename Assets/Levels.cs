using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private GameObject _maestroCharacters;
    private PlayersManager _playersManager;
    private DoppelgangersManager _doppelgangersManager;
    private BossesManager _bossesManager;
    private EnemiesManager _enemiesManager;

    private void Awake()
    {
        _currentScene.Value = SceneManager.GetActiveScene().buildIndex;
        _playersManager = _maestroCharacters.GetComponent<PlayersManager>();
        _doppelgangersManager = _maestroCharacters.GetComponent<DoppelgangersManager>();
        _bossesManager = _maestroCharacters.GetComponent<BossesManager>();
        _enemiesManager = _maestroCharacters.GetComponent<EnemiesManager>();

    }

    void Update()
    {
        GoToNextLevelWhenCleared();
        _sceneLoader.DebugSceneLoader();
    }

    private void GoToNextLevelWhenCleared()
    {
        if (_doppelgangersManager.DoppelgangersRemaining() == 0)
        {
            _sceneLoader.GoToNextLevel();
        }
    }
    private void GoToNextWaveWhenCleared()
    {

    }

}