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
    /// Mécaniques des Waves :
    /// Tant qu'une Wave n'est pas de type DIALOGS,
    /// On vérifie si le nombre d'entitées restantantes de la Wave correspond au nombre d'entitées restantes du Level,
    /// Si c'est le cas, la propriété GoToNextWave passe à true.
    /// Si, c'est une Wave de type DIALOGS, on attend un délai.
    /// Puis une fois le délai passé, on passe la propriété GoToNextWave à true.
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
    /// Récupère le composant Level du parent.
    /// Déclenche une exception si la variable est nulle.
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
    /// Assigne le type de Wave en fonction du paramètre sérialisé de l'inspector.
    /// Défini ensuite le nombre d'entitées de la Wave et le IManager associé.
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
    /// calcule le nombre d'entitées restantes pour les Waves suivantes.
    /// (note : On en déduiera que si ce nombre est égal au nombre total d'entitées restantes, c'est que la Wave est clear.)  
    /// </summary>
    private void SetNextEntitiesRemaining()
    {
        if (_waveType != WavesTypes.DIALOGS) _entitiesNextRemaining = _entitiesManager.EntitiesRemaining() - _entitiesCount;
    }
    #endregion
}