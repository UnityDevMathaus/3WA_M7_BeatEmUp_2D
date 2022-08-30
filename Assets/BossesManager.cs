using UnityEngine;

public class BossesManager : MonoBehaviour, IManager
{
    [SerializeField] private IntVariable _bossesCount;

    private void Awake()
    {
        _bossesCount.Value = FindObjectsOfType<Bosses>(true).Length;
    }

    public int EntitiesRemaining()
    {
        return _bossesCount.Value;
    }
}