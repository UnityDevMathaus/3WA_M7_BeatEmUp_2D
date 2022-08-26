using UnityEngine;

public class Doppelgangers : MonoBehaviour
{
    [SerializeField] private PlayersIdentities _playerIdentity;

    void Awake()
    {
        GetComponentInChildren<CharactersRenderers>().SetPlayersSprites(_playerIdentity);
    }

}