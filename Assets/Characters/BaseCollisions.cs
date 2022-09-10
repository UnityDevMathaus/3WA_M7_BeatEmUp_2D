using UnityEngine;

public class BaseCollisions : MonoBehaviour
{
    private bool _isInjuring; public bool IsInjuring { get => _isInjuring; set => _isInjuring = value; }
    private float _timeForInvulnerability;
    private float _delayForInvulnerability = 1f;

    public void GetInjured()
    {
        if (Time.time > _timeForInvulnerability)
        {
            _timeForInvulnerability = Time.time + _delayForInvulnerability;
            _isInjuring = true;
        }
        Debug.Log("s");
    }
}