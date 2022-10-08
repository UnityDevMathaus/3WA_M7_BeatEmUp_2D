using UnityEngine;

public class AnimationsEffects : MonoBehaviour
{
    private float _timeForNextSound;
    [SerializeField] private float _delayForNextSound = 0.5f;
    [SerializeField] private AnimationsEffectsList _animationEffects;
    private bool _isFired;
    private Animator _animator;
    //################################################################################################################################
    #region UNITY API
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _timeForNextSound = Time.time + _delayForNextSound;
    }
    void Update()
    {
        PlayEffect();
    }
    #endregion
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
                default:
                    break;
            }
        }
    }
    public void EndEffect()
    {
        gameObject.SetActive(false);
    }
    //################################################################################################################################
}