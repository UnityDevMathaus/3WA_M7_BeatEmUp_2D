using UnityEngine;

public class CharactersRenderers : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetPlayersSprites(PlayersIdentities _playerIdentity)
    {
        _renderer.sprite = _sprites[(int)_playerIdentity];
    }

    public void SetEnemiesSprites(EnemiesIdentities _playerIdentity)
    {
        _renderer.sprite = _sprites[(int)_playerIdentity];
    }

    public void SetBossesSprites(BossesIdentities _playerIdentity)
    {
        _renderer.sprite = _sprites[(int)_playerIdentity];
    }

    public void SetSpritesByIndex(int index)
    {
        _renderer.sprite = _sprites[index];
    }
}
