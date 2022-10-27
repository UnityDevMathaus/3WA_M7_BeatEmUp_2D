using UnityEngine;

public class Bosses : MonoBehaviour
{
    [SerializeField] private BossesIdentities _bossIdentity;
    [SerializeField] private IntVariable _bossesCount;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }

    void Start()
    {
        _renderer.SetBossesSprites(_bossIdentity);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        _bossesCount.Value--;
    }
}