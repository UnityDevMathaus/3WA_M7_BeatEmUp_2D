using UnityEngine;

public class FinalBossesManager : MonoBehaviour
{
    [SerializeField] private IntVariable _finalBossesCount;

    private void Awake()
    {
        _finalBossesCount.Value = GameObject.FindGameObjectsWithTag("FinalBosses").Length;
    }

    public int FinalBossesRemaining()
    {
        return _finalBossesCount.Value;
    }

}