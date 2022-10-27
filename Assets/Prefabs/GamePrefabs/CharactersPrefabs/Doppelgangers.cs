using UnityEngine;

public class Doppelgangers : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;
    [SerializeField] private IntVariable _doppelgangersCount;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }
    void Start()
    {
        _renderer.SetPlayersSprites(_playerIdentity);
    }
    void Update()
    {
        DebugKillMeDoppelgangers();
    }
    void OnDestroy()
    {
        _doppelgangersCount.Value--;
    }
    private void DebugKillMeDoppelgangers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && gameObject !=null) Destroy(gameObject);
    }
}