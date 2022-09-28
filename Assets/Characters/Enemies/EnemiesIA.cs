using UnityEngine;

public class EnemiesIA : MonoBehaviour
{
    [SerializeField] EnemiesMovements _enemyMovements;
    private EnemiesIdentities _enemyIdentity;
    //################################################################################################################################
    #region API UNITY
    void Start()
    {
        _isAlive = true;
    }
    void Update()
    {
        IAMecanics();
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE L'IA
    private bool _isAlive; public bool IsAlive { get => _isAlive; }
    private bool _isLocked; public bool IsLocked { get => _isLocked; set => _isLocked = value; }
    private void IAMecanics()
    {
        if (_isLocked)
        {
            ResetIA();
        }
        else
        {
            _fireAttack = false;
            _fireJump = false;
            if (_isAlive)
            {
                GoToBeBusy();
            }
        }
    }
    public void StopIA()
    {
        _isAlive = false;
    }
    public void ResetIA()
    {
        _hasReachATarget = false;
        _isBusy = false;
        NotBusyAnymore();
        _timerForBeBusy = Time.time + _delayForBeBusy;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE D'ACTIONS
    private bool _fireJump; public bool FireJump { get => _fireJump; }
    private bool _fireAttack; public bool FireAttack { get => _fireAttack; }
    private bool _fireMove; public bool FireMove { get => _fireMove; }
    private bool _hasReachATarget; public bool HasReachATarget { get => _hasReachATarget; set => _hasReachATarget = value; }
    private bool _isBusy; public bool IsBusy { get => _isBusy; }
    private float _timerForBeBusy;
    private float _delayForBeBusy = 0.5f;
    private float _delayWhileStanding = 1.5f;
    private float _delayWhileLost = 2.5f;
    private float _timeForPreventDoubleAttack;
    private float _delayForPreventDoubleAttack = 0.2f;
    private void GoToBeBusy()
    {
        if (!_isBusy)
        {
            _isBusy = true;
            DoSomething();
        }
        else if (Time.time > _timerForBeBusy)
        {
            if (KeepDoing())
            {
                _timerForBeBusy = Time.time + _delayForBeBusy;
            }
            else
            {
                _isBusy = false;
                NotBusyAnymore();
            }
        }
    }
    private void DoSomething()
    {
        if (_hasReachATarget)
        {
            IdentifyEnemies();
            _enemyMovements.DontMove();
        }
        else
        {
            Move();
        }
    }
    private bool KeepDoing()
    {
        int rand = Random.Range(1, 100);
        return (rand % 2 == 0);
    }
    private void NotBusyAnymore()
    {
        _fireJump = false;
        _fireAttack = false;
        _fireMove = false;
    }
    private void IdentifyEnemies()
    {
        switch (_enemyIdentity)
        {
            case EnemiesIdentities.RED:
                Fight();
                break;
            case EnemiesIdentities.GREEN:
                break;
            case EnemiesIdentities.BLUE:
                break;
            default:
                break;
        }
    }
    private void Fight()
    {
        if (Time.time > _timeForPreventDoubleAttack)
        {
            _timeForPreventDoubleAttack = Time.time + _delayForPreventDoubleAttack;
            _fireAttack = true;
            _timerForBeBusy = Time.time;
        }
    }
    private void Move()
    {
        _fireMove = true;
        int _percent = Random.Range(1, 100);
        if (_percent % 2 != 0)
        {
            if (_percent <= 2)
            {
                _enemyMovements.MoveBackward();
                _timerForBeBusy = Time.time + _delayWhileLost;
            }
            else if (_percent > 3 && _percent <= 6)
            {
                _fireMove = false;
                _enemyMovements.DontMove();
                _timerForBeBusy = Time.time + _delayWhileStanding;
            }
            else if (_percent >= 98)
            {
                _enemyMovements.MoveForward();
                _timerForBeBusy = Time.time + _delayWhileLost;
            }
            else
            {
                _enemyMovements.MoveToTarget();
            }
        }
        else
        {
            _enemyMovements.MoveToTarget();
        }
    }
    #endregion
    //################################################################################################################################
}