using System;
using UnityEngine;

public class PlayersStateMachine : MonoBehaviour
{
    private PlayersStates _currentPlayerState;
    private Players _player;
    private Animator _animator;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _player = GetComponent<Players>();
        _animator = GetComponent<Animator>();        
    }
    void Start()
    {
        TransitionToState(PlayersStates.PENDING);
    }
    void Update()
    {
        OnStateUpdate(_currentPlayerState);
    }
    #endregion
    //################################################################################################################################
    #region INPUTS EVENTS
    #endregion
    //################################################################################################################################
    #region TRIGGERS EVENTS
    /// <summary>
    /// Déclenche le trigger "OnWinning" de l'AnimatorController.
    /// </summary>
    private void OnWinning()
    {
        _animator.SetTrigger("OnWinning");
    }
    /// <summary>
    /// Déclenche le trigger "OnRaging" de l'AnimatorController.
    /// </summary>
    private void OnRaging()
    {
        _animator.SetTrigger("OnRaging");
    }
    /// <summary>
    /// Déclenche le trigger "OnInjuring" de l'AnimatorController.
    /// </summary>
    private void OnInjuring()
    {
        _animator.SetTrigger("OnInjuring");
    }
    /// <summary>
    /// Déclenche le trigger "OnJumping" de l'AnimatorController.
    /// </summary>
    private void OnJumping()
    {
        _animator.SetTrigger("OnJumping");
    }
    /// <summary>
    /// Déclenche le trigger "OnLanding" de l'AnimatorController.
    /// </summary>
    private void OnLanding()
    {
        _animator.SetTrigger("OnLanding");
    }
    /// <summary>
    /// Déclenche le trigger "OnHolding" de l'AnimatorController.
    /// </summary>
    private void OnHolding()
    {
        _animator.SetTrigger("OnHolding");
    }
    #endregion
    //################################################################################################################################
    #region STATEMACHINE
    /// <summary>
    /// PLAYERS STATE MACHINE - ENTER
    /// </summary>
    /// <param name="playerState"></param>
    private void OnStateEnter(PlayersStates playerState)
    {
        switch (playerState)
        {
            case PlayersStates.PENDING:
                OnPendingEnter();
                break;
            case PlayersStates.WALKING:
                OnWalkingEnter();
                break;
            case PlayersStates.SPRINTING:
                OnSprintingEnter();
                break;
            case PlayersStates.ATTACKING:
                OnAttackingEnter();
                break;
            case PlayersStates.HOLDING:
                OnHoldingEnter();
                break;
            case PlayersStates.JUMPING:
                OnJumpingEnter();
                break;
            case PlayersStates.INJURING:
                OnInjuringEnter();
                break;
            case PlayersStates.RAGING:
                OnRagingEnter();
                break;
            case PlayersStates.WINNING:
                OnWinningEnter();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PLAYERS STATE MACHINE - EXIT
    /// </summary>
    private void OnStateExit(PlayersStates playerState)
    {
        switch (playerState)
        {
            case PlayersStates.PENDING:
                OnPendingExit();
                break;
            case PlayersStates.WALKING:
                OnWalkingExit();
                break;
            case PlayersStates.SPRINTING:
                OnSprintingExit();
                break;
            case PlayersStates.ATTACKING:
                OnAttackingExit();
                break;
            case PlayersStates.HOLDING:
                OnHoldingExit();
                break;
            case PlayersStates.JUMPING:
                OnJumpingExit();
                break;
            case PlayersStates.INJURING:
                OnInjuringExit();
                break;
            case PlayersStates.RAGING:
                OnRagingExit();
                break;
            case PlayersStates.WINNING:
                OnWinningExit();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PLAYERS STATE MACHINE - UPDATE
    /// </summary>
    private void OnStateUpdate(PlayersStates playerState)
    {
        switch (playerState)
        {
            case PlayersStates.PENDING:
                OnPendingUpdate();
                break;
            case PlayersStates.WALKING:
                OnWalkingUpdate();
                break;
            case PlayersStates.SPRINTING:
                OnSprintingUpdate();
                break;
            case PlayersStates.ATTACKING:
                OnAttackingUpdate();
                break;
            case PlayersStates.HOLDING:
                OnHoldingUpdate();
                break;
            case PlayersStates.JUMPING:
                OnJumpingUpdate();
                break;
            case PlayersStates.INJURING:
                OnInjuringUpdate();
                break;
            case PlayersStates.RAGING:
                OnRagingUpdate();
                break;
            case PlayersStates.WINNING:
                OnWinningUpdate();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// PLAYERS STATE MACHINE - TRANSITIONS
    /// </summary>
    private void TransitionToState(PlayersStates playerState)
    {
        OnStateExit(_currentPlayerState);
        _currentPlayerState = playerState;
        OnStateEnter(playerState);
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS SANS TRAITEMENT
    #region 1 - PENDING
    private void OnPendingEnter()
    {
        //Do nothing
    }
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
    #region 3 - SPRINTING

    #endregion
    #region 4 - ATTACKING

    #endregion
    #region 5 - HOLDING

    #endregion
    #region 6 - JUMPING

    #endregion
    #region 7 - INJURING

    #endregion
    #region 8 - RAGING

    #endregion
    #region 9 - WINNING
    private void OnWinningExit()
    {
        //Do nothing
    }
    #endregion
    #endregion
    //################################################################################################################################
    #region 1 - PENDING
    private void OnPendingUpdate()
    {
        //todo : 
    }
    #endregion
    #region 2 - WALKING
    private void OnWalkingUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 3 - SPRINTING
    private void OnSprintingEnter()
    {
        throw new NotImplementedException();
    }
    private void OnSprintingExit()
    {
        throw new NotImplementedException();
    }
    private void OnSprintingUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 4 - ATTACKING
    private void OnAttackingEnter()
    {
        throw new NotImplementedException();
    }
    private void OnAttackingExit()
    {
        throw new NotImplementedException();
    }
    private void OnAttackingUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 5 - HOLDING
    private void OnHoldingEnter()
    {
        throw new NotImplementedException();
    }
    private void OnHoldingExit()
    {
        throw new NotImplementedException();
    }
    private void OnHoldingUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 6 - JUMPING
    private void OnJumpingEnter()
    {
        throw new NotImplementedException();
    }
    private void OnJumpingExit()
    {
        throw new NotImplementedException();
    }
    private void OnJumpingUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 7 - INJURING
    private void OnInjuringEnter()
    {
        throw new NotImplementedException();
    }
    private void OnInjuringExit()
    {
        throw new NotImplementedException();
    }
    private void OnInjuringUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 8 - RAGING
    private void OnRagingEnter()
    {
        throw new NotImplementedException();
    }
    private void OnRagingExit()
    {
        throw new NotImplementedException();
    }
    private void OnRagingUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    #region 9 - WINNING
    private void OnWinningEnter()
    {
        throw new NotImplementedException();
    }
    private void OnWinningUpdate()
    {
        throw new NotImplementedException();
    }
    #endregion
    //################################################################################################################################
}