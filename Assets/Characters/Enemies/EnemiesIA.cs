using UnityEngine;

public class EnemiesIA : MonoBehaviour
{
    #region API UNITY

    void Update()
    {
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
        _isFighting = false;
        _isJumping = false;
        _isHolding = false;
        _isDying = false;
    } 

    private bool _isBusy; public bool IsBusy { get => _isBusy; }
    private bool _isInjuring;
    private bool _hasReachATarget;
    private float _timerForBeBusy;
    private float _randomizeDelayBeBusy = 0.5f;
    private float _randomizeDelayBeBusyWhileStanding = 1.5f;
    private float _randomizeDelayBeBusyWhileLost = 2.5f;

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
        }
    }

    private bool KeepDoing()
    {
        int rand = Random.Range(1, 100);
        return (rand % 2 == 0);
    }

    private void DoSomething()
    {
        if (!_isInjuring)
        {
            if (_hasReachATarget)
            {
                FightATarget();
            } else
            {
                Move();
            }
        }
    }

    private void FightATarget()
    {
        _isFighting = true;
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