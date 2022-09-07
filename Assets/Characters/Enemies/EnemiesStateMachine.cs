using UnityEngine;

public class EnemiesStateMachine : MonoBehaviour
{
    private EnemiesStates _currentEnemyState;

    private void OnStateEnter(EnemiesStates enemiesStates) {
        switch (enemiesStates)
        {
            case EnemiesStates.PENDING:
                OnPendingEnter();
                break;
            case EnemiesStates.WALKING:
                OnWalkingEnter();
                break;
            case EnemiesStates.ATTACKING:
                OnAttackingEnter();
                break;
            case EnemiesStates.HOLDING:
                break;
            case EnemiesStates.JUMPING:
                break;
            case EnemiesStates.INJURING:
                break;
            case EnemiesStates.DYING:
                break;
            default:
                break;
        }
    }

    private void OnStateExit(EnemiesStates enemiesStates)
    {
        switch (enemiesStates)
        {
            case EnemiesStates.PENDING:
                OnPendingExit();
                break;
            case EnemiesStates.WALKING:
                OnWalkingExit();
                break;
            case EnemiesStates.ATTACKING:
                OnAttackingExit();
                break;
            case EnemiesStates.HOLDING:
                break;
            case EnemiesStates.JUMPING:
                break;
            case EnemiesStates.INJURING:
                break;
            case EnemiesStates.DYING:
                break;
            default:
                break;
        }
    }

    private void OnStateUpdate(EnemiesStates enemiesStates)
    {
        switch (enemiesStates)
        {
            case EnemiesStates.PENDING:
                OnPendingUpdate();
                break;
            case EnemiesStates.WALKING:
                OnWalkingUpdate();
                break;
            case EnemiesStates.ATTACKING:
                OnAttackingUpdate();
                break;
            case EnemiesStates.HOLDING:
                break;
            case EnemiesStates.JUMPING:
                break;
            case EnemiesStates.INJURING:
                break;
            case EnemiesStates.DYING:
                break;
            default:
                break;
        }
    }

    private void TransitionToState(EnemiesStates enemiesStates)
    {
        OnStateExit(_currentEnemyState);
        _currentEnemyState = enemiesStates;
        OnStateEnter(enemiesStates);
    }

    #region UNITY API
    [SerializeField] EnemiesIA _enemyIA;
    [SerializeField] Animator _animator;

    void Start()
    {
        TransitionToState(EnemiesStates.PENDING);
    }
    void Update()
    {
        FakeInputs();
        //_animator.SetBool("IsWalking", _enemyIA.IsWalking);
        OnStateUpdate(_currentEnemyState);
    }

    private void FakeInputs()
    {
        _animator.SetBool("iaPressC", _enemyIA.IaPressC);
        _animator.SetBool("iaPressX", _enemyIA.IaPressX);
        _animator.SetBool("isMoving", _enemyIA.IsMoving);
    }


    #endregion

    #region ENTER
    private void OnPendingEnter()
    {
    }
    private void OnWalkingEnter()
    {
    }
    private void OnAttackingEnter()
    {
        _animator.SetBool("isFigthing", true);
    }
    #endregion
    #region
    private void OnPendingUpdate()
    {
        if (_enemyIA.IaPressC)
        {
            TransitionToState(EnemiesStates.ATTACKING);
        } else if (_enemyIA.IsMoving)
        {
            TransitionToState(EnemiesStates.WALKING);
        }
    }
    private void OnWalkingUpdate()
    {
        if(!_enemyIA.IsMoving)
        {
            TransitionToState(EnemiesStates.PENDING);
        } else if (_enemyIA.IaPressC)
        {
            TransitionToState(EnemiesStates.ATTACKING);
        }
    }
    private void OnAttackingUpdate()
    {
        _animator.SetFloat("comboStep", _enemyIA.ComboStep);
        if (!_enemyIA.IsFighting)
        {
            if (_enemyIA.IsMoving)
            {
                TransitionToState(EnemiesStates.WALKING);
            } else
            {
                TransitionToState(EnemiesStates.PENDING);
            }
        }
    }
    #endregion
    #region EXIT
    private void OnPendingExit()
    {
    }
    private void OnWalkingExit()
    {
    }
    private void OnAttackingExit()
    {
        _animator.SetBool("isFigthing", false);
    }
    #endregion
}