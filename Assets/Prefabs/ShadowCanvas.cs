using UnityEngine;

public class ShadowCanvas : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AnimationsEffects _jumpEffect;
    [SerializeField] private AnimationsEffects _landEffect;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void StartJump()
    {
        _animator.SetTrigger("OnJump");
        _jumpEffect.gameObject.SetActive(true);
    }
    public void StartLand()
    {
        _landEffect.gameObject.SetActive(true);
    }
}