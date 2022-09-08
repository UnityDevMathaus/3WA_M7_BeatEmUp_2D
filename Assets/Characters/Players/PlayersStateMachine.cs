using System;
using UnityEngine;

public class PlayersStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Players _player;
    private PlayersStates _currentPlayerState;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _player = GetComponent<Players>();       
    }
    void Start()
    {
        TransitionToState(PlayersStates.PENDING);
    }
    void Update()
    {
        PlayerFireInputs();
        OnStateUpdate(_currentPlayerState);
    }
    #endregion
    //################################################################################################################################
    #region INPUTS EVENTS
    /// <summary>
    /// Assigne les paramètres d'inputs du PlayersAnimatorController.
    /// </summary>
    private void PlayerFireInputs()
    {
        _animator.SetBool("pressX", _player.PlayerInputs.FireJump);
        _animator.SetBool("pressC", _player.PlayerInputs.FireAttack);
        _animator.SetBool("isMoving", _player.IsMoving);
    }
    private void PlayerFireJump()
    {
        if (_player.PlayerInputs.FireJump && !_player.IsJumping)
        {
            OnJumping();
            TransitionToState(PlayersStates.JUMPING);
        }
    }
    private void PlayerFireAttack()
    {
        _animator.SetBool("isFighting", _player.IsFighting);
        _animator.SetFloat("comboStep", _player.ComboStep);
        if (_player.PlayerInputs.FireAttack)
        {
            if (!_player.IsJumping && !_player.IsHolding)
            {
                TransitionToState(PlayersStates.ATTACKING);
            }
        }
    }
    #endregion
    //################################################################################################################################
    #region TRIGGERS EVENTS
    /// <summary>
    /// Déclenche le trigger "OnWinning" de l'AnimatorController.
    /// </summary>
    private void OnWinning()
    {
        _animator.SetTrigger("OnWinning");
        TransitionToState(PlayersStates.WINNING);
    }
    /// <summary>
    /// Déclenche le trigger "OnRaging" de l'AnimatorController.
    /// </summary>
    public void OnRaging()
    {
        _animator.SetTrigger("OnRaging");
        TransitionToState(PlayersStates.RAGING);
    }
    /// <summary>
    /// Déclenche le trigger "OnInjuring" de l'AnimatorController.
    /// </summary>
    public void OnInjuring()
    {
        _animator.SetTrigger("OnInjuring");
        TransitionToState(PlayersStates.INJURING);
    }
    /// <summary>
    /// Déclenche le trigger "OnJumping" de l'AnimatorController.
    /// </summary>
    private void OnJumping()
    {
        _animator.SetTrigger("OnJumping");
        TransitionToState(PlayersStates.JUMPING);
    }
    /// <summary>
    /// Déclenche le trigger "OnLanding" de l'AnimatorController.
    /// </summary>
    private void OnLanding()
    {
        _animator.SetTrigger("OnLanding");
        if (_player.IsFighting)
        {
            TransitionToState(PlayersStates.ATTACKING);
        } else
        {
            TransitionToState(PlayersStates.PENDING);
        }        
    }
    /// <summary>
    /// Déclenche le trigger "OnHolding" de l'AnimatorController.
    /// </summary>
    private void OnHolding()
    {
        _animator.SetTrigger("OnHolding");
        TransitionToState(PlayersStates.HOLDING);
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
    private void OnAttackingEnter()
    {
        //Do nothing
    }
    private void OnAttackingExit()
    {
        //Do nothing
    }
    #endregion
    #region 5 - HOLDING

    #endregion
    #region 6 - JUMPING

    #endregion
    #region 7 - INJURING
    private void OnInjuringExit()
    {
        //Do nothing
    }
    #endregion
    #region 8 - RAGING
    private void OnRagingEnter()
    {
        //Do nothing
    }
    private void OnRagingExit()
    {
        //Do nothing  
    }
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
        PlayerFireJump();
        PlayerFireAttack();
        if (_player.IsSprinting)
        {
            TransitionToState(PlayersStates.SPRINTING);
        }
        else if (_player.IsMoving)
        {
            TransitionToState(PlayersStates.WALKING);
        }
    }
    #endregion
    #region 2 - WALKING
    private void OnWalkingUpdate()
    {
        PlayerFireJump();
        PlayerFireAttack();
        if (!_player.IsMoving)
        {
            TransitionToState(PlayersStates.PENDING);
        }
    }
    #endregion
    #region 3 - SPRINTING
    private void OnSprintingEnter()
    {
        _animator.SetBool("isSprinting", true);
    }
    private void OnSprintingExit()
    {
        _animator.SetBool("isSprinting", false);
    }
    private void OnSprintingUpdate()
    {
        PlayerFireJump();
        PlayerFireAttack();
        if (!_player.IsMoving)
        {
            TransitionToState(PlayersStates.PENDING);
        }
    }
    #endregion
    #region 4 - ATTACKING
    private void OnAttackingUpdate()
    {
        PlayerFireJump();
        _animator.SetBool("isFighting", _player.IsFighting);
        _animator.SetFloat("comboStep", _player.ComboStep);
        if (!_player.IsFighting)
        {
            if (_player.IsMoving)
            {
                TransitionToState(PlayersStates.WALKING);
            }
            else
            {
                TransitionToState(PlayersStates.PENDING);
            }
        }
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
        _animator.SetBool("isJumping", true);
    }
    private void OnJumpingExit()
    {
        _animator.SetInteger("jumpStep", 0);
        _animator.SetBool("isJumping", false);
    }
    private void OnJumpingUpdate()
    {
        PlayerFireAttack();
        _animator.SetInteger("jumpStep", _player.JumpStep);
        if (_player.StopJumpingTime() && !_player.IsFighting)
        {
            OnLanding();
        }
    }
    #endregion
    #region 7 - INJURING
    private void OnInjuringEnter()
    {
        _animator.SetBool("isDying", _player.IsDying);
    }
    private void OnInjuringUpdate()
    {
        if (!_player.IsDying && _player.StopInjuringTime())
        {
            TransitionToState(PlayersStates.PENDING);
        }
    }
    #endregion
    #region 8 - RAGING
    private void OnRagingUpdate()
    {
        if (_player.StopRagingTime())
        {
            TransitionToState(PlayersStates.PENDING);
        }
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