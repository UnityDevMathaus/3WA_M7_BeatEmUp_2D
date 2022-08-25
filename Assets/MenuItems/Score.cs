
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private IntVariable _currentScore;

    void Awake()
    {
        _currentScore.Value = 0;
    }
}