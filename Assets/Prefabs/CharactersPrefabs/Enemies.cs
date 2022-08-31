using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private EnemiesIdentities _enemyIdentity;
    [SerializeField] private IntVariable _enemiesCount;
    private CharactersRenderers _renderer;

    void Awake()
    {
        _renderer = GetComponentInChildren<CharactersRenderers>();
    }

    void Start()
    {
        _renderer.SetEnemiesSprites(_enemyIdentity);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("?");
        if (collider.CompareTag("Player"))
        {
            Debug.Log("?");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        _enemiesCount.Value--;
    }
}