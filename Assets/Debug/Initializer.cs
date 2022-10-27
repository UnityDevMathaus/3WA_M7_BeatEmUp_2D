using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] IntVariable _currentScore;
    [SerializeField] IntVariable _currentStageScore;
    [SerializeField] IntVariable _currentCompleteTime;
    [SerializeField] IntVariable _currentCheckpoint;
    [SerializeField] IntVariable _currentWave;
    void Start()
    {
        _currentScore.Value = 0;
        _currentStageScore.Value = 0;
        _currentCompleteTime.Value = 0;
        _currentCheckpoint.Value = 0;
        _currentWave.Value = 0;
    }
}