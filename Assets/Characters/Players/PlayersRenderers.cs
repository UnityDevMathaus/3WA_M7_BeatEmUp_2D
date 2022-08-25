using UnityEngine;

public class PlayersRenderers : MonoBehaviour
{
    [SerializeField] private Sprite[] _playersSprites;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>(); 
    }

    public void SetPlayerSprite(PlayersIdentities _playerIdentity)
    {
        _renderer.sprite = _playersSprites[(int)_playerIdentity];
    }
}