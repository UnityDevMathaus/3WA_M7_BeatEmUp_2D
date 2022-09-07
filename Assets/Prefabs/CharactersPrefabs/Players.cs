using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;
    [SerializeField] private CurrentPlayer _currentPlayer;
    [SerializeField] private PlayersInputsList _inputsMode;

    

    private PlayersStats _playerStats;
    private PlayersInputs _playerInputs; public Inputs PlayerInputs { get => _playerInputs.PlayerInputs; }
    private CharactersRenderers _renderer;
    private PlayersCollisions _playerCollisions;

    private IntVariable _playerHP; public IntVariable PlayerHP { get => _playerHP; set => _playerHP = value; }
    private IntVariable _playerMP; public IntVariable PlayerMP { get => _playerMP; set => _playerMP = value; }
    private IntVariable _playerLife; public IntVariable PlayerLife { get => _playerLife; set => _playerLife = value; }

    

    private PlayersStateMachine _playerStateMachine;
    private void Awake()
    {
        _playerStats = GetComponentInChildren<PlayersStats>();
        _playerInputs = GetComponentInChildren<PlayersInputs>();
        _renderer = GetComponentInChildren<CharactersRenderers>();
        _playerStateMachine = GetComponent<PlayersStateMachine>();
        _playerCollisions = GetComponentInChildren<PlayersCollisions>();
    }

    void Start()
    {
        _playerInputs.ActiveInputs(_inputsMode);
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
        _isMoving = PlayerInputs.FireMove || PlayerInputs.WhileMove;
        if (_playerCollisions.IsInjuring)
        {
            _playerHP.Value--;
            _playerCollisions.IsInjuring = false;
            _playerStateMachine.OnInjuring();
        }
    }
    private void DebugLife()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) _playerMP.Value--;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _playerLife.Value--;
    }
    //################################################################################################################################
    #region MECANIQUES DES PLAYERS
    private bool _isMoving; public bool IsMoving { get => _isMoving; }
    private bool _isSprinting; public bool IsSprinting { get => _isSprinting; }
    private bool _isFighting; public bool IsFighting { get => _isFighting; }
    private bool _isJumping; public bool IsJumping { get => _isJumping; }
    private bool _isHolding; public bool IsHolding { get => _isHolding; }
    private bool _isDying; public bool IsDying { get => _isDying; }
    #region 1 - MOVING
    private void MovingMecanics()
    {

    }
    #endregion
    #region 2 - FIGHTING
    private void FigthingMecanics()
    {

    }
    #endregion
    #region 3 - HOLDING
    private void HoldingMecanics()
    {

    }
    #endregion
    #region 4 - JUMPING
    private void JumpingMecanics()
    {

    }
    #endregion
    #region 5 - INJURING
    private void InjuringMecanics()
    {

    }
    #endregion
    #region 6 - RAGING
    private void RagingMecanics()
    {

    }
    #endregion
    #region 7 - WINNING
    private void WinningMecanics()
    {

    }
    #endregion
    #endregion
}