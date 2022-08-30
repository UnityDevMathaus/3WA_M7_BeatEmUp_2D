using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    [SerializeField] private Levels _currentLevel;
    private float _timeForStartingWave;
    private Waves _currentWave;
    private int _currentWaveIndex;

    void Awake()
    {
        _timeForStartingWave = Time.time;
    }

    void Start()
    {
        _currentWaveIndex = 0;
        _currentWave = _waves[0];
        _currentWave.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Time.time > _timeForStartingWave + _currentWave.DelayBeforeWaveStart)
        {
            if (_currentWave.GoToNextWave)
            {
                _timeForStartingWave = Time.time;
                ChangeWave();
                //todo : completer avec la condition si _currentWaveIndex dépasse la taille du tableau.
            }
        }
    }

    public void ChangeWave()
    {
        Destroy(_currentWave.gameObject);
        _waves[_currentWaveIndex] = null;
        _currentWaveIndex++;
        _currentWave = _waves[_currentWaveIndex];
        _currentWave.gameObject.SetActive(true);
    }


}