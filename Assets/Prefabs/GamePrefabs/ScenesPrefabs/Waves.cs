using UnityEngine;

public class Waves : MonoBehaviour
{
    #region VARIABLES SERIALISEES
    [SerializeField] private WavesTypes _waveType;
    [SerializeField] private CheckpointsList _waveCheckpoint; public int WaveCheckpoint { get => (int)_waveCheckpoint; }
    [SerializeField] private float _delayBeforeWaveStart = 2f; public float DelayBeforeWaveStart { get => _delayBeforeWaveStart; }
    [SerializeField] private BoolVariable _currentWaveIsActive;
    #endregion
    #region AUTRES VARIABLES
    private Levels _currentLevel;
    private IManager _entitiesManager;
    private int _entitiesCount;
    private int _entitiesNextRemaining;
    private bool _goToNextWave; public bool GoToNextWave { get => _goToNextWave; }
    #endregion
    #region API UNITY
    void Awake()
    {
        _currentWaveIsActive.Value = true;
        SettingWavesAtAwake();
        SetWaveType();
    }
    void Start()
    {
        SetNextEntitiesRemaining();
    }
    void Update()
    {
        WavesMecanics();
    }
    public void SetInactiveAndDestroy()
    {
        _currentWaveIsActive.Value = false;
        Destroy(gameObject);
    }
    #endregion
    #region FONCTIONS
    /// <summary>
    /// M�caniques des Waves :
    /// Tant qu'une Wave n'est pas de type DIALOGS,
    /// On v�rifie si le nombre d'entit�es restantantes de la Wave correspond au nombre d'entit�es restantes du Level,
    /// Si c'est le cas, la propri�t� GoToNextWave passe � true.
    /// Si, c'est une Wave de type DIALOGS, on attend un d�lai.
    /// Puis une fois le d�lai pass�, on passe la propri�t� GoToNextWave � true.
    /// </summary>
    private void WavesMecanics()
    {
        if (_waveType != WavesTypes.DIALOGS)
        {
            _goToNextWave = (_entitiesNextRemaining == _entitiesManager.EntitiesRemaining());
        }
        else
        {
            //todo : delai des dialogs.
            _goToNextWave = true;
        }
    }
    /// <summary>
    /// R�cup�re le composant Level du parent.
    /// D�clenche une exception si la variable est nulle.
    /// </summary>
    private void SettingWavesAtAwake()
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        if (level) _currentLevel = level.GetComponent<Levels>();
        else
        {
            throw new System.Exception("GameObject with tag Level not found.");
        }
    }
    /// <summary>
    /// Assigne le type de Wave en fonction du param�tre s�rialis� de l'inspector.
    /// D�fini ensuite le nombre d'entit�es de la Wave et le IManager associ�.
    /// </summary>
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
            case WavesTypes.DIALOGS:
                _entitiesCount = 0;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Si la Wave n'est pas de type DIALOGS,
    /// calcule le nombre d'entit�es restantes pour les Waves suivantes.
    /// (note : On en d�duiera que si ce nombre est �gal au nombre total d'entit�es restantes, c'est que la Wave est clear.)  
    /// </summary>
    private void SetNextEntitiesRemaining()
    {
        if (_waveType != WavesTypes.DIALOGS) _entitiesNextRemaining = _entitiesManager.EntitiesRemaining() - _entitiesCount;
    }
    #endregion
}