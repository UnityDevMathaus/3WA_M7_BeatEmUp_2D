using UnityEngine;
public class EndingCharacterAnimation : MonoBehaviour
{
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private PlayersIdentities _playerIdentity;
    //##### OBJECTS ##################################################################################################################
    private Animator _animator;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _animator = GetComponent<Animator>();
        switch (_playerIdentity)
        {
            case PlayersIdentities.BRANDON:
                _animator.SetTrigger("OnBrandonVictory");
                break;
            case PlayersIdentities.CHRIS:
                _animator.SetTrigger("OnChrisVictory");
                break;
            case PlayersIdentities.JOSE:
                _animator.SetTrigger("OnJoseVictory");
                break;
            case PlayersIdentities.MIKE:
                _animator.SetTrigger("OnMikeVictory");
                break;
            case PlayersIdentities.BEN:
                _animator.SetTrigger("OnBenVictory");
                break;
            default:
                break;
        }
    }
    #endregion
    //################################################################################################################################
}