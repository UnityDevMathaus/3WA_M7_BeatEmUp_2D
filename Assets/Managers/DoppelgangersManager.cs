using UnityEngine;

public class DoppelgangersManager : MonoBehaviour, IManager
{
    [SerializeField] private IntVariable _doppelgangersCount;

    private void Awake()
    {
        _doppelgangersCount.Value = FindObjectsOfType<Doppelgangers>(true).Length;
    }

    public int EntitiesRemaining()
    {
        return _doppelgangersCount.Value;
    }
}