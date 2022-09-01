using UnityEngine;

public class EnemiesManager : MonoBehaviour, IManager
{
    [SerializeField] private IntVariable _enemiesCount;

    private void Awake()
    {
        _enemiesCount.Value = FindObjectsOfType<Enemies>(true).Length;
    }

    public int EntitiesRemaining()
    {
        return _enemiesCount.Value;
    }
}