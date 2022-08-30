using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private IntVariable _currentScene;
    [SerializeField] private GameObject _maestroCharacters;
    private PlayersManager _playersManager;
    private FinalBossesManager _finalBossesManager; public FinalBossesManager FinalBossesManager { get => _finalBossesManager; }
    private DoppelgangersManager _doppelgangersManager; public DoppelgangersManager DoppelgangersManager { get => _doppelgangersManager; }
    private BossesManager _bossesManager; public BossesManager BossesManager { get => _bossesManager; }
    private EnemiesManager _enemiesManager; public EnemiesManager EnemiesManager { get => _enemiesManager; }
    private bool _allWavesCleared; public bool AllWavesCleared { get => _allWavesCleared; set => _allWavesCleared = value; }

    private void Awake()
    {
        _currentScene.Value = SceneManager.GetActiveScene().buildIndex;
        _playersManager = _maestroCharacters.GetComponent<PlayersManager>();
        _finalBossesManager = _maestroCharacters.GetComponent<FinalBossesManager>();
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
        if (_allWavesCleared)
        {
            _sceneLoader.GoToNextLevel();
        }
    }
    private void GoToNextWaveWhenCleared()
    {

    }

}