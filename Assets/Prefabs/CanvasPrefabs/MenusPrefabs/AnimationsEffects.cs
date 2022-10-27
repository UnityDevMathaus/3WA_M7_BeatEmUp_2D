using UnityEngine;
public class AnimationsEffects : MonoBehaviour
{
    //##### SERIALIZE FIELD PARAMETERS ###############################################################################################
    [SerializeField] private float _delayForNextSound = 0.5f;
    //##### SERIALIZE FIELD REFERENCES ###############################################################################################
    [SerializeField] private AnimationsEffectsList _animationEffects;
    //##### OBJECTS ##################################################################################################################
    private Animator _animator;
    //##### REGIONS ##################################################################################################################
    #region UNITY API
    void Awake()
    {
        InitializeAwakeReferences();
    }
    void Start()
    {
        InitializeStartReferences();
    }
    void Update()
    {
        PlayEffect();
    }
    #endregion
    //################################################################################################################################
    #region FONCTIONS D'INITIALISATION
    private void InitializeAwakeReferences()
    {
        _animator = GetComponent<Animator>();
    }
    private void InitializeStartReferences()
    {
        _timeForNextSound = Time.time + _delayForNextSound;
    }
    #endregion
    //################################################################################################################################
    #region MECANIQUE DE LA CLASSE
    //##### PRIMITIVES ###############################################################################################################
    private float _timeForNextSound;
    private bool _isFired;
    //################################################################################################################################
    private void PlayEffect()
    {
        if (Time.time > _timeForNextSound && !_isFired)
        {
            _isFired = true;
            switch (_animationEffects)
            {
                case AnimationsEffectsList.DUST1:
                    _animator.SetTrigger("OnDust1");
                    break;
                case AnimationsEffectsList.DUST2:
                    _animator.SetTrigger("OnDust2");
                    break;
                case AnimationsEffectsList.JUMPDUST:
                    _animator.SetTrigger("OnJump");
                    break;
                case AnimationsEffectsList.LANDDUST:
                    _animator.SetTrigger("OnLand");
                    break;
                default:
                    break;
            }
        }
    }
    public void EndEffect()
    {
        gameObject.SetActive(false);
    }
    #endregion
    //################################################################################################################################
}