using UnityEngine;

public class EnemiesStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Enemies _enemy;
    private EnemiesStates _currentEnemyState;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _enemy = GetComponent<Enemies>();
    }
    void Start()
    {
        TransitionToState(EnemiesStates.PENDING);
    }
    void Update()
    {
        FakeInputs();
        OnStateUpdate(_currentEnemyState);
    }
    #endregion
    //################################################################################################################################
    #region STATEMACHINE
    /// <summary>
    /// PLAYERS STATE MACHINE - ENTER
    /// </summary>
    /// <param name="playerState"></param>
    private void OnStateEnter(EnemiesStates enemiesStates)
    {
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
                OnHoldingEnter();
                break;
            case EnemiesStates.JUMPING:
                OnJumpingEnter();
                break;
            case EnemiesStates.INJURING:
                OnInjuringEnter();
                break;
            case EnemiesStates.DYING:
                OnDyingEnter();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PLAYERS STATE MACHINE - EXIT
    /// </summary>
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
                OnHoldingExit();
                break;
            case EnemiesStates.JUMPING:
                OnJumpingExit();
                break;
            case EnemiesStates.INJURING:
                OnInjuringExit();
                break;
            case EnemiesStates.DYING:
                OnDyingExit();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PLAYERS STATE MACHINE - UPDATE
    /// </summary>
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
                OnHoldingUpdate();
                break;
            case EnemiesStates.JUMPING:
                OnJumpingUpdate();
                break;
            case EnemiesStates.INJURING:
                OnInjuringUpdate();
                break;
            case EnemiesStates.DYING:
                OnDyingUpdate();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PLAYERS STATE MACHINE - TRANSITIONS
    /// </summary>
    private void TransitionToState(EnemiesStates enemiesStates)
    {
        OnStateExit(_currentEnemyState);
        _currentEnemyState = enemiesStates;
        OnStateEnter(enemiesStates);
    }
    #endregion
    //################################################################################################################################
    #region INPUTS EVENTS
    private void FakeInputs()
    {
        _animator.SetBool("iaPressC", _enemy.EnemyIA.FireAttack);
        _animator.SetBool("iaPressX", _enemy.EnemyIA.FireJump);
        _animator.SetBool("isMoving", _enemy.IsMoving);
    }
    #endregion
    //################################################################################################################################
    #region TRIGGERS EVENTS
    /// <summary>
    /// Déclenche le trigger "OnInjuring" de l'AnimatorController.
    /// </summary>
    public void OnInjuring()
    {
        _animator.SetTrigger("OnInjuring");
        TransitionToState(EnemiesStates.INJURING);
    }
    /// <summary>
    /// Déclenche le trigger "OnJumping" de l'AnimatorController.
    /// Assigne le parametre et "isJumping" à true si il la condition Players.IsHolding est vérifiée.
    /// </summary>
    private void OnJumping()
    {
        _animator.SetTrigger("OnJumping");
        TransitionToState(EnemiesStates.JUMPING);
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS SANS TRAITEMENT
    #region 1 - PENDING
    private void OnPendingExit()
    {
        //Do nothing
    }
    #endregion
    #region 2 - WALKING
    private void OnWalkingEnter()
    {
        //Do nothing
    }
    private void OnWalkingExit()
    {
        //Do nothing
    }
    #endregion
    #region 4 - ATTACKING
    #endregion
    #region 5 - HOLDING
    #endregion
    #region 6 - JUMPING
    #endregion
    #region 7 - INJURING
    private void OnInjuringEnter()
    {
        //Do nothing
    }
    private void OnInjuringExit()
    {
        _enemy.EnemyIA.ResetIA();
    }
    #endregion
    #region 8 - DYING
    private void OnDyingEnter()
    {
        //Do nothing
    }
    private void OnDyingUpdate()
    {
        //Do nothing
    }
    private void OnDyingExit()
    {
        //Do nothing
    }
    #endregion
    #endregion
    //################################################################################################################################
    #region 1 - PENDING
    private void OnPendingEnter()
    {
        
    }
    /// <summary>
    /// Transitions possibles
    /// -> Walking
    /// -> Fighting - IA Inputs
    /// </summary>
    private void OnPendingUpdate()
    {
        if (_enemy.EnemyIA.FireAttack)
        {
            TransitionToState(EnemiesStates.ATTACKING);
        }
        else if (_enemy.IsMoving)
        {
            TransitionToState(EnemiesStates.WALKING);
        }
    }
    #endregion
    //################################################################################################################################
    #region 2 - WALKING
    /// <summary>
    /// Transitions possibles
    /// -> Pending
    /// -> Fighting - IA Inputs
    /// </summary>
    private void OnWalkingUpdate()
    {
        if (!_enemy.IsMoving)
        {
            TransitionToState(EnemiesStates.PENDING);
        }
        else if (_enemy.EnemyIA.FireAttack)
        {
            TransitionToState(EnemiesStates.ATTACKING);
        }
    }
    #endregion
    //################################################################################################################################
    #region 3 - ATTACKING
    private void OnAttackingEnter()
    {
        _animator.SetBool("isFigthing", true);
    }
    /// <summary>
    /// Transitions possibles
    /// -> Pending
    /// -> Walking
    /// </summary>
    private void OnAttackingUpdate()
    {
        _animator.SetFloat("comboStep", _enemy.ComboStep);
        if (!_enemy.IsFighting)
        {
            if (_enemy.IsMoving)
            {
                TransitionToState(EnemiesStates.WALKING);
            }
            else
            {
                TransitionToState(EnemiesStates.PENDING);
            }
        }
    }
    private void OnAttackingExit()
    {
        _animator.SetBool("isFigthing", false);
    }
    #endregion
    //################################################################################################################################
    #region 4 - HOLDING
    /// <summary>
    /// Assigne le parametre "isHolding" de l'animator à "true".
    /// </summary>
    private void OnHoldingEnter()
    {

    }
    /// <summary>
    /// Assigne le parametre "isHolding" de l'animator à "false" si Players.Injuring vaut "false".
    /// </summary>
    private void OnHoldingExit()
    {

    }
    /// <summary>
    /// Transitions possibles
    /// -> Pending
    /// -> Jumping
    /// </summary>
    private void OnHoldingUpdate()
    {

    }
    #endregion
    //################################################################################################################################
    #region 5 - JUMPING
    /// <summary>
    /// Assigne le parametre "isJumping" de l'animator à "true".
    /// </summary>
    private void OnJumpingEnter()
    {
        _animator.SetBool("isJumping", true);
    }
    /// <summary>
    /// Assigne le parametre et "jumpStep" à 0 et
    /// le parametre "isJumping" de l'animator à "false" si Players.Injuring vaut "false".
    /// </summary>
    private void OnJumpingExit()
    {
        _animator.SetInteger("jumpStep", 0);
        _animator.SetBool("isJumping", false);
        _enemy.EnemyIA.ResetIA();
    }
    /// <summary>
    /// Transitions possibles
    /// -> Pending
    /// </summary>
    private void OnJumpingUpdate()
    {
        _animator.SetInteger("jumpStep", _enemy.JumpStep);
        if (_enemy.StopJumpingTime())
        {
            TransitionToState(EnemiesStates.PENDING);
        }
    }
    #endregion
    #region 7 - INJURING
    /// <summary>
    /// Transitions possibles
    /// -> Pending
    /// -> Holding
    /// -> Dying
    /// </summary>
    private void OnInjuringUpdate()
    {
        _animator.SetBool("isDying", _enemy.IsDying);
        if (_enemy.StopInjuringTime())
        {
            if (_enemy.IsDying)
            {
                TransitionToState(EnemiesStates.DYING);
            }
            else if (_enemy.IsHolding)
            {
                TransitionToState(EnemiesStates.HOLDING);
            }
            else
            {
                TransitionToState(EnemiesStates.PENDING);
            }
        }
    }
    #endregion
    #region 8 - DYING
    #endregion
}