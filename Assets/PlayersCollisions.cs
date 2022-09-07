using UnityEngine;

public class PlayersCollisions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private PlayersMovementsOnX _playerMovementsOnX;
    [SerializeField] private PlayersMovementsOnY _playerMovementsOnY;

    private float _timeForInjuring = -0.8f;
    private float _delayForInjuring = 0.8f;

    private void Update()
    {
        InjuringMecanics();
    }
    public void GetInjured()
    {
        StartInjuringTime();
    }

    private void InjuringMecanics()
    {
        SetInjureRenderer();
    }

    public void StartInjuringTime()
    {
        _timeForInjuring = Time.time;
    }
    public bool StopInjuringTime()
    {
        return (_timeForInjuring + _delayForInjuring < Time.time);
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