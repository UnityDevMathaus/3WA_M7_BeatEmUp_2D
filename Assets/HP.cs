using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private IntVariable _maxHP;
    [SerializeField] private IntVariable _currentHP;

    void Awake()
    {
        _currentHP.Value = _maxHP.Value;
    }
}
