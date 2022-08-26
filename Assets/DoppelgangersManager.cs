using UnityEngine;

public class DoppelgangersManager : MonoBehaviour
{
    [SerializeField] private IntVariable _doppelgangersCount;

    private void Awake()
    {
        _doppelgangersCount.Value = FindObjectsOfType<Doppelgangers>(true).Length;
    }

    public int DoppelgangersRemaining()
    {
        return _doppelgangersCount.Value;
    }

}