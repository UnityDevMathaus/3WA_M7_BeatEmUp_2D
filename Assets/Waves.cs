using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private WavesTypes _waveType;
    [SerializeField] private float _delayBeforeWaveStart = 2f; public float DelayBeforeWaveStart { get => _delayBeforeWaveStart; }
    private Levels _currentLevel;
    private IManager _entitiesManager;

    private int _entitiesNextRemaining;
    private int _entitiesCount;
    
    private bool _goToNextWave; public bool GoToNextWave { get => _goToNextWave; }

    void Awake()
    {
        _currentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Levels>();
        SetWaveType();
    }

    void Update()
    {
        if (_waveType != WavesTypes.DIALOGUES)
        {          
            _goToNextWave = (_entitiesNextRemaining == _entitiesManager.EntitiesRemaining());
        } else
        {
            _goToNextWave = true;
        }
    }

    private void SetWaveType()
    {
        switch (_waveType)
        {
            case WavesTypes.ENEMIES:
                _entitiesCount = FindObjectsOfType<Enemies>().Length;
                _entitiesManager = _currentLevel.EnemiesManager;
                break;
            case WavesTypes.BOSSES:
                _entitiesCount = FindObjectsOfType<Bosses>().Length;
                _entitiesManager = _currentLevel.BossesManager;
                break;
            case WavesTypes.DOPPELGANGERS:
                _entitiesCount = FindObjectsOfType<Doppelgangers>().Length;
                _entitiesManager = _currentLevel.DoppelgangersManager;
                break;
            case WavesTypes.FINALBOSSES:
                _entitiesCount = FindObjectsOfType<FinalBosses>().Length;
                _entitiesManager = _currentLevel.FinalBossesManager;
                break;
            case WavesTypes.DIALOGUES:
                _entitiesCount = 0;
                break;
            default:
                break;
        }
        _entitiesNextRemaining = _entitiesManager.EntitiesRemaining() - _entitiesCount;
    }
}