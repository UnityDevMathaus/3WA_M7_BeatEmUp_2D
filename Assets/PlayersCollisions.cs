using UnityEngine;

public class PlayersCollisions : MonoBehaviour
{
    [SerializeField] private PlayersMovementsOnX _playerMovementsOnX;
    [SerializeField] private PlayersMovementsOnY _playerMovementsOnY;
    private bool _isInjuring; public bool IsInjuring { get => _isInjuring; set => _isInjuring = value; }
    private float _timeForInvulnerability;
    private float _delayFForInvulnerability = 1f;

    public void GetInjured()
    {
        if (Time.time > _timeForInvulnerability)
        {
            _timeForInvulnerability = Time.time + _delayFForInvulnerability;
            _isInjuring = true;
        }
    }
}