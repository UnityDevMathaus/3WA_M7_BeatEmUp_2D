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
    private SpriteRenderer _playerSpriteRenderer;

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
        _playerSpriteRenderer = _renderer.GetComponent<SpriteRenderer>();
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
        FigthingMecanics();
        JumpingMecanics();      
        HoldingMecanics();

    }
    #endregion
    #region 1 - MOVING
    private float _timeForSprinting;
    private float _timeForChangeDirection;
    private float _delayForSprinting = 0.2f;
    private float _delayForChangeDirection = 0.1f;
    private KeyCode _lastDirectionKey = KeyCode.None;
    private void MovingMecanics()
    {
        _isMoving = PlayerInputs.FireMove || PlayerInputs.WhileMove;
        if (_isSprinting)
        {
            UpdateTimeForChangeDirection();
            StopSprintingWhenStopMoving();
        } else
        {
            CheckSprinting();
        }
    }
    private void UpdateTimeForChangeDirection()
    {
        if (_isMoving) _timeForChangeDirection = Time.time + _delayForChangeDirection;
    }
    private void StopSprintingWhenStopMoving()
    {
        if (!_isMoving && Time.time > _timeForChangeDirection) _isSprinting = false;
    }
    private void CheckSprinting()
    {
        if (_lastDirectionKey == KeyCode.None)
        {
            if (PlayerInputs.FireMove)
            {
                _lastDirectionKey = PlayerInputs.LastDirectionKey;
                _timeForSprinting = Time.time + _delayForSprinting;
            }
        }
        else
        {
            if (Time.time > _timeForSprinting) _lastDirectionKey = KeyCode.None;
            else if (PlayerInputs.FireMove && !_isSprinting)
            {
                _isSprinting = _lastDirectionKey == PlayerInputs.LastDirectionKey;
            }
        }
    }
    #endregion
    #region 2 - FIGHTING
    private int _comboStep; public int ComboStep { get => _comboStep; }
    private float _timeForFighting;
    private float _timeForCombo;
    private float _delayForFighting = 0.3f;
    private float _delayForCombo = 0.6f;
    private void FigthingMecanics()
    {
        PlayerInputs.CanAttack = !_isFighting;
        UpdateComboSteps();
        if (PlayerInputs.FireAttack)
        {
            StartFightingTime();
            StartComboTime();            
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
    private void StartComboTime()
    {
        _timeForCombo = Time.time + _delayForCombo;
    }
    private void UpdateComboSteps()
    {
        if (Time.time > _timeForCombo) _comboStep = 0;
        else if(PlayerInputs.FireAttack)
        {
            switch (_comboStep)
            {
                case 0:
                    _comboStep = 1;
                    break;
                case 1:
                    _comboStep = 2;
                    break;
                case 2:
                    _comboStep = 3;
                    break;
                case 3:
                    _comboStep = 0;
                    break;
                default: break;
            }
        }
    }
    #endregion
    #region 3 - HOLDING
    private bool _canHold; public bool CanHold { get => _canHold; set => _canHold = value; }
    private void HoldingMecanics()
    {
        if (PlayerInputs.FireAttack)
        {
            if (!_isHolding)
            {
                _isHolding = _canHold;
            } else
            {
                _isHolding = false;
            }
        }
    }
    #endregion
    #region 4 - JUMPING
    private float _timeForJumping;
    private float _halfJumpingTime;
    private float _delayForJumping = 1.2f;
    private float _halfDelayForJumping = 0.6f;
    private float _delayForGroundPound = 0.4f;
    private bool _isHoldingJump; public bool IsHoldingJump { get => _isHoldingJump; set => _isHoldingJump = value; }
    private int _jumpStep; public int JumpStep { get => _jumpStep; set => _jumpStep = value; }
    private void JumpingMecanics()
    {
        PlayerInputs.CanJump = !_isJumping;
        if (PlayerInputs.FireJump)
        {
            StartJumpingTime();
            _isJumping = true;
            _isHoldingJump = _isHolding;
        }
        else if (StopJumpingTime())
        {
            _jumpStep = 0;
            _isJumping = false;
        } else if (IsHalfJumpTime())
        {
            _jumpStep = 1;
        } else
        {
            Groundpound();
        }
    }
    private void StartJumpingTime()
    {
        _timeForJumping = Time.time + _delayForJumping;
        _halfJumpingTime = Time.time + _halfDelayForJumping;
    }
    public bool StopJumpingTime()
    {
        return (Time.time > _timeForJumping);
    }
    public void ForceStopJumpingTime()
    {
        _timeForJumping = 0f;
    }
    private bool IsHalfJumpTime()
    {
        return (Time.time >= _halfJumpingTime);
    }
    private void Groundpound()
    {
        if (PlayerInputs.FireAttack && !_isMoving)
        {
            _timeForJumping = Time.time + _delayForGroundPound;
            Debug.Log("GP");
        }
        
    }
    #endregion
    #region 5 - INJURING
    private float _timeForInjuring = -0.8f;
    private float _delayForInjuring = 0.8f;
    private bool _isInjuring; public bool IsInjuring { get => _isInjuring; }
    private void InjuringMecanics()
    {
        if (_playerCollisions.IsInjuring)
        {
            _playerCollisions.IsInjuring = false;
            StopInputsListening();
            StartInjuringTime();                     
            ResolvePlayerHP();
            _playerStateMachine.OnInjuring();
        } else if (StopInjuringTime())
        {
            _isInjuring = false;
            StartInputsListening();
        }
        if (!_isDying) SetInjureRenderer();
    }
    private void StartInjuringTime()
    {
        _isInjuring = true;
        _timeForInjuring = Time.time;
        _timeForJumping = Time.time;
        _halfJumpingTime = Time.time + _halfDelayForJumping;
        _jumpStep = 1;
    }
    public bool StopInjuringTime()
    {       
        return (Time.time > _timeForInjuring + _delayForInjuring);
    }
    private void ResolvePlayerHP()
    {
        _playerHP.Value--;
        if (_playerHP.Value == 0) _isDying = true;
    }
    private void SetInjureRenderer()
    {
        float timing = Time.time - _timeForInjuring;
        if (0.2f >= timing || (0.4f < timing && 0.6f > timing))
        {
            _playerSpriteRenderer.color = Color.red;
        }
        else
        {
            _playerSpriteRenderer.color = Color.white;
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
    public bool StopRagingTime()
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
        if (!PlayerInputs.IsListening && !_isDying)
        {
            PlayerInputs.CanMove = true;
            PlayerInputs.CanAttack = true;
            PlayerInputs.CanJump = true;
            PlayerInputs.CanSpecial = true;
            PlayerInputs.IsListening = true;
        }
    }
}