using UnityEngine;

public class Bosses : MonoBehaviour
{
    [SerializeField] private BossesIdentities _bossIdentity;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }

    void Start()
    {
        _renderer.SetBossesSprites(_bossIdentity);
    }
}