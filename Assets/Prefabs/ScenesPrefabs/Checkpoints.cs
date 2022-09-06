using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private CheckpointsList _checkpointCondition;
    [SerializeField] private IntVariable _currentCheckpoint;
    private int _checkpointConditionValue;

    void Start()
    {
        _checkpointConditionValue = (int)_checkpointCondition; //Permet d'éviter le cast à chaque frame.
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("WorldPlayers"))
        {
            _currentCheckpoint.Value = _checkpointConditionValue;
            Destroy(gameObject);
        }
    }
}
