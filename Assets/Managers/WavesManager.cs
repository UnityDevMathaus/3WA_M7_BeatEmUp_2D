using UnityEngine;

public class WavesManager : MonoBehaviour
{
    #region VARIABLES SERIALISEES
    [SerializeField] private Levels _currentLevel;
    [SerializeField] private Waves[] _waves;
    [SerializeField] private IntVariable _currentCheckpoint;
    [SerializeField] private IntVariable _currentWaveIndex;
    #endregion
    #region AUTRES VARIABLES
    private Waves _currentWave;
    private int _currentWaveIndexValue;
    private float _timeForStartingWave;
    #endregion
    #region API UNITY
    void Awake()
    {
        _timeForStartingWave = Time.time;
    }
    void Start()
    {
        _currentWaveIndexValue = 0;
        _currentWave = _waves[0];
        _currentWave.gameObject.SetActive(true);
    }
    void Update()
    {
        WavesMecanics();
    }
    #endregion
    #region FONCTIONS
    /// <summary>
    /// Mécaniques du WavesManager :
    /// Tant qu'une Wave existe, et que le délais d'attente de la Wave est passé,
    /// On vérifie qu'il ne s'agit pas de la dernière Wave,
    /// Si c'est le cas, on la détruit et on modifie la variable AllWavesCleared du Level à true,
    /// puis on désactive et détruit le GameObject du WavesManager.
    /// Sinon, si la Wave est terminée, on réinitialise le timer et on change de Wave.
    /// </summary>
    private void WavesMecanics()
    {
        if (_currentWave && Time.time > _timeForStartingWave + _currentWave.DelayBeforeWaveStart)
        {
            if (_currentWaveIndexValue >= _waves.Length - 1)
            {
                DestroyWave();
                _currentLevel.AllWavesCleared = true;
                DestroyWavesManagerWhenAllCleared();
            }
            else if (_currentWave.GoToNextWave)
            {
                _timeForStartingWave = Time.time;
                ChangeWave();
            } else
            {
                WaveCheckpointPassed();
            }
        }
    }
    /// <summary>
    /// Désactive puis détruit le GameObject du WavesManager.
    /// </summary>
    private void DestroyWavesManagerWhenAllCleared()
    {  
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    /// <summary>
    /// Détruit une Wave si elle existe et assigne à null ses références.
    /// </summary>
    private void DestroyWave()
    {
        if (_currentWave)
        {
            _currentWave.SetInactiveAndDestroy();
            _waves[_currentWaveIndexValue] = null;
            _currentWave = null;
        }
    }
    /// <summary>
    /// Détruit la Wave courante,
    /// incrémente la variable permettant de récupérer la prochaine Wave dans le tableau sérialisé,
    /// assigne une nouvelle Wave courante,
    /// et active son GameObject.
    /// </summary>
    private void ChangeWave()
    {
        DestroyWave();
        _currentWaveIndexValue++;
        _currentWaveIndex.Value = _currentWaveIndexValue;
        _currentWave = _waves[_currentWaveIndexValue];
    }
    private void WaveCheckpointPassed()
    {
        if (_currentWave.WaveCheckpoint <= _currentCheckpoint.Value)
        {

            _currentWave.gameObject.SetActive(true);       
        }
    }
    #endregion
}