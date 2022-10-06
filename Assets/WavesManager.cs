using UnityEngine;

public class WavesManager : MonoBehaviour
{
    #region VARIABLES SERIALISEES
    [SerializeField] private Levels _currentLevel;
    [SerializeField] private Waves[] _waves;
    #endregion
    #region VARIABLES
    private Waves _currentWave;
    private int _currentWaveIndex;
    private float _timeForStartingWave;
    #endregion
    #region API UNITY
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
        WavesMecanics();
    }
    #endregion
    #region FONCTIONS
    /// <summary>
    /// M�caniques du WavesManager :
    /// Tant qu'une Wave existe, et que le d�lais d'attente de la Wave est pass�,
    /// On v�rifie qu'il ne s'agit pas de la derni�re Wave,
    /// Si c'est le cas, on la d�truit et on modifie la variable AllWavesCleared du Level � true.
    /// Sinon, si la Wave est termin�e, on r�initialise le timer et on change de Wave.
    /// </summary>
    private void WavesMecanics()
    {
        if (_currentWave && Time.time > _timeForStartingWave + _currentWave.DelayBeforeWaveStart)
        {
            if (_currentWaveIndex >= _waves.Length - 1)
            {
                DestroyWave();
                _currentLevel.AllWavesCleared = true;
            }
            else if (_currentWave.GoToNextWave)
            {
                _timeForStartingWave = Time.time;
                ChangeWave();
            }
        }
    }
    /// <summary>
    /// D�truit une Wave si elle existe et assigne � null ses r�f�rences.
    /// </summary>
    private void DestroyWave()
    {
        if (_currentWave)
        {
            Destroy(_currentWave.gameObject);
            _waves[_currentWaveIndex] = null;
            _currentWave = null;
        }
    }
    /// <summary>
    /// D�truit la Wave courante,
    /// incr�mente la variable permettant de r�cup�rer la prochaine Wave dans le tableau s�rialis�,
    /// assigne une nouvelle Wave courante,
    /// et active son GameObject.
    /// </summary>
    private void ChangeWave()
    {
        DestroyWave();
        _currentWaveIndex++;
        _currentWave = _waves[_currentWaveIndex];
        _currentWave.gameObject.SetActive(true);
    }
    #endregion
}