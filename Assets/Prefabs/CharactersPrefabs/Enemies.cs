using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private EnemiesIdentities _enemyIdentity;
    [SerializeField] private IntVariable _enemiesCount;
    private CharactersRenderers _renderer;
    private EnemiesCollisions _enemyCollisions;
    private EnemiesIA _enemyIA; public EnemiesIA EnemyIA { get => _enemyIA; }
    private EnemiesStats _enemyStats;
    private EnemiesStateMachine _enemyStateMachine;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _enemyStats = GetComponentInChildren<EnemiesStats>();
        _renderer = GetComponentInChildren<CharactersRenderers>();
        _enemyCollisions = GetComponentInChildren<EnemiesCollisions>();
        _enemyIA = GetComponentInChildren<EnemiesIA>();
        _enemyStateMachine = GetComponent<EnemiesStateMachine>();
    }
    void Start()
    {
        _renderer.SetEnemiesSprites(_enemyIdentity);
    }
    void Update()
    {
        EnemiesMecanics();
    }
    void OnDestroy()
    {
        _enemiesCount.Value--;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE L'IA
    private bool _isMoving; public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    private bool _isFighting; public bool IsFighting { get => _isFighting; }
    private bool _isJumper; public bool IsJumper { get => _isJumper; }
    private bool _isJumping; public bool IsJumping { get => _isJumping; }
    private bool _isHolder; public bool IsHolder { get => _isHolder; }
    private bool _isHolding; public bool IsHolding { get => _isHolding; }
    private bool _isDying; public bool IsDying { get => _isDying; }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE L'IA

    #endregion
    //################################################################################################################################
    #region 0 - BASE
    private bool _isInvulnerable; public bool IsInvulnerable { get => _isInvulnerable; }
    private void EnemiesMecanics()
    {
        DyingMecanics();
        InjuringMecanics();
        MovingMecanics();
        FigthingMecanics();
        JumpingMecanics();
        HoldingMecanics();
    }
    #endregion
    #region 1 - MOVING
    private void MovingMecanics()
    {
        _isMoving = _enemyIA.FireMove;
    }
    #endregion
    #region 2 - FIGHTING
    private int _comboStep; public int ComboStep { get => _comboStep; }
    private float _timeForFighting;
    private float _timeForCombo;
    private float _delayForFighting = 0.2f;
    private float _delayForCombo = 0.55f;
    private void FigthingMecanics()
    {
        UpdateComboSteps();
        if (_enemyIA.FireAttack)
        {
            StartFightingTime();
            StartComboTime();
            _isFighting = true;
            _isInvulnerable = true;
        }
        else if (StopFightingTime())
        {
            _isFighting = false;
            _isInvulnerable = false;
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
        else if (_enemyIA.FireAttack)
        {
            switch (_comboStep)
            {
                case 0:
                    _comboStep = 1;
                    break;
                case 1:
                    _comboStep = 0;
                    break;
                default: break;
            }
        }
    }
    #endregion
    #region 3 - HOLDING
    private void HoldingMecanics()
    {

    }
    #endregion
    #region 4 - JUMPING
    private float _timeForJumping;
    private float _halfJumpingTime;
    private float _delayForJumping = 1.2f;
    private float _halfDelayForJumping = 0.6f;
    private bool _isHoldingJump; public bool IsHoldingJump { get => _isHoldingJump; }
    private int _jumpStep; public int JumpStep { get => _jumpStep; }
    private void JumpingMecanics()
    {
        if (_enemyIA.FireJump)
        {
            StartJumpingTime();
            _isJumping = true;
            _isHoldingJump = _isHolding;
        }
        else if (StopJumpingTime())
        {
            _jumpStep = 0;
            _isJumping = false;
        }
        else if (IsHalfJumpTime())
        {
            _jumpStep = 1;
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
    private bool IsHalfJumpTime()
    {
        return (Time.time >= _halfJumpingTime);
    }
    #endregion
    #region 5 - INJURING
    private float _timeForInjuring = -0.8f;
    private float _delayForInjuring = 0.8f;
    private bool _isInjuring; public bool IsInjuring { get => _isInjuring; }
    private void InjuringMecanics()
    {
        if (_isInvulnerable) _enemyCollisions.IsInjuring = false;
        if (_enemyCollisions.IsInjuring)
        {
            _enemyCollisions.IsInjuring = false;
            _enemyIA.IsLocked = true;
            StartInjuringTime();
            ResolveEnemyHP();
            _enemyStateMachine.OnInjuring();
        }
        else if (StopInjuringTime())
        {
            _isInjuring = false;
            _enemyIA.IsLocked = false;
            _timeForJumping = Time.time + _halfDelayForJumping;
            _halfJumpingTime = Time.time;
            _jumpStep = 1;
        }
    }
    private void StartInjuringTime()
    {
        _isInjuring = true;
        _timeForInjuring = Time.time + _delayForInjuring;
    }
    public bool StopInjuringTime()
    {
        return (Time.time > _timeForInjuring);
    }
    private void ResolveEnemyHP()
    {
        _enemyStats.HitEnemy(true);//todo:basculer à false;
        if (_enemyStats.EnemyIsDead())
        {
            _enemyIA.StopIA();
            _isDying = true;
        }
    }
    #endregion
    #region 6 - DYING
    private bool _isDyingEffect;
    private float _timeForDying;
    private float _delayForDying = 0.8f;
    private void DyingMecanics()
    {
        if (_isDyingEffect)
        {
            SetDeadRenderer();
        } else
        {
            if (!_isDying)
            {
                StartDyingTime();
            }
            else
            {
                if (Time.time > _timeForDying) _isDyingEffect = true;
            }
        }
    }
    private void StartDyingTime()
    {
        _timeForDying = Time.time + _delayForDying;
    }
    private void SetDeadRenderer()
    {
        float timing = Time.time - _timeForDying;
        if (0.25f >= timing || (0.5f < timing && 0.75f > timing))
        {
            _renderer.EnableRenderer();
        }
        else if (1f < timing)
        {
            Destroy(gameObject);
        } else
        {
            _renderer.DisableRenderer();
        }
    }
    #endregion
    //################################################################################################################################
}