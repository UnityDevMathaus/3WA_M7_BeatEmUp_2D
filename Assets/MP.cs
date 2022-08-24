using UnityEngine;

public class MP : MonoBehaviour
{
    [SerializeField] private IntVariable _maxMP;
    [SerializeField] private IntVariable _currentMP;

    void Awake()
    {
        _currentMP.Value = _maxMP.Value;
    }
}