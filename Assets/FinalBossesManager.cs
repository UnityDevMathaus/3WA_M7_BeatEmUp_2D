using UnityEngine;

public class FinalBossesManager : MonoBehaviour, IManager
{
    [SerializeField] private IntVariable _finalBossesCount;

    private void Awake()
    {
        _finalBossesCount.Value = GameObject.FindGameObjectsWithTag("FinalBosses").Length;
    }

    public int EntitiesRemaining()
    {
        return _finalBossesCount.Value;
    }
}