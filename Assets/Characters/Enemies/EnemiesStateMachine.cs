using UnityEngine;

public class EnemiesStateMachine : MonoBehaviour
{
    private EnemiesStates _currentEnemyState;

    private void OnStateEnter(EnemiesStates enemiesStates) {
        switch (enemiesStates)
        {
            case EnemiesStates.PENDING:
                break;
            case EnemiesStates.WALKING:
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
                break;
            case EnemiesStates.WALKING:
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
                break;
            case EnemiesStates.WALKING:
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
        _animator.SetBool("IsWalking", _enemyIA.IsWalking);
        OnStateUpdate(_currentEnemyState);
    }
    #endregion
}