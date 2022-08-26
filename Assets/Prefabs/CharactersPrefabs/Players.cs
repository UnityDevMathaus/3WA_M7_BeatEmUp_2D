using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;
    [SerializeField] private CurrentPlayer _currentPlayer;
    [SerializeField] private PlayersInputsList _inputsMode;

    private PlayersStats _playerStats;
    private PlayersInputs _playerInput;
    private CharactersRenderers _renderer;

    private IntVariable _playerHP; public IntVariable PlayerHP { get => _playerHP; set => _playerHP = value; }
    private IntVariable _playerMP; public IntVariable PlayerMP { get => _playerMP; set => _playerMP = value; }
    private IntVariable _playerLife; public IntVariable PlayerLife { get => _playerLife; set => _playerLife = value; }

    private void Awake()
    {
        _playerStats = GetComponentInChildren<PlayersStats>();
        _playerInput = GetComponentInChildren<PlayersInputs>();
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }

    void Start()
    {
        _playerInput.ActiveInputs(_inputsMode);
        _renderer.SetPlayersSprites(_playerIdentity);   
        _playerStats.CurrentPlayer = _currentPlayer;
        _playerStats.ResetCurrentPlayerStats();
        _playerHP = _playerStats.CurrentPlayerStats[0];
        _playerMP = _playerStats.CurrentPlayerStats[1];
        _playerLife = _playerStats.CurrentPlayerStats[2];
    }
    void Update()
    {
        DebugLife();
    }
    private void DebugLife()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) _playerHP.Value--;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _playerMP.Value--;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _playerLife.Value--;
    }
}