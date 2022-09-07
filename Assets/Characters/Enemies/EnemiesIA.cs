using UnityEngine;

public class EnemiesIA : MonoBehaviour
{
    #region API UNITY

    void Update()
    {
        _iaPressC = false;
        GoToBeBusy();
    }
    #endregion

    private bool _iaPressC; public bool IaPressC { get => _iaPressC; }
    private bool _iaPressX; public bool IaPressX { get => _iaPressX; }
    private bool _isMoving; public bool IsMoving { get => _isMoving; set => _isMoving = value;  }
    private bool _isFighting; public bool IsFighting { get => _isFighting; }
    private bool _isJumping; public bool IsJumping { get => _isJumping; }
    private bool _isHolding; public bool IsHolding { get => _isHolding; }
    private bool _isDying; public bool IsDying { get => _isDying; }

    private void NotBusyAnymore()
    {
        _iaPressC = false;
        _iaPressX = false;
        _isMoving = false;
        _isJumping = false;
        _isHolding = false;
        _isDying = false;
        _isFighting = false;
    } 

    private bool _isBusy; public bool IsBusy { get => _isBusy; }
    private bool _isInjuring;
    private bool _hasReachATarget; public bool HasReachATarget { get => _hasReachATarget; set => _hasReachATarget = value; }
    private float _timerForBeBusy;
    private float _randomizeDelayBeBusy = 0.5f;
    private float _randomizeDelayBeBusyWhileStanding = 1.5f;
    private float _randomizeDelayBeBusyWhileLost = 2.5f;
    private float _timerForFighting;
    private float _delayFighting = 1f;
    [SerializeField] private CircleCollider2D _hitsCollider;

    [SerializeField] EnemiesMovements _enemyMovements;
    private bool _process; public bool Process { get => _process; }

    private void GoToBeBusy()
    {
        if (!_isBusy)
        {
            _isBusy = true;
            DoSomething();
        }
        else if(Time.time > _timerForBeBusy)
        {
            if (KeepDoing())
            {
                _timerForBeBusy = Time.time + _randomizeDelayBeBusy;
            } else
            {
                _isBusy = false;
                NotBusyAnymore();
            }
        } else
        {
            _isFighting = !StopFightingTime();
            if (_isFighting)
            {
                _hitsCollider.enabled = true;
                _timeForCombo = Time.time + _delayForCombo;
                _timerForBeBusy = Time.time + 0.2f;
            } else
            {
                _hitsCollider.enabled = false;
            }
        }
        if (Time.time > _timeForCombo) _comboStep = 0;
        if (_iaPressC && _timeForCombo > Time.time)
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

    private bool KeepDoing()
    {
        int rand = Random.Range(1, 100);
        return (rand % 2 == 0);
    }

    private float _timeForPreventDoubleAttack;
    private float _delayForPreventDoubleAttack = 0.25f;
    private float _timeForFighting;
    private float _delayForFighting = 0.2f;
    private float _timeForCombo;
    private float _delayForCombo = 0.3f;
    private int _comboStep; public int ComboStep { get => _comboStep; }
    public void StartFightingTime()
    {
        _timeForFighting = Time.time + _delayForFighting;
    }
    public bool StopFightingTime()
    {
        return (Time.time > _timeForFighting);
    }
    private void DoSomething()
    {
        if (!_isInjuring)
        {
            if (_hasReachATarget && Time.time > _timeForPreventDoubleAttack)
            {
                _timeForPreventDoubleAttack = Time.time + _delayForPreventDoubleAttack;
                _iaPressC = true;
                _isFighting = true;
                StartFightingTime();
                _enemyMovements.DontMove();
            }
            else
            {
                Move();
            }
        }
    }

    private void FightATarget(int c)
    {
        //_isFighting = true;
        Debug.Log("I FIGHT : " + c);
    }

    private void Move()
    {
        int _percent = Random.Range(1, 100);
        if (_percent % 2 != 0)
        {
            if (_percent <= 2)
            {
                _enemyMovements.MoveBackward();
                _timerForBeBusy = Time.time + _randomizeDelayBeBusyWhileLost;
            }
            else if (_percent > 3 && _percent <= 6)
            {
                _enemyMovements.DontMove();
                _timerForBeBusy = Time.time + _randomizeDelayBeBusyWhileStanding;
            }
            else if (_percent >= 98)
            {
                _enemyMovements.MoveForward();
                _timerForBeBusy = Time.time + _randomizeDelayBeBusyWhileLost;
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

}