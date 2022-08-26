using UnityEngine;

public class Doppelgangers : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }
    void Start()
    {
        _renderer.SetPlayersSprites(_playerIdentity);
    }
}