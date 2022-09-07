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
        PlayersMecanics();
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
    #region 0 - BASE
    private void PlayersMecanics()
    {
        WinningMecanics();
        InjuringMecanics();
        RagingMecanics();
        MovingMecanics();
        JumpingMecanics();
        FigthingMecanics();
        HoldingMecanics();

    }
    #endregion
    #region 1 - MOVING
    private void MovingMecanics()
    {
        _isMoving = PlayerInputs.FireMove || PlayerInputs.WhileMove;
    }
    #endregion
    #region 2 - FIGHTING
    private int _comboStep; public int ComboStep { get => _comboStep; }
    private float _timeForFighting;
    private float _timeForCombo;
    private float _delayForFighting = 0.2f;
    private float _delayForCombo = 0.2f;
    private void FigthingMecanics()
    {
        PlayerInputs.CanAttack = !_isFighting;
        if (PlayerInputs.FireAttack)
        {
            StartFightingTime();
            _isFighting = true;
        } else if (StopFightingTime())
        {
            _isFighting = false;
        }
    }
    private void StartFightingTime()
    {
        _timeForFighting = Time.time + _delayForFighting;
    }
    private bool StopFightingTime()
    {
        return (Time.time > _timeForFighting);
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
        if (_playerCollisions.IsInjuring)
        {
            StopInputsListening();
            _playerHP.Value--;
            _playerCollisions.IsInjuring = false;
            _playerStateMachine.OnInjuring();
        } else
        {
            StartInputsListening();
        }
    }
    #endregion
    #region 6 - RAGING
    private float _timeForRagingChanneling;
    private float _timeBeforeRagingEnd;
    private float _delayForRagingChanneling = 2f;
    private float _delayForRagingEnd = 0.8f;
    private bool _tryRaging;
    private void RagingMecanics()
    {
        ResetRagingChannelingTime();
        ResolveTryRaging();
        if (PlayerInputs.HoldSpecial && _tryRaging) {
            StopInputsListening();
            StartRagingTime();
            _playerStateMachine.OnRaging();
        } else if (StopRagingTime())
        {
            StartInputsListening();
        }
    }
    private void ResetRagingChannelingTime()
    {
        if (!PlayerInputs.HoldSpecial) _timeForRagingChanneling = Time.time;
    }
    private void ResolveTryRaging()
    {
        _tryRaging = (Time.time > _timeForRagingChanneling + _delayForRagingChanneling);
    }
    private void StartRagingTime()
    {
        _timeBeforeRagingEnd = Time.time + _delayForRagingEnd;
    }
    private bool StopRagingTime()
    {
        return (Time.time > _timeBeforeRagingEnd);
    }
    #endregion
    #region 7 - WINNING
    private void WinningMecanics()
    {
        //PlayerInputs.CanMove = false;
        //PlayerInputs.CanAttack = false;
        //PlayerInputs.CanJump = false;
        //PlayerInputs.CanSpecial = false;
    }
    #endregion
    #endregion
    private void StopInputsListening()
    {
        PlayerInputs.CanMove = false;
        PlayerInputs.CanAttack = false;
        PlayerInputs.CanJump = false;
        PlayerInputs.CanSpecial = false;
        PlayerInputs.IsListening = false;
    }
    private void StartInputsListening()
    {
        if (!PlayerInputs.IsListening)
        {
            PlayerInputs.CanMove = true;
            PlayerInputs.CanAttack = true;
            PlayerInputs.CanJump = true;
            PlayerInputs.CanSpecial = true;
            PlayerInputs.IsListening = true;
        }
    }
}