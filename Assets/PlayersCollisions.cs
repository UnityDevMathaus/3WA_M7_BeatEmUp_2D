using UnityEngine;

public class PlayersCollisions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private PlayersMovementsOnX _playerMovementsOnX;
    [SerializeField] private PlayersMovementsOnY _playerMovementsOnY;
    private bool _isInjuring; public bool IsInjuring { get => _isInjuring; set => _isInjuring = value; }
    private float _timeForInvulnerability;
    private float _delayFForInvulnerability = 1f;

    public void GetInjured()
    {
        if (Time.time > _timeForInvulnerability)
        {
            StartInjuringTime();
            _timeForInvulnerability = Time.time + _delayFForInvulnerability;
            _isInjuring = true;
        }
    }

    private float _timeForInjuring = -0.8f;
    private float _delayForInjuring = 0.8f;
    public void StartInjuringTime()
    {
        _timeForInjuring = Time.time;
    }

    private void Update()
    {
        InjuringMecanics();
        Debug.Log(_isInjuring);
    }


    private void InjuringMecanics()
    {
        SetInjureRenderer();
    }

    public void SetInjureRenderer()
    {
        float timing = Time.time - _timeForInjuring;
        if (0.2f >= timing || (0.4f < timing && 0.6f > timing))
        {
            _playerRenderer.color = Color.red;
        }
        else
        {
            _playerRenderer.color = Color.white;
        }
    }
}